
import {
  Component,
  OnInit,
  inject,
  ChangeDetectorRef
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { DamageReportService } from '../../services/damage-report';
import {
  DamageReport as DamageReportApi,
  CreateDamageReportRequest
} from '../../models/damage-report';

interface DamageReport {
  id: number;
  date: string;
  equipment: string;
  serial: string;
  description: string;
  reportedBy: string;
  status: 'Pending' | 'Resolved';
  icon: string;
}

@Component({
  selector: 'app-damagereports',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './damagereports.html',
  styleUrl: './damagereports.css',
})
export class Damagereports implements OnInit {
  private damageReportService = inject(DamageReportService);
  private cdr = inject(ChangeDetectorRef);

  searchText = '';
  selectedStatus = 'All Status';
  startDate = '';
  endDate = '';

  showReportForm = false;
  currentPage = 1;
  pageSize = 4;

  selectedReport: DamageReport | null = null;

  loading = false;
  saving = false;
  errorMessage = '';
  successMessage = '';

  newReport = {
    equipment: '',
    description: '',
    reportedBy: 'Ahmad'
  };

  reports: DamageReport[] = [];

  ngOnInit(): void {
    this.loadReports();
  }

  // GET: api/DamageReports
  loadReports(): void {
    this.loading = true;
    this.errorMessage = '';

    this.damageReportService.getAll({
      search: this.searchText,
      status: this.selectedStatus,
      startDate: this.startDate,
      endDate: this.endDate
    })
    .pipe(
      finalize(() => {
        this.loading = false;
        this.cdr.detectChanges();
      })
    )
    .subscribe({
      next: records => {
        this.reports = records.map(record => this.mapReport(record));

        this.currentPage = Math.min(
          this.currentPage,
          this.totalPages
        );
      },
      error: error => {
        console.error('Failed to load damage reports:', error);
        this.errorMessage =
          error?.error?.message ??
          'Unable to load damage reports. Please try again.';
      }
    });
  }

  // API filtering is server-side.
  get filteredReports(): DamageReport[] {
    return this.reports;
  }

  get paginatedReports(): DamageReport[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredReports.slice(start, start + this.pageSize);
  }

  get totalPages(): number {
    return Math.max(
      1,
      Math.ceil(this.filteredReports.length / this.pageSize)
    );
  }

  get firstVisible(): number {
    return this.filteredReports.length === 0
      ? 0
      : (this.currentPage - 1) * this.pageSize + 1;
  }

  get lastVisible(): number {
    return Math.min(
      this.currentPage * this.pageSize,
      this.filteredReports.length
    );
  }

  applyFilters(): void {
    this.currentPage = 1;
    this.loadReports();
  }

  resetFilters(): void {
    this.searchText = '';
    this.selectedStatus = 'All Status';
    this.startDate = '';
    this.endDate = '';
    this.currentPage = 1;

    this.loadReports();
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  openReportForm(): void {
    this.newReport = {
      equipment: '',
      description: '',
      reportedBy: 'Ahmad'
    };

    this.errorMessage = '';
    this.successMessage = '';
    this.showReportForm = true;
  }

  // POST: api/DamageReports
  saveReport(): void {
    if (
      !this.newReport.equipment.trim() ||
      !this.newReport.description.trim() ||
      !this.newReport.reportedBy.trim() ||
      this.saving
    ) {
      return;
    }

    const request: CreateDamageReportRequest = {
      equipment: this.newReport.equipment.trim(),
      description: this.newReport.description.trim(),
      reportedBy: this.newReport.reportedBy.trim(),
      serial: 'N/A'
    };

    this.saving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.damageReportService.create(request)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: created => {
          this.showReportForm = false;
          this.successMessage = 'Damage report submitted successfully.';

          // Clear filters so the newly created report is visible.
          this.searchText = '';
          this.selectedStatus = 'All Status';
          this.startDate = '';
          this.endDate = '';
          this.currentPage = 1;

          // Use the server response, then refresh the list.
          const mapped = this.mapReport(created);

          this.reports = [
            mapped,
            ...this.reports.filter(r => r.id !== mapped.id)
          ];

          this.loadReports();
        },
        error: error => {
          console.error('Failed to create damage report:', error);
          this.errorMessage =
            error?.error?.message ??
            'Unable to submit the damage report. Please try again.';
        }
      });
  }

  // GET: api/DamageReports/{id}
  viewReport(report: DamageReport): void {
    this.errorMessage = '';

    this.damageReportService.getById(report.id).subscribe({
      next: record => {
        this.selectedReport = this.mapReport(record);
      },
      error: error => {
        console.error('Failed to load damage report details:', error);
        this.errorMessage =
          error?.error?.message ??
          'Unable to load report details.';
      }
    });
  }

  closeDetails(): void {
    this.selectedReport = null;
  }

  private mapReport(record: DamageReportApi): DamageReport {
    return {
      id: record.id,
      date: this.formatDate(record.date),
      equipment: record.equipment,
      serial: record.serial || 'N/A',
      description: record.description,
      reportedBy: record.reportedBy,
      status: record.status === 'Resolved' ? 'Resolved' : 'Pending',
      icon: this.getIcon(record.equipment)
    };
  }

  private formatDate(value: string): string {
    if (!value) {
      return '';
    }

    const date = new Date(value);

    if (Number.isNaN(date.getTime())) {
      return value;
    }

    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
  }

  private getIcon(equipment: string): string {
    const value = equipment.toLowerCase();

    if (value.includes('gps') || value.includes('rover')) {
      return 'gps';
    }

    if (value.includes('drone')) {
      return 'drone';
    }

    if (value.includes('tablet')) {
      return 'tablet';
    }

    if (value.includes('tripod')) {
      return 'tripod';
    }

    return 'equipment';
  }
}

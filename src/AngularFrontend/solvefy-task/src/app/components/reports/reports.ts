
import {
  Component,
  OnInit,
  inject,
  ChangeDetectorRef
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { EquipmentHistoryService } from '../../services/equipment-history';
import { EquipmentHistoryRecord } from '../../models/equipment-history';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './reports.html',
  styleUrl: './reports.css',
})
export class Reports implements OnInit {
  private historyService = inject(EquipmentHistoryService);
  private cdr = inject(ChangeDetectorRef);

  startDate = '2026-09-01';
  endDate = '2026-09-30';

  appliedStartDate = this.startDate;
  appliedEndDate = this.endDate;

  history: EquipmentHistoryRecord[] = [];

  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadHistory();
  }

  // GET: api/EquipmentHistory
  loadHistory(): void {
    this.loading = true;
    this.errorMessage = '';

    this.historyService
      .getAll(this.appliedStartDate, this.appliedEndDate)
      .pipe(
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: records => {
          this.history = records.map(record => ({
            ...record,
            date: this.formatDate(record.date),
            remarks: record.remarks ?? ''
          }));
        },
        error: error => {
          console.error('Failed to load equipment history:', error);

          this.errorMessage =
            error?.error?.message ??
            'Unable to load equipment history. Please try again.';
        }
      });
  }

  get filteredHistory(): EquipmentHistoryRecord[] {
    // Date filtering is performed by the backend.
    return this.history;
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

  applyFilter(): void {
    if (
      this.startDate &&
      this.endDate &&
      this.startDate > this.endDate
    ) {
      alert('Start date cannot be later than end date.');
      return;
    }

    this.appliedStartDate = this.startDate;
    this.appliedEndDate = this.endDate;

    this.loadHistory();
  }

  resetFilter(): void {
    this.startDate = '';
    this.endDate = '';
    this.appliedStartDate = '';
    this.appliedEndDate = '';

    this.loadHistory();
  }
}

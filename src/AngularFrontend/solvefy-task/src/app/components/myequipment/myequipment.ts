
import {
  ChangeDetectorRef,
  Component,
  inject,
  OnInit
} from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';

import { MyEquipmentService } from '../../services/my-equipment';
import { MyEquipment } from '../../models/my-equipment';

interface AssignedEquipment {
  id: number;
  name: string;
  serialNumber: string;
  type: string;
  checkedOutDate: string;
  expectedReturnDate: string;
  status: 'Checked Out' | 'Overdue' | 'Returned';
  icon: string;
}

@Component({
  selector: 'app-myequipment',
  standalone: true,
  imports: [],
  templateUrl: './myequipment.html',
  styleUrl: './myequipment.css',
})
export class Myequipment implements OnInit {
  private router = inject(Router);
  private myEquipmentService = inject(MyEquipmentService);
  private cdr = inject(ChangeDetectorRef);

  // Replace with the currently logged-in employee's name.
  assignedTo = 'Ahmad';

  equipment: AssignedEquipment[] = [];

  loading = false;
  returningId: number | null = null;
  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    this.loadMyEquipment();
  }

  loadMyEquipment(): void {
    this.loading = true;
    this.errorMessage = '';

    this.myEquipmentService
      .getMyEquipment(this.assignedTo)
      .pipe(
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (records: MyEquipment[]) => {
          this.equipment = records.map(record => ({
            id: record.id,
            name: record.equipmentName,
            serialNumber: record.serialNumber ?? '',
            type: record.type ?? '',
            checkedOutDate: this.formatDate(record.checkedOutDate),
            expectedReturnDate: this.formatDate(
              record.expectedReturnDate
            ),
            status: this.getStatus(record),
            icon: this.getIcon(
              record.type,
              record.equipmentName
            )
          }));
        },
        error: error => {
          console.error('Failed to load my equipment:', error);
          this.errorMessage =
            error?.error?.message ??
            'Unable to load your equipment. Please try again.';
        }
      });
  }

  returnEquipment(item: AssignedEquipment): void {
    if (
      item.status === 'Returned' ||
      this.returningId !== null
    ) {
      return;
    }

    const confirmed = window.confirm(
      `Do you want to return ${item.name}?`
    );

    if (!confirmed) {
      return;
    }

    this.returningId = item.id;
    this.errorMessage = '';
    this.successMessage = '';

    this.myEquipmentService
      .returnEquipment(item.id)
      .pipe(
        finalize(() => {
          this.returningId = null;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: response => {
          item.status = 'Returned';
          this.successMessage =
            response.message ||
            `${item.name} has been returned successfully.`;
        },
        error: error => {
          console.error('Failed to return equipment:', error);

          this.errorMessage =
            error?.error?.message ??
            'Unable to return equipment. Please try again.';
        }
      });
  }

  private getStatus(
    record: MyEquipment
  ): AssignedEquipment['status'] {
    if (record.status === 'Returned') {
      return 'Returned';
    }

    const expectedDate = new Date(record.expectedReturnDate);
    const today = new Date();

    expectedDate.setHours(0, 0, 0, 0);
    today.setHours(0, 0, 0, 0);

    return expectedDate < today ? 'Overdue' : 'Checked Out';
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

  private getIcon(type: string | null, name: string): string {
    const value = `${type ?? ''} ${name}`.toLowerCase();

    if (value.includes('gps') || value.includes('rover')) {
      return 'gps';
    }

    if (value.includes('tablet')) {
      return 'tablet';
    }

    if (value.includes('drone')) {
      return 'drone';
    }

    if (value.includes('tripod')) {
      return 'tripod';
    }

    return 'tablet';
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}

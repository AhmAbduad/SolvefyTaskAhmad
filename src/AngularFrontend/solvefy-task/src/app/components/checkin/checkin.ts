
import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { CheckInService } from '../../services/checkin';
import {
  CheckInEquipment,
  CreateCheckInRequest
} from '../../models/checkin';

@Component({
  selector: 'app-checkin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkin.html',
  styleUrl: './checkin.css',
})
export class Checkin implements OnInit {
  private router = inject(Router);
  private checkInService = inject(CheckInService);

  private cdr = inject(ChangeDetectorRef);
  selectedEquipment: number | null = null;

  returnDate = this.getLocalDateString();
  returnTime = '16:30';
  condition = 'Good';
  notes = '';

  submitted = false;
  loadingEquipment = false;
  loadingDetails = false;
  saving = false;

  errorMessage = '';
  successMessage = '';

  equipmentOptions: CheckInEquipment[] = [];
  selectedItem: CheckInEquipment | null = null;

  ngOnInit(): void {
    this.loadEquipment();
  }

  private getLocalDateString(): string {
    const date = new Date();

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  // API 1: Load equipment from SQL Server.
  loadEquipment(): void {
    this.loadingEquipment = true;
    this.errorMessage = '';

    this.checkInService.getEquipment().subscribe({
      next: (equipment) => {
        this.equipmentOptions = equipment;
        this.loadingEquipment = false;
          this.cdr.detectChanges();

        if (equipment.length > 0) {
          this.selectedEquipment = equipment[0].id;
          this.cdr.detectChanges();
          this.onEquipmentChange();
        }
      },
      error: (error) => {
        console.error('Failed to load equipment:', error);

        this.loadingEquipment = false;
        this.errorMessage =
          'Unable to load equipment from the database.';
      }
    });
  }

  // API 2: Get the selected equipment details.
  onEquipmentChange(): void {
    this.selectedItem = null;
    this.errorMessage = '';

    const equipmentId = this.selectedEquipment;

    if (equipmentId === null) {
      return;
    }

    this.loadingDetails = true;
          this.cdr.detectChanges();

    this.checkInService.getEquipmentById(equipmentId).subscribe({
      next: (equipment) => {
        if (this.selectedEquipment === equipmentId) {
          this.selectedItem = equipment;
        }

        this.loadingDetails = false;
      },
      error: (error) => {
        console.error('Failed to load equipment details:', error);

        if (this.selectedEquipment === equipmentId) {
          this.selectedItem = null;
          this.errorMessage =
            'Unable to load the selected equipment details.';
        }

        this.loadingDetails = false;
      }
    });
  }

  // API 3: Save the check-in record.
  checkIn(): void {
    this.submitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (
      this.selectedEquipment === null ||
      !this.returnDate ||
      !this.returnTime ||
      !this.condition
    ) {
      this.errorMessage =
        'Please complete all required fields.';
      return;
    }

    if (this.loadingEquipment || this.loadingDetails || this.saving) {
      return;
    }

    if (!this.selectedItem) {
      this.errorMessage =
        'Please select valid equipment before checking it in.';
      return;
    }

    const request: CreateCheckInRequest = {
      equipmentId: this.selectedEquipment,
      returnDate: this.returnDate,
      returnTime: `${this.returnTime}:00`,
      condition: this.condition,
      notes: this.notes.trim()
    };

    console.log('Check-in request:', request);

    this.saving = true;

    this.checkInService.createCheckIn(request).subscribe({
      next: (result) => {
        console.log('Check-in saved:', result);

        this.saving = false;
        this.successMessage =
          'Equipment check-in saved successfully.';

        // Reflect the status returned by the backend.
        if (this.selectedItem && result.status) {
          this.selectedItem = {
            ...this.selectedItem,
            status: result.status
          };
        }

        alert(this.successMessage);
      },
      error: (error) => {
        console.error('Check-in failed:', error.status, error.error);

        this.saving = false;
        this.errorMessage =
          error?.error?.message ||
          error?.error?.title ||
          'Unable to save the check-in record.';
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/dashboard']);
  }
}

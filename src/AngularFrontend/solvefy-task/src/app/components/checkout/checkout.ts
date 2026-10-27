import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CheckoutService } from '../../services/checkout';
import { CreateCheckoutRequest, EquipmentOption } from '../../models/checkout';
import { ApprovalService } from '../../services/approval';

@Component({
  selector: 'app-checkout',
  imports: [FormsModule],
  templateUrl: './checkout.html',
  styleUrl: './checkout.css',
})
export class Checkout implements OnInit  {

  private approvalService = inject(ApprovalService);
 private router = inject(Router);
  private checkoutService = inject(CheckoutService);

  minReturnDate = this.getLocalDateString();

private cdr = inject(ChangeDetectorRef);

private getLocalDateString(): string {
  const date = new Date();
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
}


  saving = false;
  loadingEquipment = false;
  loadingAssignee = false;

  errorMessage = '';
  successMessage = '';
  submitted = false;

  // Store the equipment ID, not its name.
  selectedEquipment: number | null = null;
  assignedTo = '';
  expectedReturnDate = '';
  purpose = '';

  equipmentOptions: EquipmentOption[] = [];

  ngOnInit(): void {
    this.loadEquipmentOptions();
  }

  // API 1: Load equipment from the database.
  loadEquipmentOptions(): void {
    this.loadingEquipment = true;
    this.errorMessage = '';

    this.checkoutService.getEquipmentOptions().subscribe({
      next: (equipment) => {
        this.equipmentOptions = equipment;
        this.loadingEquipment = false;

        if (equipment.length > 0) {
          this.selectedEquipment = equipment[0].id;
          this.onEquipmentChange();
          this.cdr.detectChanges();
        } else {
          this.selectedEquipment = null;
          this.assignedTo = '';
        }
      },
      error: (error) => {
        console.error('Failed to load equipment:', error);

        this.loadingEquipment = false;
        this.errorMessage =
          'Unable to load equipment. Please try again.';
      }
    });
  }

  // API 2: Get the assignee for the selected equipment ID.
 

onEquipmentChange(): void {
  const equipmentId = this.selectedEquipment;

  this.assignedTo = '';
  this.errorMessage = '';

  if (equipmentId === null) {
    return;
  }

  this.loadingAssignee = true;

  this.checkoutService
    .getAssignedToByEquipmentId(equipmentId)
    .subscribe({
      next: (assignee: string) => {
        console.log('Assignee received from API:', assignee);

        this.assignedTo = assignee.trim();
        this.loadingAssignee = false;

        console.log('Assigned To field:', this.assignedTo);
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Assignee API error:', error);

        this.assignedTo = '';
        this.loadingAssignee = false;
        this.errorMessage = 'Unable to load assignee.';
      }
    });
}



  // API 3: Submit checkout request.
  

requestCheckOut(): void {
  this.submitted = true;
  this.errorMessage = '';
  this.successMessage = '';

  if (
    this.selectedEquipment === null ||
    !this.assignedTo.trim() ||
    !this.expectedReturnDate
  ) {
    this.errorMessage = 'Please complete all required fields.';
    return;
  }

  if (this.saving || this.loadingAssignee) {
    return;
  }

  const selectedItem = this.equipmentOptions.find(
    item => item.id === this.selectedEquipment
  );

  if (!selectedItem) {
    this.errorMessage = 'Please select valid equipment.';
    return;
  }

  // STEP 1: If the selected equipment is a drone,
  // create an approval request instead of checking it out.
  if (
    selectedItem.category.toLowerCase().includes('drone') 
  ) {
    const approvalRequest = {
      equipmentId: selectedItem.id,
      requestedBy: this.assignedTo.trim(),
      expectedReturn: this.expectedReturnDate,
      purpose: this.purpose.trim()
    };

    this.saving = true;

    this.approvalService.create(approvalRequest).subscribe({
     next: (result) => {
        this.saving = false;
        this.successMessage = 'Approval request submitted successfully.';

        console.log('Approval created:', result);
        console.log('Navigating to approval ID:', result.id);

        this.router.navigate(['/approvals', result.id]).then(
          (navigated) => {
            console.log('Navigation successful:', navigated);

            if (!navigated) {
              console.error('Angular could not navigate to the approval page.');
            }
          }
        );
      },
      error: (error) => {
        console.error('Approval request failed:', error);

        this.saving = false;
        this.errorMessage =
          error?.error?.message ||
          error?.error?.title ||
          'Unable to submit the approval request.';
      }
    });

    // IMPORTANT: Stop here. Do not call the normal checkout API.
    return;
  }

  // STEP 2: Non-drone equipment follows your existing checkout process.
  const request: CreateCheckoutRequest = {
    equipmentName: selectedItem.name,
    assignedTo: this.assignedTo.trim(),
    expectedReturnDate: this.expectedReturnDate,
    purpose: this.purpose.trim()
  };

  console.log('Checkout request:', request);

  this.saving = true;

  this.checkoutService.createCheckout(request).subscribe({
    next: (result) => {
      console.log('Checkout saved successfully:', result);

      this.saving = false;
      this.successMessage = 'Checkout request saved successfully.';
      alert(this.successMessage);
    },
    error: (error) => {
      console.error('Checkout save failed:', error.status, error.error);

      this.saving = false;
      this.errorMessage =
        error?.error?.errors?.EquipmentName?.[0] ||
        error?.error?.title ||
        'Unable to save checkout request.';
    }
  });
}



  cancel(): void {
    this.router.navigate(['/dashboard']);
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}

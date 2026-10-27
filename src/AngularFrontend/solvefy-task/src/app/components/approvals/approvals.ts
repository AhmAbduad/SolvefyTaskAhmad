import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ApprovalService } from '../../services/approval';
import { ApprovalRequest } from '../../models/approval';

@Component({
  selector: 'app-approvals',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './approvals.html',
  styleUrl: './approvals.css'
})
export class Approvals implements OnInit {
  private approvalService = inject(ApprovalService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  status: 'Pending Approval' | 'Approved' | 'Rejected' =
    'Pending Approval';

  comments = '';

  request: ApprovalRequest | null = null;
  approvalRequests: ApprovalRequest[] = [];

  loading = false;
  saving = false;
  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam !== null) {
      const id = Number(idParam);

      if (!Number.isInteger(id) || id <= 0) {
        this.errorMessage = 'Invalid approval request ID.';
        return;
      }

      this.loadRequestById(id);
    } else {
      this.loadRequests();
    }
  }

  // GET all approval requests
  loadRequests(): void {
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.approvalService.getAll().subscribe({
      next: (requests) => {
        this.approvalRequests = requests;
        this.loading = false;

        if (requests.length === 0) {
          this.request = null;
          this.errorMessage = 'No approval requests were found.';
          return;
        }

        // Backend returns newest requests first.
        this.setCurrentRequest(requests[0]);
      },
      error: (error) => {
        console.error('Failed to load approval requests:', error);
        this.loading = false;
        this.errorMessage = this.getErrorMessage(
          error,
          'Unable to load approval requests from the server.'
        );
      }
    });
  }

  // GET a specific approval request
  loadRequestById(id: number): void {
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.approvalService.getById(id).subscribe({
      next: (request) => {
        this.setCurrentRequest(request);
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load approval request:', error);
        this.loading = false;
        this.errorMessage = this.getErrorMessage(
          error,
          'Unable to load this approval request.'
        );
      }
    });
  }

  private setCurrentRequest(request: ApprovalRequest): void {
    this.request = request;
    this.status = request.status;
    this.comments = request.comments ?? '';
  }

  // PUT approve endpoint
  approve(): void {
    if (!this.canMakeDecision()) {
      return;
    }

    this.saving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.approvalService
      .approve(this.request!.id, this.comments)
      .subscribe({
        next: (updatedRequest) => {
          this.setCurrentRequest(updatedRequest);
          this.saving = false;
          this.successMessage = 'Approval request approved successfully.';
          this.updateRequestInList(updatedRequest);
        },
        error: (error) => {
          console.error('Approval failed:', error);
          this.saving = false;
          this.errorMessage = this.getErrorMessage(
            error,
            'Unable to approve this request.'
          );
        }
      });
  }

  // PUT reject endpoint
  reject(): void {
    if (!this.canMakeDecision()) {
      return;
    }

    this.saving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.approvalService
      .reject(this.request!.id, this.comments)
      .subscribe({
        next: (updatedRequest) => {
          this.setCurrentRequest(updatedRequest);
          this.saving = false;
          this.successMessage = 'Approval request rejected successfully.';
          this.updateRequestInList(updatedRequest);
        },
        error: (error) => {
          console.error('Rejection failed:', error);
          this.saving = false;
          this.errorMessage = this.getErrorMessage(
            error,
            'Unable to reject this request.'
          );
        }
      });
  }

  private canMakeDecision(): boolean {
    if (!this.request) {
      this.errorMessage = 'No approval request is selected.';
      return false;
    }

    if (this.status !== 'Pending Approval') {
      return false;
    }

    if (this.loading || this.saving) {
      return false;
    }

    return true;
  }

  private updateRequestInList(updatedRequest: ApprovalRequest): void {
    this.approvalRequests = this.approvalRequests.map((item) =>
      item.id === updatedRequest.id ? updatedRequest : item
    );
  }

  private getErrorMessage(error: any, fallback: string): string {
    return error?.error?.message
      || error?.error?.title
      || fallback;
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}
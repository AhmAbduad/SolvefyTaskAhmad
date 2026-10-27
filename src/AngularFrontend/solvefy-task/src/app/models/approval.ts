export interface ApprovalRequest {
  id: number;
  equipmentId: number;
  equipmentName: string;
  serialNumber: string | null;
  requestedBy: string;
  requestedOn: string;
  expectedReturn: string;
  purpose: string;
  status: 'Pending Approval' | 'Approved' | 'Rejected';
  comments: string | null;
  decisionAt: string | null;
}

export interface CreateApprovalRequest {
  equipmentId: number;
  requestedBy: string;
  expectedReturn: string;
  purpose: string;
}

export interface ApprovalDecision {
  comments: string | null;
}
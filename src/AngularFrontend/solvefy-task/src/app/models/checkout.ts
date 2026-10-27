export interface CreateCheckoutRequest {
  equipmentName: string;
  assignedTo: string;
  expectedReturnDate: string;
  purpose: string;
}

export interface EquipmentOption {
  id: number;
  name: string;
  type: string;
  category: string;
}

export interface CheckoutRecord {
  id: number;
  equipmentName: string;
  assignedTo: string;
  expectedReturnDate: string;
  purpose: string;
  status: string;
  createdAt: string;
}
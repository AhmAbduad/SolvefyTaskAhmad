export interface MyEquipment {
  id: number;
  equipmentName: string;
  serialNumber: string | null;
  type: string | null;
  checkedOutDate: string;
  expectedReturnDate: string;
  status: 'Checked Out' | 'Overdue' | 'Returned';
}

export interface ReturnEquipmentResponse {
  message: string;
  id: number;
  equipmentName: string;
  status: string;
}

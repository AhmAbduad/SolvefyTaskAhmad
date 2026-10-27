export interface CheckInEquipment {
  id: number;
  name: string;
  serial: string | null;
  type: string;
  status: string;
}

export interface CreateCheckInRequest {
  equipmentId: number;
  returnDate: string;
  returnTime: string;
  condition: string;
  notes: string;
}

export interface CheckInRecord {
  id: number;
  equipmentId: number;
  returnDate: string;
  returnTime: string;
  condition: string;
  notes: string | null;
  status: string;
  createdAt: string;
}
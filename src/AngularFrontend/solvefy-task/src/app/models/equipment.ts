export interface Equipment {
  id: number;
  name: string;
  category: string;
  serialNumber?: string | null;
  type: string;
  status: string;
  assignedTo?: string | null;
}

export interface CreateEquipmentRequest {
  name: string;
  category: string;
  serialNumber?: string | null;
  type: string;
}
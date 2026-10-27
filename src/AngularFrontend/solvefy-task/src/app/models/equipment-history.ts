export interface EquipmentHistoryRecord {
  id: number;
  date: string;
  equipment: string;
  action: 'Returned' | 'Checked Out';
  person: string;
  remarks: string;
}
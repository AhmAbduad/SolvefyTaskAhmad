export interface DamageReport {
  id: number;
  date: string;
  equipment: string;
  serial: string;
  description: string;
  reportedBy: string;
  status: 'Pending' | 'Resolved';
  icon: string;
}

export interface CreateDamageReportRequest {
  equipment: string;
  serial?: string;
  description: string;
  reportedBy: string;
}
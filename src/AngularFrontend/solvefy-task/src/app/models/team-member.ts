export type TeamRole = 'Field Staff' | 'Office Staff' | 'Admin';
export type TeamStatus = 'Active' | 'Inactive';

export interface TeamMember {
  id: number;
  name: string;
  role: TeamRole;
  phone: string;
  email: string;
  status: TeamStatus;
}

export interface CreateTeamMemberDto {
  name: string;
  role: TeamRole;
  phone: string;
  email: string;
  status: TeamStatus;
}

export type UpdateTeamMemberDto = CreateTeamMemberDto;
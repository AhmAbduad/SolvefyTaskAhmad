import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { TeamMembersService } from '../../services/team-members';
import {
  TeamMember,
  TeamRole,
  TeamStatus,
  CreateTeamMemberDto
} from '../../models/team-member';

@Component({
  selector: 'app-team-members',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './teamsmembers.html',
  styleUrl: './teamsmembers.css'
})
export class TeamMembers implements OnInit {
  private readonly teamService = inject(TeamMembersService);

  searchText = '';
  selectedRole = 'All Roles';

  currentPage = 1;
  pageSize = 7;

  showForm = false;
  isEditing = false;
  editingId: number | null = null;

  isLoading = false;
  isSaving = false;
  errorMessage = '';

  roles = ['All Roles', 'Field Staff', 'Office Staff', 'Admin'];

  memberForm: CreateTeamMemberDto = this.emptyMember();

  members: TeamMember[] = [];

  ngOnInit(): void {
    this.loadMembers();
  }

  private emptyMember(): CreateTeamMemberDto {
    return {
      name: '',
      role: 'Field Staff',
      phone: '',
      email: '',
      status: 'Active'
    };
  }

  loadMembers(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.teamService.getAll().subscribe({
      next: (members) => {
        this.members = members;
        this.currentPage = Math.min(this.currentPage, this.totalPages);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading team members:', error);
        this.errorMessage =
          error.error?.message ?? 'Unable to load team members.';
        this.isLoading = false;
      }
    });
  }

  get filteredMembers(): TeamMember[] {
    const search = this.searchText.trim().toLowerCase();

    return this.members.filter(member => {
      const matchesSearch =
        member.name.toLowerCase().includes(search) ||
        member.phone.toLowerCase().includes(search) ||
        member.email.toLowerCase().includes(search);

      const matchesRole =
        this.selectedRole === 'All Roles' ||
        member.role === this.selectedRole;

      return matchesSearch && matchesRole;
    });
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.filteredMembers.length / this.pageSize));
  }

  get paginatedMembers(): TeamMember[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredMembers.slice(start, start + this.pageSize);
  }

  get firstVisible(): number {
    return this.filteredMembers.length === 0
      ? 0
      : (this.currentPage - 1) * this.pageSize + 1;
  }

  get lastVisible(): number {
    return Math.min(
      this.currentPage * this.pageSize,
      this.filteredMembers.length
    );
  }

  onFilterChange(): void {
    this.currentPage = 1;
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  openAddForm(): void {
    this.isEditing = false;
    this.editingId = null;
    this.memberForm = this.emptyMember();
    this.errorMessage = '';
    this.showForm = true;
  }

  editMember(member: TeamMember): void {
    this.isEditing = true;
    this.editingId = member.id;

    this.memberForm = {
      name: member.name,
      role: member.role,
      phone: member.phone,
      email: member.email,
      status: member.status
    };

    this.errorMessage = '';
    this.showForm = true;
  }

  saveMember(): void {
    const name = this.memberForm.name.trim();
    const phone = this.memberForm.phone.trim();
    const email = this.memberForm.email.trim();

    if (!name || !phone || !email) {
      this.errorMessage = 'Please fill in all required fields.';
      return;
    }

    if (this.isSaving) {
      return;
    }

    const dto: CreateTeamMemberDto = {
      name,
      role: this.memberForm.role,
      phone,
      email,
      status: this.memberForm.status
    };

    this.isSaving = true;
    this.errorMessage = '';

    const request$ =
      this.isEditing && this.editingId !== null
        ? this.teamService.update(this.editingId, dto)
        : this.teamService.create(dto);

    request$.subscribe({
      next: (savedMember) => {
        if (this.isEditing) {
          this.members = this.members.map(member =>
            member.id === savedMember.id ? savedMember : member
          );
        } else {
          this.members = [...this.members, savedMember];
        }

        this.showForm = false;
        this.isSaving = false;
        this.currentPage = this.totalPages;
      },
      error: (error) => {
        console.error('Error saving team member:', error);
        this.errorMessage =
          error.error?.message ?? 'Unable to save team member.';
        this.isSaving = false;
      }
    });
  }

  deleteMember(member: TeamMember): void {
    if (!confirm(`Are you sure you want to delete ${member.name}?`)) {
      return;
    }

    this.errorMessage = '';

    this.teamService.delete(member.id).subscribe({
      next: () => {
        this.members = this.members.filter(m => m.id !== member.id);
        this.currentPage = Math.min(this.currentPage, this.totalPages);
      },
      error: (error) => {
        console.error('Error deleting team member:', error);
        this.errorMessage =
          error.error?.message ?? 'Unable to delete team member.';
      }
    });
  }

  cancelForm(): void {
    this.showForm = false;
    this.errorMessage = '';
  }

  getInitials(name: string): string {
    return name
      .trim()
      .split(/\s+/)
      .slice(0, 2)
      .map(part => part.charAt(0))
      .join('')
      .toUpperCase();
  }

  trackById(_index: number, member: TeamMember): number {
    return member.id;
  }
}
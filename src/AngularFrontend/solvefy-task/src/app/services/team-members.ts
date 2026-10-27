import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import {
  TeamMember,
  CreateTeamMemberDto,
  UpdateTeamMemberDto
} from '../models/team-member';

@Injectable({
  providedIn: 'root'
})
export class TeamMembersService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/TeamMembers`;

  getAll(): Observable<TeamMember[]> {
    return this.http.get<TeamMember[]>(this.apiUrl);
  }

  getById(id: number): Observable<TeamMember> {
    return this.http.get<TeamMember>(`${this.apiUrl}/${id}`);
  }

  create(dto: CreateTeamMemberDto): Observable<TeamMember> {
    return this.http.post<TeamMember>(this.apiUrl, dto);
  }

  update(id: number, dto: UpdateTeamMemberDto): Observable<TeamMember> {
    return this.http.put<TeamMember>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
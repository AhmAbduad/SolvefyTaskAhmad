import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import {
  ApprovalRequest,
  ApprovalDecision,
  CreateApprovalRequest
} from '../models/approval';

@Injectable({
  providedIn: 'root'
})
export class ApprovalService {
  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/Approvals`;

  // GET: api/Approvals
  getAll(): Observable<ApprovalRequest[]> {
    return this.http.get<ApprovalRequest[]>(this.apiUrl);
  }

  // GET: api/Approvals/{id}
  getById(id: number): Observable<ApprovalRequest> {
    return this.http.get<ApprovalRequest>(`${this.apiUrl}/${id}`);
  }

  // POST: api/Approvals
  create(
    request: CreateApprovalRequest
  ): Observable<ApprovalRequest> {
    return this.http.post<ApprovalRequest>(this.apiUrl, request);
  }

  // PUT: api/Approvals/{id}/approve
  approve(
    id: number,
    comments: string
  ): Observable<ApprovalRequest> {
    const body: ApprovalDecision = {
      comments: comments.trim() || null
    };

    return this.http.put<ApprovalRequest>(
      `${this.apiUrl}/${id}/approve`,
      body
    );
  }

  // PUT: api/Approvals/{id}/reject
  reject(
    id: number,
    comments: string
  ): Observable<ApprovalRequest> {
    const body: ApprovalDecision = {
      comments: comments.trim() || null
    };

    return this.http.put<ApprovalRequest>(
      `${this.apiUrl}/${id}/reject`,
      body
    );
  }
}
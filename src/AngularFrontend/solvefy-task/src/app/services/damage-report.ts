
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import {
  DamageReport,
  CreateDamageReportRequest
} from '../models/damage-report';

@Injectable({
  providedIn: 'root'
})
export class DamageReportService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/DamageReports`;

  getAll(filters?: {
    search?: string;
    status?: string;
    startDate?: string;
    endDate?: string;
  }): Observable<DamageReport[]> {
    let params = new HttpParams();

    if (filters?.search?.trim()) {
      params = params.set('search', filters.search.trim());
    }

    if (filters?.status && filters.status !== 'All Status') {
      params = params.set('status', filters.status);
    }

    if (filters?.startDate) {
      params = params.set('startDate', filters.startDate);
    }

    if (filters?.endDate) {
      params = params.set('endDate', filters.endDate);
    }

    return this.http.get<DamageReport[]>(this.apiUrl, { params });
  }

  getById(id: number): Observable<DamageReport> {
    return this.http.get<DamageReport>(`${this.apiUrl}/${id}`);
  }

  create(
    request: CreateDamageReportRequest
  ): Observable<DamageReport> {
    return this.http.post<DamageReport>(this.apiUrl, request);
  }

  resolve(id: number): Observable<unknown> {
    return this.http.put(`${this.apiUrl}/${id}/resolve`, {});
  }
}

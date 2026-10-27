
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { EquipmentHistoryRecord } from '../models/equipment-history';

@Injectable({
  providedIn: 'root'
})
export class EquipmentHistoryService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/EquipmentHistory`;

  getAll(
    startDate?: string,
    endDate?: string
  ): Observable<EquipmentHistoryRecord[]> {
    let params = new HttpParams();

    if (startDate) {
      params = params.set('startDate', startDate);
    }

    if (endDate) {
      params = params.set('endDate', endDate);
    }

    return this.http.get<EquipmentHistoryRecord[]>(
      this.apiUrl,
      { params }
    );
  }

  getById(id: number): Observable<EquipmentHistoryRecord> {
    return this.http.get<EquipmentHistoryRecord>(
      `${this.apiUrl}/${id}`
    );
  }
}

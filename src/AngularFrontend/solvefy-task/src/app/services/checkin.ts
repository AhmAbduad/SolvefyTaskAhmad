
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import {
  CheckInEquipment,
  CheckInRecord,
  CreateCheckInRequest
} from '../models/checkin';

@Injectable({
  providedIn: 'root'
})
export class CheckInService {
  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/CheckIn`;

  // API 1: GET /api/CheckIn/equipment
  getEquipment(): Observable<CheckInEquipment[]> {
    return this.http.get<CheckInEquipment[]>(
      `${this.apiUrl}/equipment`
    );
  }

  // API 2: GET /api/CheckIn/equipment/{equipmentId}
  getEquipmentById(
    equipmentId: number
  ): Observable<CheckInEquipment> {
    return this.http.get<CheckInEquipment>(
      `${this.apiUrl}/equipment/${equipmentId}`
    );
  }

  // API 3: POST /api/CheckIn
  createCheckIn(
    request: CreateCheckInRequest
  ): Observable<CheckInRecord> {
    return this.http.post<CheckInRecord>(
      this.apiUrl,
      request
    );
  }
}

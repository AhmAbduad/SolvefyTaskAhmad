
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

import {
  Equipment,
  CreateEquipmentRequest
} from '../models/equipment';

@Injectable({
  providedIn: 'root',
})
export class EquipmentService {
  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/Equipment`;

  // API 1: Get all equipment
  getAllEquipment(): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(this.apiUrl);
  }

  // API 2: Insert equipment
  createEquipment(
    equipment: CreateEquipmentRequest
  ): Observable<Equipment> {
    return this.http.post<Equipment>(
      this.apiUrl,
      equipment
    );
  }
}

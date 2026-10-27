
import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  MyEquipment,
  ReturnEquipmentResponse
} from '../models/my-equipment';

@Injectable({
  providedIn: 'root'
})
export class MyEquipmentService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/MyEquipment`;

  getMyEquipment(assignedTo: string): Observable<MyEquipment[]> {
    return this.http.get<MyEquipment[]>(this.apiUrl, {
      params: { assignedTo }
    });
  }

  getById(id: number): Observable<MyEquipment> {
    return this.http.get<MyEquipment>(`${this.apiUrl}/${id}`);
  }

  returnEquipment(id: number): Observable<ReturnEquipmentResponse> {
    return this.http.put<ReturnEquipmentResponse>(
      `${this.apiUrl}/${id}/return`,
      {}
    );
  }
}

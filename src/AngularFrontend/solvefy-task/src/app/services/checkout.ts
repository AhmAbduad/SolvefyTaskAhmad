import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import {
  CheckoutRecord,
  CreateCheckoutRequest,
  EquipmentOption
} from '../models/checkout';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/Checkout`;

  // GET: /api/Checkout/equipment
  getEquipmentOptions(): Observable<EquipmentOption[]> {
    return this.http.get<EquipmentOption[]>(
      `${this.apiUrl}/equipment`
    );
  }

  // GET: /api/Checkout/assigned-to/{equipmentId}
  getAssignedToByEquipmentId(
  equipmentId: number
): Observable<string> {
  return this.http.get(
    `${this.apiUrl}/assigned-to/${equipmentId}`,
    { responseType: 'text' }
  );
}

  // POST: /api/Checkout
  createCheckout(
    request: CreateCheckoutRequest
  ): Observable<CheckoutRecord> {
    return this.http.post<CheckoutRecord>(
      this.apiUrl,
      request
    );
  }
}
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private baseUrl = 'http://localhost:5248/api/RestaurantOrders';

  constructor(private http: HttpClient) {}

  getOrdersByRestaurant(restaurantId: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}/by-restaurant/${restaurantId}`);
  }

  // ✅ New method to update order status
  updateStatus(orderId: number, status: string): Observable<any> {
  return this.http.put(`${this.baseUrl}/${orderId}/status`, { newStatus: status });
}

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MenuItem } from './menu-item';


@Injectable({
  providedIn: 'root'
})
export class MenuItemService {
  private baseUrl = 'http://localhost:5248/api/MenuItems';

  constructor(private http: HttpClient) {}

  // GET: fetch all items for a restaurant
  getByRestaurant(restaurantId: number): Observable<MenuItem[]> {
    return this.http.get<MenuItem[]>(`${this.baseUrl}/by-restaurant/${restaurantId}`);
  }

  // GET: single menu item by ID
  getById(id: number): Observable<MenuItem> {
    return this.http.get<MenuItem>(`${this.baseUrl}/${id}`);
  }

  // POST: create new menu item
  add(item: MenuItem): Observable<any> {
    return this.http.post(this.baseUrl, item);
  }

  // PUT: update existing item
  update(id: number, item: MenuItem): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, item);
  }

  // DELETE: remove item
  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}

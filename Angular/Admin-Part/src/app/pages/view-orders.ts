import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';  
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-view-orders',
  imports: [CommonModule, FormsModule, RouterModule],  
  templateUrl: './view-orders.html',
  styleUrls: ['./view-orders.scss']
})
export class ViewOrdersComponent implements OnInit {
  orders: any[] = [];
  baseUrl = 'http://localhost:5248/api/Admin/orders';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any[]>(this.baseUrl).subscribe(data => {
      this.orders = data;
    });
  }

  updateOrderStatus(orderId: number, status: string): void {
    const updatedOrder = { status };

    this.http.put(`${this.baseUrl}/${orderId}`, updatedOrder).subscribe({
      next: res => console.log('Updated:', res),
      error: err => console.error('Update failed:', err)
    });
  }

  deleteOrder(orderId: number): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.http.delete(`${this.baseUrl}/${orderId}`).subscribe(() => {
        this.orders = this.orders.filter(o => o.orderId !== orderId);
        alert('Order deleted successfully.');
      });
    }
  }
}

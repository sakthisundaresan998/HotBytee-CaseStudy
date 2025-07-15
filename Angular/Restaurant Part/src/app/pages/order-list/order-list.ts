import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // ✅ Import FormsModule for [(ngModel)]
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, FormsModule], // ✅ Add FormsModule here
  templateUrl: './order-list.html',
  styleUrls: ['./order-list.scss']
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    const restaurantId = localStorage.getItem('restaurantId');
    console.log('Restaurant ID:', restaurantId);
    if (restaurantId) {
      this.orderService.getOrdersByRestaurant(+restaurantId).subscribe(data => {
        this.orders = data;
      });
    }
  }

  updateStatus(order: Order): void {
  this.orderService.updateStatus(order.orderId, order.status).subscribe({
    next: () => {
      // ✅ Show success alert here:
      alert(`Order #${order.orderId} status updated to ${order.status}`);
    },
    error: (err) => {
      console.error('Error updating status:', err);
      alert('Failed to update order status');
    }
  });
}

}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-restaurant-dashboard',
  templateUrl: './restaurant-dashboard.html',
  styleUrls: ['./restaurant-dashboard.scss'],
  imports: [CommonModule]
})
export class RestaurantDashboardComponent implements OnInit {
  restaurantId: string | null = '';
  restaurantName: string | null = '';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.restaurantId = localStorage.getItem('restaurantId');
    this.restaurantName = localStorage.getItem('restaurantName');
  }

  goToMenu(): void {
    this.router.navigate(['/menu-items']);
  }

  goToOrders(): void {
    this.router.navigate(['/orders']);
  }
}

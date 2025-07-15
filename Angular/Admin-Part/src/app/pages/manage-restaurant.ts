import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-manage-restaurant',
  imports: [CommonModule, RouterModule,FormsModule ],
  templateUrl: './manage-restaurant.html',
  styleUrls: ['./manage-restaurant.scss']
})
export class ManageRestaurantComponent implements OnInit {
  restaurants: any[] = [];
  baseUrl = 'http://localhost:5248/api/Admin/restaurants';

  showForm = false;

  newRestaurant = {
    name: '',
    address: '',
    contactEmail: '',
    contactPhone: '',
    description: ''
  };

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchRestaurants();
  }

  fetchRestaurants(): void {
    this.http.get<any[]>(this.baseUrl).subscribe(data => {
      this.restaurants = data;
    });
  }

  deleteRestaurant(id: number): void {
    if (confirm('Are you sure you want to delete this restaurant?')) {
      this.http.delete(`${this.baseUrl}/${id}`).subscribe(() => {
        this.restaurants = this.restaurants.filter(r => r.restaurantId !== id);
      });
    }
  }

  addRestaurant(): void {
    this.http.post(this.baseUrl, this.newRestaurant).subscribe({
      next: () => {
        alert('Restaurant added successfully!');
        this.showForm = false;
        this.newRestaurant = {
          name: '',
          address: '',
          contactEmail: '',
          contactPhone: '',
          description: ''
        };
        this.fetchRestaurants(); // refresh the list
      },
      error: () => {
        alert('Failed to add restaurant.');
      }
    });
  }
}

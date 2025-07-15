import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';


@Component({
  standalone: true,
  selector: 'app-restaurant-login',
  templateUrl: './restaurant-login.html',
  styleUrls: ['./restaurant-login.scss'],
  imports: [CommonModule, FormsModule,RouterModule]
})
export class RestaurantLoginComponent {
  form = {
    contactEmail: '',
    password: ''
  };

  errorMessage = '';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    this.http.post<any>('http://localhost:5248/api/RestaurantAuth/login', this.form)
      .subscribe({
        next: (res) => {
          alert(res.message);
          localStorage.setItem('token', res.token);
          localStorage.setItem('restaurantId', res.restaurantId);
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          this.errorMessage = err.error || 'Login failed';
        }
      });
  }
}

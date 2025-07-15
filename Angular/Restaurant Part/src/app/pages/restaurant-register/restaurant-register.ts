import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';


@Component({
  standalone: true,
  selector: 'app-restaurant-register',
  templateUrl: './restaurant-register.html',
  styleUrls: ['./restaurant-register.scss'],
  imports: [CommonModule, FormsModule,RouterModule]
})
export class RestaurantRegisterComponent {
  form = {
    name: '',
    address: '',
    contactEmail: '',
    contactPhone: '',
    description: '',
    password: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  register() {
    this.http.post('http://localhost:5248/api/RestaurantAuth/register', this.form)
      .subscribe({
        next: () => {
          alert('Registered successfully!');
          this.router.navigate(['/restaurant-login']);
        },
        error: err => {
          alert(err.error || 'Registration failed.');
        }
      });
  }
}

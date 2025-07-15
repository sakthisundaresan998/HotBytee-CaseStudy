import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  constructor(private router: Router) {}

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/admin-login']);
  }
}
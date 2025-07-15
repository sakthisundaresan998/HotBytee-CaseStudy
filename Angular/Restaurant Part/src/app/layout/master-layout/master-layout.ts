import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-master-layout',
  imports: [CommonModule, RouterModule],
  templateUrl: './master-layout.html',
  styleUrls: ['./master-layout.scss']
})
export class MasterLayoutComponent {
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/restaurant-login']);
  }

  constructor(private router: Router) {}
}

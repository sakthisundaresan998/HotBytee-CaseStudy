import { Routes } from '@angular/router';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard';
import { ManageRestaurantComponent } from './pages/manage-restaurant';
import { ViewMenuComponent } from './pages/view-menu';
import { ViewOrdersComponent } from './pages/view-orders';
import { AdminLoginComponent } from './pages/admin-login/admin-login';

export const routes: Routes = [
  { path: '', redirectTo: 'admin-login', pathMatch: 'full' },
  { path: 'admin-login', component: AdminLoginComponent },
  { path: 'dashboard', component: AdminDashboardComponent },
  { path: 'restaurants', component: ManageRestaurantComponent },
  { path: 'menus', component: ViewMenuComponent },
  { path: 'orders', component: ViewOrdersComponent }
];

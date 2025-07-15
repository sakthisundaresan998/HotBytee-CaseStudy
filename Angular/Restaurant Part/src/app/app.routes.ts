import { Routes } from '@angular/router';
import { MasterLayoutComponent } from './layout/master-layout/master-layout';
import { RestaurantLoginComponent } from './pages/restaurant-login/restaurant-login';
import { RestaurantRegisterComponent } from './pages/restaurant-register/restaurant-register';
import { RestaurantDashboardComponent } from './pages/restaurant-dashboard/restaurant-dashboard';
import { MenuItemListComponent } from './pages/menu-item-list/menu-item-list';
import { MenuItemFormComponent } from './pages/menu-item-form/menu-item-form';
import { OrderListComponent } from './pages/order-list/order-list'; 

export const routes: Routes = [
  {
    path: '',
    component: MasterLayoutComponent,
    children: [
      { path: '', redirectTo: 'restaurant-login', pathMatch: 'full' },
      { path: 'restaurant-login', component: RestaurantLoginComponent },
      { path: 'restaurant-register', component: RestaurantRegisterComponent },
      { path: 'dashboard', component: RestaurantDashboardComponent },
      { path: 'menu-items', component: MenuItemListComponent },
      { path: 'menu-items/add', component: MenuItemFormComponent },
      { path: 'menu-items/edit/:id', component: MenuItemFormComponent },
      { path: 'orders', component: OrderListComponent }
    ]
  }
];

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItem } from '../../services/menu-item';
import { MenuItemService } from '../../services/menu-item.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu-item-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './menu-item-list.html',
  styleUrls: ['./menu-item-list.scss']
})
export class MenuItemListComponent implements OnInit {
  items: MenuItem[] = [];

  constructor(
    private menuItemService: MenuItemService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const restaurantId = localStorage.getItem('restaurantId');
    if (restaurantId) {
      this.menuItemService.getByRestaurant(+restaurantId).subscribe((data: MenuItem[]) => {
        this.items = data;
      });
    }
  }

  addMenuItem(): void {
    this.router.navigate(['/menu-items/add']);
  }

  editItem(id: number): void {
    this.router.navigate(['/menu-items/edit', id]);
  }

  deleteItem(id: number): void {
    if (confirm('Are you sure you want to delete this item?')) {
      this.menuItemService.delete(id).subscribe(() => {
        this.items = this.items.filter(item => item.menuItemId !== id);
      });
    }
  }
}

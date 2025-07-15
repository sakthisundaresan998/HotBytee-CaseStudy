import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuItemService } from '../../services/menu-item.service';
import { MenuItem } from '../../services/menu-item';

@Component({
  standalone: true,
  selector: 'app-menu-item-form',
  templateUrl: './menu-item-form.html',
  styleUrls: ['./menu-item-form.scss'],
  imports: [CommonModule, FormsModule]
})
export class MenuItemFormComponent implements OnInit {
  form: MenuItem = {
    name: '',
    category: '',
    price: 0,
    dietaryInfo: '',
    description: '',
    tasteInfo: '',
    nutritionalInfo: '',
    imageUrl: '',
    isAvailable: true,
    restaurantId: 0
  };

  isEdit = false;

  constructor(
    private service: MenuItemService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    const restaurantId = localStorage.getItem('restaurantId');

    if (restaurantId) {
      this.form.restaurantId = +restaurantId;
    }

    if (id) {
      this.isEdit = true;
      this.service.getById(+id).subscribe(data => {
        this.form = data;
      });
    }
  }

  save(): void {
    if (this.isEdit && this.form.menuItemId) {
      this.service.update(this.form.menuItemId, this.form).subscribe(() => {
        this.router.navigate(['/menu-items']);
      });
    } else {
      this.service.add(this.form).subscribe(() => {
        this.router.navigate(['/menu-items']);
      });
    }
  }
}

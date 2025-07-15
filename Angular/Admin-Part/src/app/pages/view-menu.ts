import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-view-menu',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './view-menu.html',
  styleUrls: ['./view-menu.scss']
})
export class ViewMenuComponent implements OnInit {
  menuItems: any[] = [];
  baseUrl = 'http://localhost:5248/api/Admin/menus';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any[]>(this.baseUrl).subscribe(data => {
      this.menuItems = data;
    });
  }

}
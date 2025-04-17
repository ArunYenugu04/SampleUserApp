import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule], 
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {
  users: User[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    console.log(' UserListComponent loaded');
    this.userService.getUsers().subscribe({
      next: (users) => {
        this.users = users;
        console.log(' Users loaded:', users);
      },
      error: (err) => {
        console.error(' Failed to load users:', err);
      }
    });
  }
}

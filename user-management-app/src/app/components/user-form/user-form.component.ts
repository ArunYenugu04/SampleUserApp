import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
  standalone: true,
  imports: [FormsModule], 
})
export class UserFormComponent implements OnInit {
  user: User = {
    id: 0,
    firstname: '',
    lastname: '',
    email: '',
    birthdate: '',
    address: {
      streetAddress: '',
      city: '',
      state: '',
      postalCode: ''
    },
    telephones: {
      phoneNumber: '',
      type: ''
    },
    userInstitutions: []
  };

  isEditMode = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    
    if (id && id !== 0) {
      this.isEditMode = true;
      this.userService.getUserById(id).subscribe(data => {
        this.user = data;
      });
    } else {
      this.isEditMode = false; // Treat id=0 as Add Mode
    }
  }

  onSubmit(form: NgForm): void {
    if (this.isEditMode) {
      this.userService.updateUser(this.user.id, this.user).subscribe(() => {
        alert('User updated!');
        this.router.navigate(['/']);
      });
    } else {
      this.userService.addUser(this.user).subscribe(() => {
        alert('User created!');
        this.router.navigate(['/']);
      });
    }
  }
}

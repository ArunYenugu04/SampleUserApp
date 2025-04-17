import { Component } from '@angular/core';
import { UserListComponent } from './components/user-list/user-list.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  // standalone: true,
  imports: [UserListComponent,RouterModule],  // ✅ Don’t import HttpClientModule here!
  templateUrl: './app.component.html'
})
export class AppComponent {}

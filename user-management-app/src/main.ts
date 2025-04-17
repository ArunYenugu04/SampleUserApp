import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { routes } from './app/app.routes';
import { UserListComponent } from './app/components/user-list/user-list.component';
import { UserFormComponent } from './app/components/user-form/user-form.component';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),        
    provideRouter([
      { path: '', component: UserListComponent },
      { path: 'edit/:id', component: UserFormComponent },
    ])     
  ]
}).catch(err => console.error(err));

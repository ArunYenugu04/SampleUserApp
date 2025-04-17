import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './components/user-list/user-list.component';

const routes: Routes = [
  { path: '', component: UserListComponent },
  {
    path: 'edit/:id',
    loadComponent: () =>
      import('./components/user-form/user-form.component').then(
        (m) => m.UserFormComponent
      )
  },
  {
    path: 'add',
    loadComponent: () =>
      import('./components/user-form/user-form.component').then(
        (m) => m.UserFormComponent
      )
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

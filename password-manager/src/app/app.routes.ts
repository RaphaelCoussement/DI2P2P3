import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationListComponent } from './components/applications/application-list.component';
import { PasswordListComponent } from './components/passwords/password-list.component';

// Définition des routes
export const routes: Routes = [
  { path: 'applications', component: ApplicationListComponent },
  { path: 'passwords', component: PasswordListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationListComponent } from './components/applications/application-list.component';
import { PasswordListComponent } from './components/passwords/password-list.component';
import { PasswordDetailComponent } from './components/passwords/password-details.component';
import { ApplicationDetailsComponent } from './components/applications/application-details.component';

// Définition des routes
export const routes: Routes = [
  { path: 'applications', component: ApplicationListComponent },
  { path: 'applications/:id', component: ApplicationDetailsComponent },
  { path: 'passwords', component: PasswordListComponent },
  { path: 'passwords/:id', component: PasswordDetailComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

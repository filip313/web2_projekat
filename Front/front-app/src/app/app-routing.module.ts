import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { DostavljaciComponent } from './admin/dostavljaci/dostavljaci.component';
import { ChangeComponent } from './user/change/change.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children:
    [
      { path: 'login', component: LoginComponent},
      { path: 'register', component: RegisterComponent},
      { path: 'change', component: ChangeComponent}
    ]
  },
  {
    path: 'admin', component: AdminComponent,
    children:
    [
      { path: 'dostavljaci', component:DostavljaciComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

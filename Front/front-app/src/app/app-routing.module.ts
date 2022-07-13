import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPorudzbineComponent } from './admin/admin-porudzbine/admin-porudzbine.component';
import { AdminComponent } from './admin/admin.component';
import { DostavljaciComponent } from './admin/dostavljaci/dostavljaci.component';
import { PorudzbinaComponent } from './porudzbina/porudzbina.component';
import { UserPorudzbineComponent } from './porudzbina/user-porudzbine/user-porudzbine.component';
import { ProizvodComponent } from './proizvod/proizvod.component';
import { ChangeComponent } from './user/change/change.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { UserComponent } from './user/user.component';
import { NovaPorudzbinaComponent } from './porudzbina/nova-porudzbina/nova-porudzbina.component';
import { TrenutnaPorudzbinaComponent } from './porudzbina/trenutna-porudzbina/trenutna-porudzbina.component';

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
      { path: 'dostavljaci', component:DostavljaciComponent},
      { path: 'porudzbine', component:AdminPorudzbineComponent}
    ]
  },
  {
    path:'proizvodi', component: ProizvodComponent
  },
  {
    path:'porudzbina', component:PorudzbinaComponent,
    children:
    [
      { path: 'zavrsene', component:UserPorudzbineComponent},
      { path: 'new', component:NovaPorudzbinaComponent},
      { path: 'trenutna', component:TrenutnaPorudzbinaComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

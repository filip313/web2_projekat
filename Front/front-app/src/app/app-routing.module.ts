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
import { TrenutnaPorudzbinaComponent } from './porudzbina/trenutna-porudzbina/trenutna-porudzbina.component';
import { NapraviPorudzbinuComponent } from './porudzbina/napravi-porudzbinu/napravi-porudzbinu.component';
import { NovePorudzbineComponent } from './porudzbina/nove-porudzbine/nove-porudzbine.component';
import { AdminGuardGuard } from './auth/guards/admin-guard.guard';
import { PotrosacGuard } from './auth/guards/potrosac.guard';
import { environment } from 'src/environments/environment';
import { AdminPotrosacGuard } from './auth/guards/admin-potrosac.guard';
import { PotrosacDostavljacGuard } from './auth/guards/potrosac-dostavljac.guard';
import { DostavljacGuard } from './auth/guards/dostavljac.guard';
import { PorudzbineMapaComponent } from './porudzbina/nove-porudzbine/porudzbine-mapa/porudzbine-mapa.component';

const routes: Routes = [
  {
    path: '', redirectTo: '/user/login', pathMatch:'full'
  },
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
    path: 'admin', component: AdminComponent, canActivate:[AdminGuardGuard], 
    children:
    [
      { path: 'dostavljaci', component:DostavljaciComponent, },
      { path: 'porudzbine', component:AdminPorudzbineComponent,}
    ]
  },
  {
    path:'proizvodi', component: ProizvodComponent, canActivate:[AdminPotrosacGuard], 
  },
  {
    path:'porudzbina', component:PorudzbinaComponent, canActivate:[PotrosacDostavljacGuard],
    children:
    [
      { path: 'zavrsene', component:UserPorudzbineComponent, canActivate:[PotrosacDostavljacGuard]},
      { path: 'poruci', component:NapraviPorudzbinuComponent, canActivate:[PotrosacGuard]},
      { path: 'trenutna', component:TrenutnaPorudzbinaComponent, canActivate:[PotrosacDostavljacGuard]},
      { path: 'nove', component:NovePorudzbineComponent , canActivate:[DostavljacGuard]},
      { path: 'mapa', component:PorudzbineMapaComponent, canActivate: [DostavljacGuard]},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

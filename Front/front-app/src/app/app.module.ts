import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, NgControl, ReactiveFormsModule } from '@angular/forms';
import { UserService } from './shared/services/user.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule} from '@angular/material/chips';
import { RegisterComponent } from './user/register/register.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import{ MatSnackBarModule } from '@angular/material/snack-bar';

import { ToastrModule } from 'ngx-toastr';
import { ChangeComponent } from './user/change/change.component';
import { InterceptorService } from './auth/interceptor.service';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { PorudzbinaComponent } from './porudzbina/porudzbina.component';
import { AdminComponent } from './admin/admin.component';
import { DostavljaciComponent } from './admin/dostavljaci/dostavljaci.component';
import { ProizvodService } from './shared/services/proizvod.service';
import { ProizvodComponent } from './proizvod/proizvod.component';
import { AddProizvodComponent } from './proizvod/add-proizvod/add-proizvod.component';
import { AdminPorudzbineComponent } from './admin/admin-porudzbine/admin-porudzbine.component';
import { UserPorudzbineComponent } from './porudzbina/user-porudzbine/user-porudzbine.component';
import { TrenutnaPorudzbinaComponent } from './porudzbina/trenutna-porudzbina/trenutna-porudzbina.component';
import { KorpaItemComponent } from './proizvod/korpa-item/korpa-item.component';
import { NapraviPorudzbinuComponent } from './porudzbina/napravi-porudzbinu/napravi-porudzbinu.component';
import { NovePorudzbineComponent } from './porudzbina/nove-porudzbine/nove-porudzbine.component';
import { AdminGuardGuard } from './auth/guards/admin-guard.guard';
import { PotrosacGuard } from './auth/guards/potrosac.guard';
import { AdminPotrosacGuard } from './auth/guards/admin-potrosac.guard';
import { PotrosacDostavljacGuard } from './auth/guards/potrosac-dostavljac.guard';
import { AuthService } from './auth/auth.service';
import { NavbarComponent } from './navbar/navbar.component';

import { OAuthModule } from 'angular-oauth2-oidc';
import { PotvrdiPorudzbinuComponent } from './porudzbina/napravi-porudzbinu/potvrdi-porudzbinu/potvrdi-porudzbinu.component';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    LoginComponent,
    RegisterComponent,
    ChangeComponent,
    PorudzbinaComponent,
    AdminComponent,
    DostavljaciComponent,
    ProizvodComponent,
    AddProizvodComponent,
    AdminPorudzbineComponent,
    UserPorudzbineComponent,
    NapraviPorudzbinuComponent,
    TrenutnaPorudzbinaComponent,
    KorpaItemComponent,
    NovePorudzbineComponent,
    NavbarComponent,
    PotvrdiPorudzbinuComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatListModule,
    MatTableModule,
    MatDialogModule,
    MatCardModule,
    MatIconModule,
    MatGridListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatSnackBarModule,
    ToastrModule.forRoot({
      progressBar : true
    }),
    OAuthModule.forRoot() 
  ],
  providers: [
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true,
    },
    { provide:JWT_OPTIONS, useValue:JWT_OPTIONS},
    JwtHelperService,
    ProizvodService,
    AuthService,
    AdminGuardGuard,
    PotrosacGuard,
    AdminPotrosacGuard,
    PotrosacDostavljacGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

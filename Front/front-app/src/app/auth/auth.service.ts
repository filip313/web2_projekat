import { SocialAuthService } from '@abacritt/angularx-social-login';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwt:JwtHelperService, private authService: SocialAuthService) { }

  isUserLoggedIn():boolean{
    let token = sessionStorage.getItem('token');
    if(token != null){
      return true;
    }
    return false;
  }

  getUserId():number{
    let token = sessionStorage.getItem('token');
    if(token){
      let decoded = this.jwt.decodeToken(token);
      return decoded.id;
    }

    return -1;
  }

  isUserAdmin():boolean{
    let token = sessionStorage.getItem('token');
    if(token){
      let decoded = this.jwt.decodeToken(token);
      
      return 'Admin' === decoded[environment.userRoleKey];
    }
    return false;
  }

  isUserPotrosac():boolean{
    let token = sessionStorage.getItem('token');
    if(token){
      let decoded = this.jwt.decodeToken(token);
      
      return 'Potrosac' === decoded[environment.userRoleKey];
    }
    return false;
  }

  isUserDostavljac():boolean{
    let token = sessionStorage.getItem('token');
    if(token){
      let decoded = this.jwt.decodeToken(token);
      
      return 'Dostavljac' === decoded[environment.userRoleKey];
    }
    return false;
  }

  logout(){
    sessionStorage.clear();
    this.authService.signOut();
    localStorage.clear();
  }
}

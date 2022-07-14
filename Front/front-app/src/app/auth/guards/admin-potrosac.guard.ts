import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminPotrosacGuard implements CanActivate {
  constructor(private auth:AuthService, private router:Router){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):boolean {
      
      if(this.auth.isUserAdmin()){
        return true;
      }
      else if(this.auth.isUserPotrosac()){
        this.router.navigateByUrl('porudzbina/create');
        return true;
      }

      this.router.navigateByUrl('user/login');
      return false;
  }

  
}

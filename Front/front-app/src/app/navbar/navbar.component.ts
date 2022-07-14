import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationError, NavigationStart, Router} from '@angular/router';
import { AuthService } from '../auth/auth.service';
import {filter, distinctUntilChanged} from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private auth:AuthService, private router:Router){
    this.router.events.subscribe(
      val => {
        if(val instanceof NavigationEnd){
          this.osvezi();
        }
      }
    )
  }

  ulogovan:boolean = false;
  admin:boolean = false;
  potrosac:boolean = false;
  dostavljac:boolean = false;

  ngOnInit(): void {
    this.ulogovan = this.auth.isUserLoggedIn();
    this.osvezi();

    
  }

  osvezi(){
    this.ulogovan = this.auth.isUserLoggedIn();
    if(this.ulogovan){
      this.admin = this.auth.isUserAdmin();
      this.potrosac = this.auth.isUserPotrosac();
      this.dostavljac = this.auth.isUserDostavljac();
    }
  }

  logout(){
    this.auth.logout();
    this.router.navigateByUrl('user/login');
  }

}

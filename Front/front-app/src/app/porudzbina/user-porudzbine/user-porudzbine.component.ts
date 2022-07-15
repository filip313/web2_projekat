import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-porudzbine',
  templateUrl: './user-porudzbine.component.html',
  styleUrls: ['./user-porudzbine.component.css']
})
export class UserPorudzbineComponent implements OnInit {

  porudzbine:Porudzbina[];
  potrosac:boolean = false;
  cols = ['id', 'user', 'adresa', 'ukCena', 'prihvat', 'trajanje']
  constructor(private servis: PorudzbinaService, private auth:AuthService,
    private snackBar: MatSnackBar, private router:Router) { }

  ngOnInit(): void {
    this.potrosac = this.auth.isUserPotrosac();
    this.servis.getUserPorudzbine(this.auth.getUserId()).subscribe(
      (data:Porudzbina[]) =>{
        let temp = []
        for(let por of data){
          if(por.status == "Dostavljena"){
            temp.push(por);
          }
        }

        this.porudzbine = temp;
      },
      error => {
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
         this.snackBar.open(error.error, "", { duration: 2000,} ); 
      });
  }
}

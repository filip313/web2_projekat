import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { NovaPorudzbina } from 'src/app/shared/models/novaporudzbina.model';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PrihvatiPorudzbinu } from 'src/app/shared/models/prihavtiporudzbinu.model';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';

@Component({
  selector: 'app-nove-porudzbine',
  templateUrl: './nove-porudzbine.component.html',
  styleUrls: ['./nove-porudzbine.component.css']
})
export class NovePorudzbineComponent implements OnInit {

  constructor(private service: PorudzbinaService, 
    private snackBar: MatSnackBar, private router:Router, private auth:AuthService) { }

  novePorudzbine:Porudzbina[];
  cols=['id', 'user', 'adresa', 'ukCena', 'komentar', 'dugme'];

  ngOnInit(): void {
    this.service.getNovePorudzbine().subscribe(
      (data:Porudzbina[]) => {
        this.novePorudzbine = data;
      },
      error => {
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
        this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    )
  }

  prihvatiPorudzbinu(porudzbinaId:number){
    let data = new PrihvatiPorudzbinu();
    data.porudzbinaId = porudzbinaId;
    
    data.dostavljacId = this.auth.getUserId();

    this.service.prihvatiPorudzbinu(data).subscribe(
      (data:Porudzbina) => {
        console.log(data);
        this.router.navigateByUrl('porudzbina/trenutna');
      },
      error => {
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
       this.snackBar.open(error.error, "", { duration: 2000,} ); 
      }
    )
  }

}

import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { AddProizvodComponent } from 'src/app/proizvod/add-proizvod/add-proizvod.component';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-trenutna-porudzbina',
  templateUrl: './trenutna-porudzbina.component.html',
  styleUrls: ['./trenutna-porudzbina.component.css']
})
export class TrenutnaPorudzbinaComponent implements OnInit {

  cekajuDostavu:any[] = [];
  trenutnaPorudzbina:any;
  cols=['naziv', 'cena','kolicina']
  dostavljac=false;
  intervalId:any;
  trajanjeSekunde:number;
  minuti:number;
  sekunde:number;

  constructor(private service: PorudzbinaService,private auth:AuthService ,
    private toastr:ToastrService, private router:Router ) { }
  ngOnInit(): void {
   
    this.dostavljac = this.auth.isUserDostavljac();
    this.service.getUserPorudzbine(this.auth.getUserId()).subscribe(
      (data:Porudzbina[]) => {
        for(let por of data){
          if(por.status == "DostavljaSe" && (this.trenutnaPorudzbina == null || this.trenutnaPorudzbina == undefined)){
            
            this.trenutnaPorudzbina = por;
            this.setTimer(por.trajanjeDostave, por.vremePrihvata);
          }
          else if(por.status == "CekaDostavu"){
              this.cekajuDostavu.push(por);
          }
        }
    });

  }

  showNovaPorudzbina(){
    this.router.navigateByUrl('porudzbina/create');
  }

  setTimer(trajanjeDostave:any, vremePrihvata:any){

    const trenutniDatum= Date.now();
    const prihvat = new Date(vremePrihvata).valueOf();
    let timeDiff = Math.abs(trenutniDatum - prihvat);
    timeDiff = (Math.round((timeDiff/1000)));
    timeDiff = trajanjeDostave.totalSeconds - timeDiff;
    console.log(timeDiff);
    if(timeDiff <= 0){
      this.service.zavrsiPorudzbinu(this.trenutnaPorudzbina.id).subscribe(
        (data:Porudzbina) => {
          this.router.navigateByUrl("porudzbina/zavrsene")
        },
        error => {
          this.router.navigateByUrl("porudzbina/zavrsene")
        }
      )
    }
    else{
      this.trajanjeSekunde = timeDiff;
      this.minuti = Math.floor(this.trajanjeSekunde/60);
      this.sekunde = this.trajanjeSekunde - this.minuti * 60;
      this.intervalId = setInterval(() => {
      this.trajanjeSekunde = this.trajanjeSekunde - 1;
      this.updateIspis();
      console.log(this.trajanjeSekunde);
        if(this.trajanjeSekunde == 0){
          clearInterval(this.intervalId);
          this.service.zavrsiPorudzbinu(this.trenutnaPorudzbina.id).subscribe(
            (data:Porudzbina) => {
              this.router.navigateByUrl("porudzbina/zavrsene")
            },
            error => {
              this.router.navigateByUrl("porudzbina/zavrsene")
            }
          );
        }
        }, 1000);
    }
  }

  updateIspis(){
    this.minuti = Math.floor(this.trajanjeSekunde/60);
    this.sekunde -= 1;
    if(this.sekunde == -1){
      this.sekunde = 59;
    }
  }
}

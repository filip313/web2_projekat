import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';

@Component({
  selector: 'app-admin-porudzbine',
  templateUrl: './admin-porudzbine.component.html',
  styleUrls: ['./admin-porudzbine.component.css']
})
export class AdminPorudzbineComponent implements OnInit {
  porudzbine:Porudzbina[];

  statusi:{[key:string]:string}= {
    "CekaDostavu":"warn",
    "DostavljaSe":"accent",
    "Dostavljena":"primary"
  };

  cols = ['id', 'narucilac', 'dostavljac', 'status'];
  constructor(private servis:PorudzbinaService, private snackBar:MatSnackBar,
    private router:Router) { }

  ngOnInit(): void {
    this.servis.getPorudzbine().subscribe(
      (data:Porudzbina[]) =>{
        for(let por of data){
          if(por.dostavljac.id == por.narucilac.id){
            por.dostavljac.username = "";
          }
        }
        this.porudzbine = data;
      },
      error => {
        if(error.status == 401){
          this.router.navigateByUrl('user/login');
        }
       this.snackBar.open(error.error, "", { duration: 2000,} ); 
      }
    )
  }

}

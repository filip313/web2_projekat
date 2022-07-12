import { Component, OnInit } from '@angular/core';
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
  constructor(private servis:PorudzbinaService, private toastr:ToastrService) { }

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
        this.toastr.error(error.error);
      }
    )
  }

}

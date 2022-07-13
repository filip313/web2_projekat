import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AddProizvodComponent } from 'src/app/proizvod/add-proizvod/add-proizvod.component';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';

@Component({
  selector: 'app-trenutna-porudzbina',
  templateUrl: './trenutna-porudzbina.component.html',
  styleUrls: ['./trenutna-porudzbina.component.css']
})
export class TrenutnaPorudzbinaComponent implements OnInit {

  trenutnaPorudzbina:any;
  constructor(private service: PorudzbinaService, private jwt:JwtHelperService,
    private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
    let token = localStorage.getItem('token');
    if(token != null){
      let decoded = this.jwt.decodeToken(token);
      this.service.getUserPorudzbine(decoded.id).subscribe(
        (data:Porudzbina[]) => {
          for(let por of data){
            if(por.status == "DostavljaSe"){
              console.log(por);
              this.trenutnaPorudzbina = por;
              return;
            }
          }
          this.trenutnaPorudzbina = null;
        }
      )
    }
  }

  showNovaPorudzbina(){
    this.router.navigateByUrl('porudzbina/new');
  }
}

import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { PorudzbinaProizvod } from '../shared/models/porudzbinaproizvod.model';
import { Proizvod } from '../shared/models/proizovd.model';
import { ProizvodService } from '../shared/services/proizvod.service';
import { AddProizvodComponent } from './add-proizvod/add-proizvod.component';
import { KorpaItemComponent } from './korpa-item/korpa-item.component';

@Component({
  selector: 'app-proizvod',
  templateUrl: './proizvod.component.html',
  styleUrls: ['./proizvod.component.css']
})
export class ProizvodComponent implements OnInit {
  admin = false;
  proizvodi:Proizvod[];
  cols=['naziv', 'cena', 'sastojci'];
  constructor(private proizvodService:ProizvodService, private formBuilder:FormBuilder,
    private toastr:ToastrService, private jwt: JwtHelperService, private dialog:MatDialog) { }

  @Output() dodatEvent = new EventEmitter<PorudzbinaProizvod>();


  ngOnInit(): void {
    let token = localStorage.getItem('token');
    let dekoded = this.jwt.decodeToken(token?token:"");
    this.admin = (dekoded[environment.userRoleKey] == "Admin")?true: false;
    if(dekoded[environment.userRoleKey] == "Potrosac"){
      this.cols.push('dodaj');
    }

    this.proizvodService.getProizvode().subscribe(
      (data:Proizvod[]) =>{
        this.proizvodi = data;
        console.log(data);
      },
      error => {
        this.toastr.error(error.error);
      }
    )
  }

  dodaj(){
    this.dialog.open(AddProizvodComponent, {width:'50%', height:'45%'}).afterClosed().subscribe( data =>{
        this.ngOnInit();
      }
    );
  }
  
  dodajUKorpu(proizvod:Proizvod){
    const dialogRef = this.dialog.open(KorpaItemComponent, {
      width:'350px',
    });
    dialogRef.afterClosed().subscribe(
      result => {
        console.log(result);
        if(result == undefined){
          return;
        }
        
        let item = new PorudzbinaProizvod();
        item.proizvod = proizvod;
        item.proizvodId = proizvod.id;
        item.kolicina = result;
        this.dodatEvent.emit(item);
      }
    )

  }
}

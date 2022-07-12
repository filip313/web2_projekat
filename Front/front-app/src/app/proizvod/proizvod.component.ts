import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Proizvod } from '../shared/models/proizovd.model';
import { ProizvodService } from '../shared/services/proizvod.service';
import { AddProizvodComponent } from './add-proizvod/add-proizvod.component';

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

  ngOnInit(): void {
    let token = localStorage.getItem('token');
    let dekoded = this.jwt.decodeToken(token?token:"");
    this.admin = (dekoded[environment.userRoleKey] == "Admin")?true: false;
    
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

}

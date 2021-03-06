import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
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
    private toastr:ToastrService, private auth:AuthService, private dialog:MatDialog, private router:Router,
    private snackBar:MatSnackBar) { }

  @Output() dodatEvent = new EventEmitter<PorudzbinaProizvod>();


  ngOnInit(): void {
    this.admin = this.auth.isUserAdmin(); 
    if(this.auth.isUserPotrosac()){
      this.cols.push('dodaj');
    }

    this.proizvodService.getProizvode().subscribe(
      (data:Proizvod[]) =>{
        this.proizvodi = data;
        console.log(data);
      },
      error => {
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
       this.snackBar.open(error.error, "", { duration: 2000,} ); 
      }
    )
  }

  dodaj(){
    this.dialog.open(AddProizvodComponent, {width:'50%', height:'45%', panelClass:'panel'}).afterClosed().subscribe( data =>{
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

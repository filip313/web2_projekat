import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { NovaPorudzbina } from 'src/app/shared/models/novaporudzbina.model';
import { PorudzbinaProizvod } from 'src/app/shared/models/porudzbinaproizvod.model';
import { environment } from 'src/environments/environment';
import { PorudzbinaService } from '../../shared/services/porudzbina.service';
import { MatDialog } from '@angular/material/dialog';
import { PotvrdiPorudzbinuComponent } from './potvrdi-porudzbinu/potvrdi-porudzbinu.component';

@Component({
  selector: 'app-napravi-porudzbinu',
  templateUrl: './napravi-porudzbinu.component.html',
  styleUrls: ['./napravi-porudzbinu.component.css']
})
export class NapraviPorudzbinuComponent implements OnInit {

  constructor(private service: PorudzbinaService, 
    private toastr:ToastrService, private formBuilder: FormBuilder, private router: Router,
    private auth:AuthService, private snackBar:MatSnackBar, private dialog:MatDialog) { 

    }
  
  korpa:PorudzbinaProizvod[] = [];
  cols = ['naziv','kol', 'cena','ukloni'];
  @ViewChild(MatTable) table : MatTable<PorudzbinaProizvod>;
  forma:FormGroup;
  dostava:number;
  payapl:object;
  nacini = ['Pouzecem', 'PayPal']
  selected = "";
  orderId:number = 0;
  

  ngOnInit(): void {
    this.forma = this.formBuilder.group({
      adresa:['',[
        Validators.required,
        Validators.minLength(10),
      ]],
      komentar:['', [

      ]],
      nacinPlacanja:['',[
        Validators.required,
      ]]
    },{
      validators:[]
    });
    this.dostava = environment.cenaDostave;
 }


  dodajUKorpu(value:PorudzbinaProizvod){
    let same = false; 
    for(let item of this.korpa){
      if(item.proizvodId == value.proizvodId){
        item.kolicina += value.kolicina;
        same = true;
        break;
      }
    }

    if(!same){
      this.korpa.push(value);
    }
    this.table.renderRows();
  }

  ukupnaCena():number{
    let ret = 0;
    for(let item of this.korpa){
      ret += (item.kolicina * item.proizvod.cena);
    }
    return ret;
  }

  ukloni(id:number){
    const index = this.korpa.findIndex( x => x.id === id);
    this.korpa.splice(index, 1);
    this.table.renderRows();
  }
  

  fillTestOrder():NovaPorudzbina{
    let data = new NovaPorudzbina();
    data.adresa = this.forma.controls['adresa'].value;
    data.komentar = this.forma.controls['komentar'].value;
    data.cena = this.ukupnaCena() + this.dostava;
    data.cenaDostave = this.dostava;
    data.proizvodi = this.korpa;
    data.narucilacId = this.auth.getUserId();
    data.nacinPlacanja = this.selected;
    return data;
  }

  testOrder(data:NovaPorudzbina){
    this.service.testPorudzbinu(data).subscribe(
      (data) => {
        this.dialog.open(PotvrdiPorudzbinuComponent, {
          data:data,
          width:'100%',
        });
      },
      (error) => {
        if(error.status === 401){
          this.router.navigateByUrl('/user/login');
        }
        this.snackBar.open(error.error, "", { duration: 2000,});
        console.log(error);
      }
    );
  }
 
  pripremiPorudzbinu(){
    if(this.korpa.length <= 0){
      this.snackBar.open("Korpa ne moze biti prazna!", "", { duration: 2000,})
      return;
    }
    let data = this.fillTestOrder();
    this.testOrder(data);
  }
}

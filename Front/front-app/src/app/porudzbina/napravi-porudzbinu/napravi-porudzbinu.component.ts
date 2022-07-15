import { validateHorizontalPosition } from '@angular/cdk/overlay';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { NovaPorudzbina } from 'src/app/shared/models/novaporudzbina.model';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaProizvod } from 'src/app/shared/models/porudzbinaproizvod.model';
import { environment } from 'src/environments/environment';
import { PorudzbinaService } from '../../shared/services/porudzbina.service';

@Component({
  selector: 'app-napravi-porudzbinu',
  templateUrl: './napravi-porudzbinu.component.html',
  styleUrls: ['./napravi-porudzbinu.component.css']
})
export class NapraviPorudzbinuComponent implements OnInit {

  constructor(private service: PorudzbinaService, 
    private toastr:ToastrService, private formBuilder: FormBuilder, private router: Router,
    private auth:AuthService, private snackBar:MatSnackBar) { }
  
  korpa:PorudzbinaProizvod[] = [];
  cols = ['naziv','kol', 'cena','ukloni'];
  @ViewChild(MatTable) table : MatTable<PorudzbinaProizvod>;
  forma:FormGroup;
  dostava:number;

  ngOnInit(): void {
    this.forma = this.formBuilder.group({
      adresa:['',[
        Validators.required,
        Validators.minLength(10),
      ]],
      komentar:['', [

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
  
  onSubmit(){
    if(this.korpa.length <= 0){
      this.snackBar.open('Korpa ne moze biti prazna!');
      return;
    }
    let data = new NovaPorudzbina();
    data.adresa = this.forma.controls['adresa'].value;
    data.komentar = this.forma.controls['komentar'].value;
    data.cena = this.ukupnaCena() + this.dostava;
    data.cenaDostave = this.dostava;
    data.proizvodi = this.korpa;
    data.narucilacId = this.auth.getUserId();
    this.service.napraviPorudzbinu(data).subscribe(
      (data:NovaPorudzbina) => {
        console.log(data);
        this.router.navigateByUrl('porudzbina/trenutna')
      },
      error =>{
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
        this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    );
  }
}

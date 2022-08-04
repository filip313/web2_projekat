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
import { loadScript } from '@paypal/paypal-js';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-napravi-porudzbinu',
  templateUrl: './napravi-porudzbinu.component.html',
  styleUrls: ['./napravi-porudzbinu.component.css']
})
export class NapraviPorudzbinuComponent implements OnInit {

  constructor(private service: PorudzbinaService, 
    private toastr:ToastrService, private formBuilder: FormBuilder, private router: Router,
    private auth:AuthService, private snackBar:MatSnackBar) { 

    }
  
  korpa:PorudzbinaProizvod[] = [];
  cols = ['naziv','kol', 'cena','ukloni'];
  @ViewChild(MatTable) table : MatTable<PorudzbinaProizvod>;
  forma:FormGroup;
  dostava:number;
  payapl:object;
  nacini = ['Pouzecem', 'PayPal']
  selected = "";

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
    loadScript({ 'client-id':'AYdJNmq5wy6-6kdLrXv7NqqTA8E_eGtKvlIc_t5rsTTwDEPBQnRPCmec7Z_63JGOshUaUKFEPh0opgoV', 'currency':'USD', 'enable-funding':'card'})
      .then(
        (paypal:any) => {
          paypal.Buttons({
            style: {
              shape: 'pill',
              color: 'blue',
              layout: 'horizontal',
              label: 'paypal',
            },
            createOrder:(data:any, actions:any) =>{
              if(this.korpa.length <= 0){
                this.snackBar.open("Korpa ne moze biti prazna!", "", { duration: 2000,});
                return  new Error();
              }
              return actions.order.create({
                
                purchase_units: [{
                  amount:{ value: this.ukupnaCena() + environment.cenaDostave}
                }]
              }).catch((error:any) =>{ this.snackBar.open("Korpa ne moze biti prazna!", "", { duration: 2000,} );});
            },
            onApprove: (data:any, actions:any) =>{
              return actions.order.capture().then((orderData:any) => {
                console.log('Capture result ', orderData.purchase_units[0].payments.captures[0]);
                const transaction = orderData.purchase_units[0].payments.captures[0];
                this.napraviPorudzbinu(transaction.id, transaction.status);
                alert(`Transaction ${transaction.status}: ${transaction.id}\n\nSee console for details`)
              }
              )}
          }).render("#paypalButton")
          .then((data:any)=>{
            console.log(paypal);
          })
          .catch(
            (error:any) => {
              console.error("Failed to redner PayPal button", error);
            });
      })
      .catch(
        (error) =>{
          console.error("failed to load the PayPal JS SDK script", error);
        });
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
    this.napraviPorudzbinu();
  }

  napraviPorudzbinu( id:string="", status:string=""){
    let data = new NovaPorudzbina();
    data.adresa = this.forma.controls['adresa'].value;
    data.komentar = this.forma.controls['komentar'].value;
    data.cena = this.ukupnaCena() + this.dostava;
    data.cenaDostave = this.dostava;
    data.proizvodi = this.korpa;
    data.narucilacId = this.auth.getUserId();
    data.payPalId = id;
    data.payPalStatus = status;
    data.nacinPlacanja = this.selected;
    console.log(data);
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

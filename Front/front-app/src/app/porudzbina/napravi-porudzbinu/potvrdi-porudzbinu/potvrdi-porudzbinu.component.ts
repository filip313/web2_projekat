import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NovaPorudzbina } from 'src/app/shared/models/novaporudzbina.model';
import { loadScript } from '@paypal/paypal-js';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-potvrdi-porudzbinu',
  templateUrl: './potvrdi-porudzbinu.component.html',
  styleUrls: ['./potvrdi-porudzbinu.component.css']
})
export class PotvrdiPorudzbinuComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<PotvrdiPorudzbinuComponent>, private snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: NovaPorudzbina, private service: PorudzbinaService,
    private router: Router) { }

  ngOnInit(): void {
    console.log(this.data);

    if (this.data.nacinPlacanja == "PayPal") {
      loadScript({ 'client-id': 'AYdJNmq5wy6-6kdLrXv7NqqTA8E_eGtKvlIc_t5rsTTwDEPBQnRPCmec7Z_63JGOshUaUKFEPh0opgoV', 'currency': 'USD', 'enable-funding': 'card', 'locale': 'en_RS' })
        .then(
          (paypal: any) => {
            paypal.Buttons({
              style: {
                shape: 'pill',
                color: 'blue',
                layout: 'horizontal',
                label: 'paypal',
                tagline: false,
              },
              createOrder: (data: any, actions: any) => {
                console.log(actions);
                return actions.order.create({
                  purchase_units: [{
                    amount: { value: this.data.cena },
                    shipping: { address: { country_code: "RS", address_line_1: this.data.adresa, postal_code: "21000", admin_area_1: "Serbia", admin_area_2: "Novi Sad" } }
                  }],
                  application_context: {
                    brand_name: "Kafana Slozna Braca",
                    payment_method: {
                      payee_preferred: "IMMEDIATE_PAYMENT_REQUIRED",
                      standard_entry_class_code: "WEB"
                    },
                    shipping_preference: "SET_PROVIDED_ADDRESS",
                    user_action: "PAY_NOW"
                  },
                });
              },
              onApprove: (data: any, actions: any) => {

                console.log('ACTIONS: ', actions);
                console.log('DATA: ', data);
                return actions.order.capture().then((orderData: any) => {
                  console.log('ORDER DATA: ', orderData);
                  console.log('Capture result ', orderData.purchase_units[0].payments.captures[0]);
                  const transaction = orderData.purchase_units[0].payments.captures[0];
                  if (transaction.status == "COMPLETED") {
                    this.napraviPorudzbinu(transaction.id, transaction.status);
                  }
                }
                )
              }
            }).render("#paypalButton")
              .then((data: any) => {
                console.log(paypal);
              })
              .catch(
                (error: any) => {
                  console.error("Failed to redner PayPal button", error);
                });
          }
        )
        .catch(
          (error) => {
            console.error("failed to load the PayPal JS SDK script", error);
          }
        );
    }
  }

  cols = ['naziv', 'kol', 'cena']

  napraviPorudzbinu( id:string="", status:string=""){
    this.data.payPalId = id;
    this.data.payPalStatus = status;
    this.service.napraviPorudzbinu(this.data).subscribe(
      (data:NovaPorudzbina) => {
        console.log(data);
        this.router.navigateByUrl('porudzbina/trenutna');
        this.dialogRef.close();
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

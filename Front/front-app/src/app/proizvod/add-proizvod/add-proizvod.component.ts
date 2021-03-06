import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Proizvod } from 'src/app/shared/models/proizovd.model';
import { ProizvodService } from 'src/app/shared/services/proizvod.service';

@Component({
  selector: 'app-add-proizvod',
  templateUrl: './add-proizvod.component.html',
  styleUrls: ['./add-proizvod.component.css']
})
export class AddProizvodComponent implements OnInit {
  form:FormGroup;
  constructor(private servis:ProizvodService, private formBuilder:FormBuilder,
    private dialogRef:MatDialogRef<AddProizvodComponent>, private router:Router,
    private snackBar:MatSnackBar) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      naziv:['',[
        Validators.required,
        Validators.minLength(3)
      ]],
      cena:['',[
        Validators.required,
        Validators.min(10)
      ]],
      sastojci:['',[
        Validators.required,
        Validators.minLength
      ]]
    });
  }
  onSubmit(){
    let data = new Proizvod();
    data.naziv = this.form.controls['naziv'].value;
    data.cena = this.form.controls['cena'].value;
    data.sastojci = this.form.controls['sastojci'].value;
    this.servis.dodajProizovd(data).subscribe(
      (data:Proizvod) => {
        this.dialogRef.close();
      },
      error =>{
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
        this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    )
  }
  get naziv(){
    return this.form.get('naziv');
  }
  get cena(){
    return this.form.get('cena');
  }
  get sastojci(){
    return this.form.get('sastojci');
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserData } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/shared/services/user.service';
import { CustomNewPasswordValidator } from 'src/app/shared/validators/password.validator';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/auth/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-change',
  templateUrl: './change.component.html',
  styleUrls: ['./change.component.css']
})
export class ChangeComponent implements OnInit {
  slika = "";
  changeForm : FormGroup = this.formBuilder.group({
    id: [''],
    username:['',[
      Validators.minLength(3)
    ]],
    email:['',[
      Validators.email
    ]],
    stariPassword:['',[
      Validators.required,
    ]],
    noviPassword:[''],
    noviPasswordConfirm:[''],
    ime:[''],
    prezime:[''],
    datumRodjenja:[''],
    adresa:['',[
    ]],
    file:['',[
    ]]
  });

  constructor(private userService: UserService,private router: Router,
    private formBuilder: FormBuilder, private auth:AuthService,  private toastr: ToastrService,
    private snackBar:MatSnackBar) { }
  file:File;
  ngOnInit(): void {
    this.userService.getUserData(this.auth.getUserId()).subscribe(
      (data:UserData) =>{
        console.log(data);
        this.changeForm.get('id')?.setValue(data.id.toString());
        this.changeForm.get('username')?.setValue(data.username);
        this.changeForm.get('username')?.disable();
        this.changeForm.get('email')?.setValue(data.email);
        this.changeForm.get('ime')?.setValue(data.ime);
        this.changeForm.get('prezime')?.setValue(data.prezime);
        this.changeForm.get('datumRodjenja')?.setValue(data.datumRodjenja.toString());
        this.changeForm.get('adresa')?.setValue(data.adresa);
        this.slika = environment.imageDisplayPrefix + data.slika;
      },
      error =>{
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
        this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    );
  }

  get username(){
    return this.changeForm.get('username');
  }
  get email(){
    return this.changeForm.get('email');
  }
  get password(){
    return this.changeForm.get('stariPassword');
  }
  get datumRodjenja(){
    return this.changeForm.get('datumRodjenja');
  }

  onFileSelected(event:any){
    this.file = event.target.files[0];
    let reader = new FileReader();
    reader.onloadend = (e) => {
      console.log(reader.result);
      this.slika = reader.result as string;
    }
    reader.readAsDataURL(this.file);
  }
  
  onSubmit(){
    let formData = new FormData()
    formData.append("Id", this.changeForm.controls["id"].value);
    formData.append("Username", this.changeForm.controls["username"].value);
    formData.append("Email", this.changeForm.controls["email"].value);
    formData.append("StariPassword", this.changeForm.controls["stariPassword"].value);
    formData.append("NoviPassword", this.changeForm.controls["noviPassword"].value);
    formData.append("Ime", this.changeForm.controls["ime"].value);
    formData.append("Prezime", this.changeForm.controls["prezime"].value);
    formData.append("DatumRodjenja", JSON.stringify(this.changeForm.controls["datumRodjenja"].value).substring(1,11));
    formData.append("Adresa", this.changeForm.controls["adresa"].value);
    formData.append("File", this.file);

    this.userService.changeUserData(formData).subscribe(
      (data:UserData) =>{
        this.ngOnInit();
      },
      error =>{
        this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    );

  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Register } from 'src/app/shared/models/register.model';
import { UserService } from 'src/app/shared/services/user.service';
import { CustomValidacijaRodjenja } from 'src/app/shared/validators/datum.validator';
import { CustomPasswordValidator } from 'src/app/shared/validators/password.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  file:File;
  slika:string;
  constructor(private service: UserService, private formBuilder:FormBuilder,
    private snackBar:MatSnackBar) { }
  
  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      username: ['',[
        Validators.required,
        Validators.minLength(3),
      ]],
      email: ['',[
        Validators.required,
        Validators.email
      ]],
      password: ['',[
        Validators.required,
        Validators.minLength(8)
      ]],
      passwordConfirm: ['', [
        Validators.required,
        Validators.minLength(8)
      ]],
      ime:['',[
        Validators.required,
      ]],
      prezime:['',[
        Validators.required
      ]],
      datumRodjenja:['',[
        Validators.required,
        CustomValidacijaRodjenja
      ]],
      adresa:['',[
        Validators.required,
        Validators.minLength(10)
      ]],
      userType:['',[
        Validators.required
      ]],
      file:['',[]]
      
    },
    {
      validators:[CustomPasswordValidator]
    });
  }

  onFileSelected(event:any){
    this.registerForm.controls['file'] = event.target.files;
    this.file = event.target.files[0];
    let reader = new FileReader();
    reader.onloadend = (e) => {
      console.log(reader.result);
      this.slika = reader.result as string;
    }
    reader.readAsDataURL(this.file);
  }

  onSubmit(){
      console.log(this.registerForm);
      let registerData = new FormData();
      registerData.append('Username', this.registerForm.controls['username'].value);
      registerData.append('Password', this.registerForm.controls['password'].value);
      registerData.append('Email', this.registerForm.controls['email'].value);
      registerData.append('Ime', this.registerForm.controls['ime'].value);
      registerData.append('Prezime', this.registerForm.controls['prezime'].value);
      registerData.append('DatumRodjenja' , JSON.stringify(this.registerForm.controls['datumRodjenja'].value).substring(1,11));
      registerData.append('UserType' ,this.registerForm.controls['userType'].value);
      registerData.append('File',this.file);
      registerData.append('Adresa',this.registerForm.controls['adresa'].value);

    this.service.register(registerData).subscribe(
    (data:Register) =>{
      console.log(data);
    },
    error =>{
      this.snackBar.open(error.error, "", { duration: 2000,} );
    }
    );
  }

  get username(){
    return this.registerForm.get("username");
  }
  get email(){
    return this.registerForm.get("email");
  }
  get password(){
    return this.registerForm.get("password");
  }
  get datumRodjenja(){
    return this.registerForm.get("datumRodjenja");
  }
}

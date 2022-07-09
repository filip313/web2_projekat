import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  constructor(private service: UserService, private formBuilder:FormBuilder) { }

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
      
    },[CustomPasswordValidator]);
  }

  onFileSelected(event:any){
    this.registerForm.controls['file'] = event.target.files;
  }

  onSubmit(){
      console.log(this.registerForm);
      let registerData = new FormData();
      registerData.append('Username', this.registerForm.controls['username'].value);
      registerData.append('Password', this.registerForm.controls['password'].value);
      registerData.append('Email', this.registerForm.controls['email'].value);
      registerData.append('Ime', this.registerForm.controls['ime'].value);
      registerData.append('Prezime', this.registerForm.controls['prezime'].value);
      registerData.append('DatumRodjenja' ,this.registerForm.controls['datumRodjenja'].value);
      registerData.append('UserType' ,this.registerForm.controls['userType'].value);
      registerData.append('File',this.registerForm.controls['file'].value);
      registerData.append('Adresa',this.registerForm.controls['adresa'].value);

    this.service.register(registerData).subscribe(
    (data:Register) =>{
      console.log(data);
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

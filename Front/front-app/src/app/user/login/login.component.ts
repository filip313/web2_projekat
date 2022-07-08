import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/shared/models/login.model';
import { Token } from 'src/app/shared/models/token.model';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  
  constructor(private service:UserService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['',[
        Validators.required,
      ]],
      password: ['',[
        Validators.required,
      ]]
    });

    this.loginForm.valueChanges.subscribe(console.log);
  }

  onSubmit(){
    let login:Login = new Login();
    login.password = this.loginForm.controls['password'].value;
    login.username = this.loginForm.controls['username'].value;

    this.service.login(login).subscribe(
      (data : Token) => {
        console.log(data);
      },
      error =>{
        console.log(error);
      }
    )
  }

}

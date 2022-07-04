import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/shared/models/login.model';
import { Token } from 'src/app/shared/models/token.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private service:UserService ) { }

  loginForm = new FormGroup({
    username: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required),
  });

  ngOnInit(): void {
  }

  onSubmit(){
    let login:Login = new Login();
    let temp;
    if((temp = this.loginForm.controls['username'].value) != null)
      login.username = temp;
    if((temp = this.loginForm.controls['password'].value) != null)
      login.password = temp;

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

import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/shared/models/login.model';
import { Token } from 'src/app/shared/models/token.model';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
}) export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  
  constructor(private service:UserService, private formBuilder: FormBuilder,
     private auth:AuthService, private router:Router, private snackBar:MatSnackBar,
     private authService: SocialAuthService) {    
       this.authService.authState.subscribe((data) => {
        if(data != null){
        this.service.socialLogin(data).subscribe(
          (data) =>{
            sessionStorage.setItem('token', data.token);
            this.router.navigateByUrl('porudzbina/poruci');
          },
          (error) => {
            console.log(error);
            this.snackBar.open("Doslo je do greske prilikom pokusaja logovanja sa Googlom!", "", { duration: 2000,});
          }
        );
        }
   },
    (error) => {
      console.log(error);
    })
  }

  socialUser: SocialUser = new SocialUser;

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['',[
        Validators.required,
      ]],
      password: ['',[
        Validators.required,
      ]]
    });
    if(!this.auth.isUserLoggedIn()){

      this.loginForm.valueChanges.subscribe(console.log);
    }
    else{
      if(this.auth.isUserAdmin()){
          this.router.navigateByUrl('admin/porudzbine')
      }
      else if(this.auth.isUserDostavljac()){
        this.router.navigateByUrl('porudzbina/nove');
      }
      else if(this.auth.isUserPotrosac()){
        this.router.navigateByUrl('porudzbina/poruci');
      }
      else{
        this.auth.logout();
      }
    }



  }

  onSubmit(){
    let login:Login = new Login();
    login.password = this.loginForm.controls['password'].value;
    login.username = this.loginForm.controls['username'].value;

    this.service.login(login).subscribe(
      (data : Token) => {
        console.log(data);
        localStorage.setItem('token', data.token);
        sessionStorage.setItem('token', data.token);
        this.ngOnInit();
      },
      error =>{
        this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    )
  }

  loginWithGoogle(){
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }
}

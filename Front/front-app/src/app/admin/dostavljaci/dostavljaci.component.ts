import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserData } from 'src/app/shared/models/user.model';
import { Verifkacija } from 'src/app/shared/models/verifikacija.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-dostavljaci',
  templateUrl: './dostavljaci.component.html',
  styleUrls: ['./dostavljaci.component.css']
})
export class DostavljaciComponent implements OnInit {

  dostavljaci:UserData[];
  cols = ['username', 'ime', 'prezime', 'email', 'dugme'];
  constructor(private userService: UserService, private router: Router, private snackBar: MatSnackBar) { }
  
  ngOnInit(): void {
    this.userService.getDostavljace().subscribe(
      (data:UserData[]) =>{
        console.log(data);
        this.dostavljaci = data;
      },
      error =>{
        if(error.status == 401)
          this.router.navigateByUrl('/user/login')
          this.snackBar.open(error.error, "", { duration: 2000,} );
      }
    )
  }

  verifikacija(id:number, verifikovan:boolean){
    let data = new Verifkacija();
    data.dostavljacId = id;
    data.verifikacija = !verifikovan;

    this.userService.verifikujDostavljaca(data).subscribe(
      (data:UserData) =>{
        for(let user of this.dostavljaci){
          if(user.id == data.id){
            user.verifikovan = data.verifikovan;
          }
        }
      },
      error =>{
        if(error.status == 401){
          this.router.navigateByUrl('/user/login')
        }
              this.snackBar.open(error.error, "", { duration: 2000,} );
              }
    )
  }

}

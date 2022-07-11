import { Component, OnInit } from '@angular/core';
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
  constructor(private userService: UserService, private router: Router, private toastr: ToastrService) { }
  
  ngOnInit(): void {
    this.userService.getDostavljace().subscribe(
      (data:UserData[]) =>{
        console.log(data);
        this.dostavljaci = data;
      },
      error =>{
        this.router.navigateByUrl('/user/login')
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
        this.toastr.error(error.error);
      }
    )
  }

}

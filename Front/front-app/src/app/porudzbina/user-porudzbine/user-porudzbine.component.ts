import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-porudzbine',
  templateUrl: './user-porudzbine.component.html',
  styleUrls: ['./user-porudzbine.component.css']
})
export class UserPorudzbineComponent implements OnInit {

  porudzbine:Porudzbina[];
  potrosac:boolean = false;
  cols = ['id', 'user', 'adresa', 'ukCena', 'prihvat', 'trajanje']
  constructor(private servis: PorudzbinaService, private jwt:JwtHelperService,
    private toastr : ToastrService) { }

  ngOnInit(): void {
    let token = localStorage.getItem('token');
    if(token != null){
      let decoded = this.jwt.decodeToken(token);
      this.potrosac = decoded[environment.userRoleKey] == "Potrosac" ? true : false;
      this.servis.getUserPorudzbine(decoded.id).subscribe(
        (data:Porudzbina[]) =>{
          let temp = []
          for(let por of data){
            if(por.status == "Dostavljena"){
              temp.push(por);
            }
          }
          console.log(temp);
          this.porudzbine = temp;
        },
        error => {
          this.toastr.error(error.error);
        }
      )
    }
  }

}

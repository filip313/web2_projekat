import { Component, OnInit } from '@angular/core';
import { UserData } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-dostavljaci',
  templateUrl: './dostavljaci.component.html',
  styleUrls: ['./dostavljaci.component.css']
})
export class DostavljaciComponent implements OnInit {

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getDostavljace().subscribe(
      (data:[UserData]) =>{
        console.log(data);
      }
    )
  }

}

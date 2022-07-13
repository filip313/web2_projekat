import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PorudzbinaProizvod } from 'src/app/shared/models/porudzbinaproizvod.model';
import { PorudzbinaService } from '../../shared/services/porudzbina.service';

@Component({
  selector: 'app-nova-porudzbina',
  templateUrl: './nova-porudzbina.component.html',
  styleUrls: ['./nova-porudzbina.component.css']
})
export class NovaPorudzbinaComponent implements OnInit {

  constructor(private service: PorudzbinaService, private jwt:JwtHelperService,
    private toastr:ToastrService) { }
  
  korpa:PorudzbinaProizvod[] = [];
  cols = ['naziv','kol', 'cena','ukloni'];
  @ViewChild(MatTable) table : MatTable<PorudzbinaProizvod>;
  ngOnInit(): void {
    
  }

  dodajUKorpu(value:PorudzbinaProizvod){
    this.korpa.push(value);
    this.table.renderRows();
  }

  ukupnaCena():number{
    let ret = 0;
    for(let item of this.korpa){
      ret += (item.kolicina * item.proizvod.cena);
    }
    return ret;
  }
}

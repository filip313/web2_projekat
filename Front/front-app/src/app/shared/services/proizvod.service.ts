import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Proizvod } from '../models/proizovd.model';

@Injectable({
  providedIn: 'root'
})
export class ProizvodService {

  constructor(private http:HttpClient) { }

  getProizvode():Observable<Proizvod[]>{
    return this.http.get<Proizvod[]>(environment.serverURL + "api/Proizvod/all");
  }

  dodajProizovd(data:Proizvod):Observable<Proizvod>{
    return this.http.post<Proizvod>(environment.serverURL + "api/Proizvod/add", data);
  }
}

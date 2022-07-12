import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NovaPorudzbina } from '../models/novaporudzbina.model';
import { Porudzbina } from '../models/porudzbina.model';
import { PrihvatiPorudzbinu } from '../models/prihavtiporudzbinu.model';

@Injectable({
  providedIn: 'root'
})
export class PorudzbinaService {

  constructor(private http:HttpClient) { }

  getPorudzbine():Observable<Porudzbina[]>{
    return this.http.get<Porudzbina[]>(environment.serverURL + 'api/Porudzbina/all');
  }
  napraviPorudzbinu(data:NovaPorudzbina):Observable<NovaPorudzbina>{
    return this.http.post<NovaPorudzbina>(environment.serverURL + 'api/Porudzbina/create', data);
  }
  getUserPorudzbine(userId:number):Observable<Porudzbina[]>{
    return this.http.get<Porudzbina[]>(environment.serverURL + 'api/Porudzbina/user/' + userId.toString());
  }
  prihvatiPorudzbinu(data:PrihvatiPorudzbinu):Observable<Porudzbina>{
    return this.http.post<Porudzbina>(environment.serverURL + "api/Porudzbina/prihvati", data);
  }
  getNovePorudzbine():Observable<Porudzbina[]>{
    return this.http.get<Porudzbina[]>(environment.serverURL + 'api/Porudzbine/nove');
  }
  zavrsiPorudzbinu(porudzbinaId:number):Observable<Porudzbina>{
    return this.http.post<Porudzbina>(environment.serverURL + 'api/Porudzbina/zavrsi', porudzbinaId);
  }
}

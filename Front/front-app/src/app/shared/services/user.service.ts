import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from '../models/login.model';
import { Token } from '../models/token.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  login(loginData:Login):Observable<Token>{
    return this.http.post<Token>(environment.serverURL + 'api/User/login', loginData);
  }
}

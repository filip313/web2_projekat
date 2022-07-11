import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from '../models/login.model';
import { Token } from '../models/token.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Register } from '../models/register.model';
import { UserData } from '../models/user.model';
import { Verifkacija } from '../models/verifikacija.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  login(loginData:Login):Observable<Token>{
    return this.http.post<Token>(environment.serverURL + 'api/User/login', loginData);
  }

  register(registerData: FormData):Observable<Register>{
    return this.http.post<Register>(environment.serverURL + 'api/User/register', registerData);
  }
  getUserData(userId:number):Observable<UserData>{
    return this.http.get<UserData>(environment.serverURL + 'api/User/' + userId.toString())
  };

  changeUserData(changeData:FormData):Observable<UserData>{
    return this.http.post<UserData>(environment.serverURL + 'api/User/izmeni', changeData);
  }
  getDostavljace():Observable<UserData[]>{
    return this.http.get<UserData[]>(environment.serverURL + 'api/User/dostavljaci');
  }
  verifikujDostavljaca(data:Verifkacija):Observable<UserData>{
    return this.http.post<UserData>(environment.serverURL + 'api/User/verifikuj', data);
  }
}

import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http'
import { User } from '../models/user.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserService {
 
  private currentUser = undefined
  
  constructor(private http:HttpClient) { }
  get IsLogin() {
    return this.currentUser != undefined;
  }

  get CurrentUser() {
    return this.currentUser;
  }

  setCurrentUser(user) {
    this.currentUser = user;
  }
  setLogoff(){
    this.currentUser=undefined;
  }
  checkUser(user:User):Observable<boolean>{//post 
    return this.http.post<boolean>(environment.url+'user/CheckUser',user);
  }
  addUser(user:User): Observable<boolean>{//post
  return this.http.post<boolean>(environment.url+'user/AddUser',user)
  }
  getuser(code:number):Observable<User> {
    return this.http.post<User>(environment.url+'user/Getmyuser',code)
  }
  saveChanges(u:User):Observable<boolean> {
    return this.http.post<boolean>(environment.url+'user/SaveChanges',u);
  }
  saveChangesInGmhim(u: User):Observable<boolean> {
    return this.http.post<boolean>(environment.url+'gmh/SaveChangesInGmhim',u);
  }

}


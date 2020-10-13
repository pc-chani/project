import { Injectable } from '@angular/core';
import { User } from '../models/User.model';
import { Observable } from 'rxjs';
import { GMH } from '../models/Gmh.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GmhService {
private myGmhim:GMH[];
  constructor(private http:HttpClient) { }
  
  getMyGmhim(currentUser: User):Observable<GMH[]> {
  return this.http.post<GMH[]>(environment.url+'gmh/getMyGmhim',currentUser);
  }
  setMyGmhim(gmhim){
    this.myGmhim=gmhim;
  }
  getOneGmh(code:number):GMH {
    return this.myGmhim.find(g=>g.GmhCode==code);
  }
 delete(gmh:GMH):Observable<boolean>{
   this.myGmhim.splice(this.myGmhim.indexOf(gmh),1)
   return this.http.post<boolean>(environment.url+'gmh/delete',gmh);
 }
  getUser(gmh:GMH):Observable<User>{
    return this.http.post<User>(environment.url+'user/getUser',gmh);
  }
  saveChange(gmh:GMH):Observable<boolean>{
    return this.http.post<boolean>(environment.url+'gmh/saveChange',gmh);
  
  }
  add(gmh:GMH):Observable<boolean>{
    return this.http.post<boolean>(environment.url+'gmh/add',gmh);
  }
}

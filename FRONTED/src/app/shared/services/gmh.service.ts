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
public myGmh:GMH[];
  constructor(private http:HttpClient) { }
  
  getMyGmhim(currentUser: User):Observable<GMH[]> {
  return this.http.post<GMH[]>(environment.url+'gmh/getMyGmhim',currentUser);
  
  }
  getOneGmh(code:number):GMH {
    return this.myGmh.find(g=>g.GmhCode==code);
  }
}

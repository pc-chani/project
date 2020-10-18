import { Injectable } from '@angular/core';
import { User } from '../models/User.model';
import { Observable } from 'rxjs';
import { GMH } from '../models/Gmh.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
<<<<<<< HEAD
import { CategoryGMH } from '../models/CategoryGMH.model';

var map;
var service;
var infowindow
=======
>>>>>>> 774e900000862fa6ff75e9c9c8769c873e83436c

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


  getCategoryGmach():Observable<CategoryGMH[]>{
    console.log(this.http.get<CategoryGMH[]>(environment.url+'gmh/getCategories'));
    return this.http.get<CategoryGMH[]>(environment.url+'gmh/getCategories')
  }


  getCategoriesForGmach(masterGmh:CategoryGMH):Observable<CategoryGMH[]>{
    console.log(this.http.post<CategoryGMH[]>(environment.url+'gmh/getCategoriesForGmach',masterGmh));
    return this.http.post<CategoryGMH[]>(environment.url+'gmh/getCategoriesForGmach',masterGmh)
  }

  search(gmhForSearch:CategoryGMH):Observable<GMH[]>{
    console.log(this.http.post<GMH[]>(environment.url+'gmh/searchGMH',gmhForSearch));
    return this.http.post<GMH[]>(environment.url+'gmh/searchGMH',gmhForSearch)
  }

  //showGMHS(a: any, gmhs: GMH[]) {   }

}

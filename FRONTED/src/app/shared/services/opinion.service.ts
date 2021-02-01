import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GMH } from '../models/Gmh.model';
import { Opinion } from '../models/Opinion.model';

@Injectable({
  providedIn: 'root'
})
export class OpinionService {

  constructor(private http: HttpClient) { }
  getOpinionsForGMH(mygmh: GMH) : Observable<Opinion[]> {
    console.log(mygmh);
    return this.http.post<Opinion[]>(environment.url + 'opinion/getOpinionsForGMH', mygmh);
  }
  addOpinion(opinion:Opinion):Observable<boolean>{
    return this.http.post<boolean>(environment.url+'opinion/addOpinion',opinion);
  }
}

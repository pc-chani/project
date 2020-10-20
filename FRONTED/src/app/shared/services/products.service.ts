import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/Product.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GMH } from '../models/Gmh.model';
import { productToGmh } from '../models/productToGMH.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http:HttpClient) { }



  getProductsForGMH(mygmh:GMH):Observable<productToGmh[]> {
    return this.http.post<productToGmh[]>(environment.url+'products/getProductsForGMH',mygmh);
  }
  getProduct(ptg:productToGmh):Observable<Product>{
    return this.http.post<Product>(environment.url+'products/getProduct',ptg);
  }
  saveChange(product:productToGmh):Observable<boolean>{
    return this.http.post<boolean>(environment.url+'products/saveChange',product);
  }
  addProduct( product:productToGmh):Observable<boolean>{
       return this.http.post<boolean>(environment.url+'products/add',product);
  }
  delete( product:productToGmh):Observable<boolean>{
    return this.http.post<boolean>(environment.url+'products/delete',product);
}
postImage(fd : FormData): Observable<string>{
  return this.http.post<string>(environment.url+'products/postImg', fd );
}

getImage(): Observable<Blob> {
  return this.http.get( environment.url+'products/getImg', { responseType: 'blob' })      
}
getProducts():Observable<Product[]> {
  return this.http.get<Product[]>(environment.url+'products/getProducts');
}

getProductsAccordingToGmhCategory(myGmh:GMH):Observable<Product[]> {
  return this.http.post<Product[]>(environment.url+'products/getProductsAccordingToGmhCategory',myGmh);
}
}

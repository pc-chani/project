import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../models/Product.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GMH } from '../models/Gmh.model';
import { Images, productToGmh } from '../models/productToGMH.model';
import { Lending } from '../models/Lending.model';
import { Opinion } from '../models/Opinion.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
 

  constructor(private http: HttpClient) { }

  getProductsForGMH(mygmh: GMH): Observable<productToGmh[]> {
    return this.http.post<productToGmh[]>(environment.url + 'products/getProductsForGMH', mygmh);
  }
  
  getProduct(ptg: productToGmh): Observable<Product> {
    return this.http.post<Product>(environment.url + 'products/getProduct', ptg);
  }
  edit(fd: FormData): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/edit', fd);
  }
  changeAmount(product: productToGmh): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/saveChange', product);
  }
  addProduct(fd: FormData): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/postImg', fd)
  }
  addPr(p: Product): Observable<number> {
    return this.http.post<number>(environment.url + 'products/addProduct', p)
  }
  delete(p: productToGmh): Observable<boolean> {
    if(p!=null)
    return this.http.post<boolean>(environment.url + 'products/delete', p);
  }
  getImage(p:productToGmh): Observable<Images[]> {
    return this.http.post<Images[]>(environment.url + 'products/getImg',p);
  }
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.url + 'products/getProducts');
  }
  getProductsAccordingToGmhCategory(myGmh: GMH): Observable<Product[]> {
    return this.http.post<Product[]>(environment.url + 'products/getProductsAccordingToGmhCategory', myGmh);
  }
  getLendings(p: productToGmh): Observable<Lending[]> {
    return this.http.post<Lending[]>(environment.url + 'lendings/getLendings', p);

  }
  addLending(l:Lending):Observable<boolean>{
    return this.http.post<boolean>(environment.url + 'lendings/addLending', l);
  }
  removeLending(l:Lending):Observable<boolean>{
    return this.http.post<boolean>(environment.url + 'lendings/deleteLending', l);
  }
 
}

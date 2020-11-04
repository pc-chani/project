import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../models/Product.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GMH } from '../models/Gmh.model';
import { Images, productToGmh } from '../models/productToGMH.model';
import { Lending } from '../models/Lending.model';

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
  saveChange(product: productToGmh): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/saveChange', product);
  }
  addProduct(fd: FormData): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/postImg', fd)
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
  getLendigs(p: productToGmh): Observable<Lending[]> {
    return this.http.post<Lending[]>(environment.url + 'lendings/getLendings', p);

  }
}

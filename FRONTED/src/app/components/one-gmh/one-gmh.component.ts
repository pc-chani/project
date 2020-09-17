import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { Product } from 'src/app/shared/models/Product.model';
import { productToGmh } from 'src/app/shared/models/productToGMH.model';
import { ProductsService } from 'src/app/shared/services/products.service';



@Component({
  selector: 'app-one-gmh',
  templateUrl: './one-gmh.component.html',
  styleUrls: ['./one-gmh.component.css']
})
export class OneGmhComponent implements OnInit {
  myGmh: GMH
  products: productToGmh[];
  constructor(private router: Router, private route: ActivatedRoute, private gmhService: GmhService, private productsServices: ProductsService) { }

  ngOnInit(): void {
    const GmhCode = this.route.snapshot.paramMap.get('id');
    this.myGmh = this.gmhService.getOneGmh(parseInt(GmhCode));
    this.productsServices.getProducts(this.myGmh).subscribe(
      res => {
        this.products = res; console.log(res);
        this.products.forEach(p => {
          this.productsServices.getProduct(p).subscribe(
            res => { p.Name = res.Productname; console.log(res); },
            err => { console.log(err); }
          )
        });
      },
      err => { console.log(err); }
    )

  }
}

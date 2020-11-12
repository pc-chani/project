import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { productToGmh } from 'src/app/shared/models/productToGMH.model';
import { User } from 'src/app/shared/models/User.model';
import { ProductsService } from 'src/app/shared/services/products.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-gmh',
  templateUrl: './gmh.component.html',
  styleUrls: ['./gmh.component.css']
})
export class GMHComponent implements OnInit {
  @Input('gmh') gmh:GMH;

  productsToGmh:productToGmh[];
    constructor(private userService: UserService,private productsService:ProductsService) { }
  
    ngOnInit(): void {
      console.log(this.gmh)
      this.productsService.getProductsForGMH(this.gmh).subscribe(
        res => {
          this.productsToGmh = res,
          console.log(res)
        },
        err => console.log(err),
      );
    }

  

}

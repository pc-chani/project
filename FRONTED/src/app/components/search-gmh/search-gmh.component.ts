import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { ProductsService } from 'src/app/shared/services/products.service';
import { Observable } from 'rxjs';
import { Product } from 'src/app/shared/models/Product.model';
import { map, startWith } from 'rxjs/operators';
import getBrowserFingerprint from 'get-browser-fingerprint';
import { Searches } from 'src/app/shared/models/Searches.model';
import { validSearch } from 'src/app/validators/valid';
@Component({
  selector: 'app-search-gmh',
  templateUrl: './search-gmh.component.html',
  styleUrls: ['./search-gmh.component.css'],
})
export class SearchGMHComponent implements OnInit {
 // @Output() gmhsArr = new EventEmitter<GMH[]>();
  searchForm: FormGroup;
  categories: CategoryGMH[];
  masterCategory: CategoryGMH;
  tatC: CategoryGMH;
  tatCategories: CategoryGMH[];
  gmhs: GMH[];
  filteredProducts: Observable<Product[]>;
  products: Array<Product>
  formData;
  currLat=0;
  currLng=0;
  adress;
  distance=20;
  hideTatCategory=true;
  //openGmhDetails;
  IncreaseTheSearchArea;
  uniqueChars: GMH[] = [];
  constructor(private userService: UserService, private productsService: ProductsService, private router: Router, private gmhService: GmhService,private route: ActivatedRoute,) { }
  ngOnInit(): void {
    this.searchForm = new FormGroup({
      textSearch: new FormControl('',Validators.compose([Validators.pattern('[א-ת]{10}')])),
      category: new FormControl(''),
      tatCategory: new FormControl(''),
      currentLocation:new FormControl(''),
      location: new FormControl(''),
      distance:new FormControl(''),
    },{validators: validSearch("location")});
    this.getCategoryGmh();
    this.productsService.getProducts().subscribe(
      res => {
        this.products = res,
          // console.log(res),
          this.filter()
      },
      err => console.log(err),
    );
    this.getCurrentLocation();
  }
  filter() {
    this.filteredProducts = this.searchForm.controls.textSearch.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.ProductName),
        map(name => name ? this._filter(name) : this.products.slice())
      );
  }

  getCurrentLocation() {
    this.searchForm.controls.location.setValue("")
    this.searchForm.controls.location.disable()
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        this.currLat = position.coords.latitude;
        this.currLng = position.coords.longitude;
      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }
  }
  chooseLocation(){
    this.searchForm.controls.location.enable()
  }

  private _filter(name: string): Product[] {
    const filterValue = name.toLowerCase();
    return this.products.filter(c => c.Productname.toLowerCase().indexOf(filterValue) === 0);
  }

  getCategoryGmh() {
    this.gmhService.getCategoryGmach().subscribe(res => {
      this.categories = res;
      //console.log(res);
      err => { console.log(err); }
    });
  }

  handleDestinationChange(a: Address) {
    this.adress = a;
    //  console.log(a)
  }

  getCategoriesForGmach() {
    this.hideTatCategory=false
    this.searchForm.controls['tatCategory'].enable();
    //  console.log(this.searchForm.controls.category.value);
    this.categories.forEach(element => {
      if (element.CategoryName == this.searchForm.controls.category.value)
        this.masterCategory = element;
    });
    this.gmhService.getCategoriesForGmach(this.masterCategory).subscribe(res => {
      this.tatCategories = res;
      // console.log(res);
      err => { console.log(err); }
    });
  }
  
  

  saveSearch() {
    const fingerprint = getBrowserFingerprint();
    let s = new Searches();
    s.fingerPrint = fingerprint;
    s.City=this.adress.vicinity
    if (this.currLat != 0) s.Adress = (this.currLat + " " + this.currLng).toString();
    else if (this.adress != undefined) s.Adress = this.adress;
    if (this.searchForm.controls.tatCategory.value.value != "" && this.searchForm.controls.tatCategory.value.value!=undefined) s.Category = this.tatCategories.find(tc => tc.CategoryName == this.searchForm.controls.tatCategory.value).CategoryCode;
    else if (this.searchForm.controls.category.value != "") s.Category = this.categories.find(tc => tc.CategoryName == this.searchForm.controls.category.value).CategoryCode;
    else if (this.searchForm.controls.textSearch.value.Productname != "") {
      s.Category = this.products.find(p => p.Productname == this.searchForm.controls.textSearch.value.Productname).CategoryCode;
    }
     console.log(s);
     this.gmhService.saveSearch(s).subscribe(
     res => console.log(res)
      )
  }
  
 
  showGMHS(a) {
    this.router.navigate(['/gmh', this.gmhs]);
  }
  displayFn(c: Product): string {
    return c && c.Productname ? c.Productname : '';
  }


  search() 
  {
    const fingerprint = getBrowserFingerprint();
    // this.openGmhDetails = null

    this.formData = new FormData();
    if (this.searchForm.controls.textSearch.value != null)
      this.formData.append('text', this.searchForm.controls.textSearch.value.Productname)
    else this.formData.append('text', "")
    if (this.searchForm.controls.category.value != ""&&this.searchForm.controls.category.value != "-כל הקטגוריות-")
      this.formData.append('category', this.masterCategory.CategoryCode)
    else this.formData.append('category', 0)
    //console.log(this.searchForm.controls.tatCategory.value+","+this.tatCategories)
    if (this.searchForm.controls.tatCategory.value != ""&&this.tatCategories != undefined&&this.searchForm.controls.tatCategory.value != "-כל תתי הקטגוריות-") 
    {
      this.tatCategories.forEach(element => { if (element.CategoryName == this.searchForm.controls.tatCategory.value) this.tatC = element; });
      this.formData.append('tatCategory', this.tatC.CategoryCode)
    }
    else this.formData.append('tatCategory', 0);
    //console.log(this.formData.get('tatCategory'))
    if (this.adress == undefined||this.adress=="") {
       this.formData.append('CurrentLocation1', this.currLat); 
       this.formData.append('CurrentLocation2', this.currLng)
       this.formData.append('location', "")
      }

   else {
     this.formData.append('location', this.adress.formatted_address);
     this.formData.append('CurrentLocation1', 0);
     this.formData.append('CurrentLocation2', 0)
   }

    //console.log(this.searchForm.controls.distance.value)
    if(this.searchForm.controls.distance.value!="")
    this.distance=this.searchForm.controls.distance.value;
    this.formData.append('distance', this.distance);
    console.log(this.distance)
    if(this.currLat==0&&this.currLng==0&&this.adress == undefined||this.adress=="")
    this.distance=20000;
  
      console.log(this.formData)    
     this.gmhService.search(this.formData)
    .subscribe(res => {
      this.gmhService.gmhsSearch = res;
      
      console.log(res)
      console.log(this.gmhService.gmhsSearch)
      if(this.gmhService.gmhsSearch.length==0)
       this.IncreaseTheSearchArea=true;      
     else {                
   this.IncreaseTheSearchArea=false;
 }        
      this.gmhService.gmhsSearch.forEach(e => 
        {
     // this.gmhs = res;
     // this.gmhs.forEach(e => {


    // if (this.adress == undefined) {
    //   this.formData.append('CurrentLocation1', this.currLat);
    //   this.formData.append('CurrentLocation2', this.currLng)
    //   this.formData.append('location', "")
    // }
    // else {
    //   this.formData.append('location', this.adress.formatted_address);
    //   this.formData.append('CurrentLocation1', 0);
    //   this.formData.append('CurrentLocation2', 0)
    // }
    // this.gmhService.search(this.formData).subscribe(res => {
    //   this.gmhs = res;
    //   this.gmhs.forEach(e => {
        this.gmhService.getUser(e).subscribe(res => {
          e.User = res;

          err => { console.log(err); }
        });
      }); 
      this.router.navigate(['/gmhList']);
      err => { console.log(err); }
    });
   // this.saveSearch();
 
   // this.gmhsArr.emit(this.gmhs)
}
 
  //     });
  //      console.log(res);
  //     this.saveSearch();
  //     err => { console.log(err); }
  //   });
  // }
  // showGMHS(a) {
  //   // console.log(a);
  //   this.router.navigate(['/gmh', this.gmhs]);
  // }
  // displayFn(c: Product): string {
  //   return c && c.Productname ? c.Productname : '';
  // }
}
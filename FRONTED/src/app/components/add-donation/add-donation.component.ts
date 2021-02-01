import { getLocaleDateTimeFormat } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { donation } from 'src/app/shared/models/Donations.model';
import { Product } from 'src/app/shared/models/Product.model';
import { CategoriesService } from 'src/app/shared/services/categories.service';
import { DonationService } from 'src/app/shared/services/donation.service';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { ProductsService } from 'src/app/shared/services/products.service';

@Component({
  selector: 'app-add-donation',
  templateUrl: './add-donation.component.html',
  styleUrls: ['./add-donation.component.css']
})
export class AddDonationComponent implements OnInit {
  donationForm: FormGroup
  categories: Array<CategoryGMH>
  filteredCategories: Observable<CategoryGMH[]>;
  tatCategories: Array<CategoryGMH>
  filteredTatCategories: Observable<CategoryGMH[]>;
  adress;
  formData: FormData = new FormData();
  url;
  donationCode;
  donation = true;
  donor = false
  filteredProducts: Observable<Product[]>;
  products: Array<Product>
  constructor(private gmhService: GmhService, private donationService: DonationService,
    private categoriesService: CategoriesService, private productsService: ProductsService) { }

  ngOnInit(): void {
    this.gmhService.getCategoryGmach().subscribe(
      res => {
        this.categories = res; this.filter()

      },
      err => console.log(err)
    );
    this.donationForm = new FormGroup({
      donationName: new FormControl('', Validators.required),
      Categories: new FormControl('', Validators.required),
      newCategory: new FormControl({ value: '', disabled: true }),
      tatCategories: new FormControl({ value: '', disabled: true }),
      newTatCategory: new FormControl({ value: '', disabled: true }),
      comments: new FormControl(''),
      donorName: new FormControl('', Validators.required),
      donorEmail: new FormControl('', Validators.email),
      adress: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.pattern('[0-9]{9,10}'))
    })

  }
  getProducts(c) {
    this.productsService.getProductsAccordingToGmhCategory(c.option.value).subscribe(
      res => {
        this.products = res,
       //   console.log(res);

        this.filter1();
      },
      err => console.log(err),
    );
  }
  filter1() {
    this.filteredProducts = this.donationForm.controls.donationName.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.ProductName),
        map(name => name ? this._filter1(name) : this.products.slice())
      );
      
  }
  filter() {
    this.filteredCategories = this.donationForm.controls.Categories.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.CategoryName),
        map(name => name ? this._filter(name) : this.categories.slice())
      );
  }
  private _filter(name: string): CategoryGMH[] {
    const filterValue = name.toLowerCase();
    return this.categories.filter(c => c.CategoryName.toLowerCase().indexOf(filterValue) === 0);
  }
  private _filter1(name: string): Product[] {
    const filterValue = name.toLowerCase();
    return this.products.filter(c => c.Productname.toLowerCase().indexOf(filterValue) === 0);
  }
  displayFn(c: CategoryGMH): string {
    return c && c.CategoryName ? c.CategoryName : '';
  }
  displayFn1(p: Product): string {
    return p && p.ProductCode ? p.Productname : '';
  }
  handleDestinationChange(a: Address) {
    // console.log(a)
    this.adress = a.formatted_address;
  }
  // getCategoryGmh() {
  //   this.gmhService.getCategoryGmach().subscribe(res => {
  //     this.categories = res;
  //     console.log(res);
  //     err => { console.log(err); }
  //   });
  getCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        let currLat = position.coords.latitude;
        let currLng = position.coords.longitude;
      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }

  }
  getTatCategoriesForGmh(c) {
    this.donationForm.controls.tatCategories.enable()
    this.gmhService.getCategoriesForGmach(c.option.value).subscribe(res => {
      this.tatCategories = res;
    //  console.log(res),

        this.filteredTatCategories = this.donationForm.controls.tatCategories.valueChanges
          .pipe(
            startWith(''),
            map(value => typeof value === 'string' ? value : value.CategoryName),
            map(name => name ? this._filter(name) : this.tatCategories.slice())
          );
      err => { console.log(err); }
    });
  }
  addDonation(d) {
    //let d=new donation()
    d.Adress = this.adress;
    // d.category = this.donationForm.controls.Categories.value.CategoryCode;
    d.Description = this.donationForm.controls.comments.value;
    //d.Category=this.donationForm.controls.tatCategories.value.CategoryCode;
    d.Phone = this.donationForm.controls.phone.value;
    console.log(this.donationForm.controls.donationName.value);
    d.donationName = this.donationForm.controls.donationName.value;
    d.donorName = this.donationForm.controls.donorName.value;
    d.donorEmail = this.donationForm.controls.donorEmail.value;
    console.log(d);
    this.formData.append('donation', JSON.stringify(d))
    this.donationService.addDonation(this.formData).subscribe(
      res => {
        console.log(res)
        this.donationCode = res;
        if (res != 0) {
          alert("התקבלה בהצלחה תודה!" + res + "תרומה")
          this.formData = new FormData()
        }
      }
    )
  }
  handleFileInput(etf) {
    this.formData.append('Image', etf[0]);
    console.log(etf);
    if (etf && etf[0]) {
      for (const f of etf) {
        const file = f;
        const reader = new FileReader();
        reader.onload = e => this.url = reader.result;
        reader.readAsDataURL(file);
      }
      //  console.log(this.url);
    }
  }
  newcategory() {
    this.donationForm.controls["newCategory"].enable();
    this.donationForm.controls["Categories"].disable();
    this.donationForm.controls["tatCategories"].disable();
    this.donationForm.controls["newTatCategory"].disable();
    this.donationForm.controls["newTatCategory"].setValue('');
    this.donationForm.controls["tatCategories"].setValue('');

    this.donationForm.controls["Categories"].setValue('');

  }
  newtatcategory() {
    this.donationForm.controls["newTatCategory"].enable();
    this.donationForm.controls["tatCategories"].disable();
    this.donationForm.controls["tatCategories"].setValue('');

    // this.tatCategories=new Array<CategoryGMH>();
  }
  choosetatcategory() {
    if (this.donationForm.controls["newCategory"].disabled)
      this.donationForm.controls["tatCategories"].enable();
    this.donationForm.controls["newTatCategory"].disable();
    this.donationForm.controls["newTatCategory"].setValue('')
  }
  choosecategory() {
    this.donationForm.controls["newCategory"].disable();
    this.donationForm.controls["Categories"].enable();
    this.donationForm.controls["newCategory"].setValue('')
  }
  setCategory() {
    let d = new donation();
    let master;
    console.log(this.donationForm.controls["Categories"].value != null, this.donationForm.controls["Categories"].value);

    if (this.donationForm.controls["Categories"].value != null && this.donationForm.controls["Categories"].value != "")//נבחרה קגורית אב
    {
      this.categories.forEach(element => {
        if (element.CategoryName == this.donationForm.controls.Categories.value.CategoryName) {
          d.Category = element.CategoryCode;
          master = element.CategoryCode
        }
      })
      this.setTatCategory(d, master)
    }
    else if (this.donationForm.controls["newCategory"].value != "") {//קטגורית אב חדשה
      console.log(this.donationForm.controls["newCategory"].value);

      let c = new CategoryGMH();
      c.CategoryName = this.donationForm.controls["newCategory"].value;
      console.log(c);

      this.categoriesService.addCategory(c).subscribe(
        res => {
          console.log(res);
          d.Category = res;
          master = res;
          this.setTatCategory(d, master)

        }
      )
    }
  }
  setTatCategory(d, master) {
    if (this.donationForm.controls["newTatCategory"].value != "")//תת קטגוריה חדשה
    {
      console.log(this.donationForm.controls["newTatCategory"].value);

      let c = new CategoryGMH();
      c.CategoryName = this.donationForm.controls["newTatCategory"].value;
      c.MasterCategoryCode = master;
      this.categoriesService.addCategory(c).subscribe(
        res => {
          console.log(res);
          d.Category = res;
          //  d.MasterCategory = master;
          console.log(d);
          this.addDonation(d);
        }
      )

    }
    else if (this.donationForm.controls["tatCategories"].value != "") {//נבחרה תת קטגוריה
      console.log(this.donationForm.controls["tatCategories"].value);
      this.tatCategories.forEach(element => {
        if (element.CategoryName === this.donationForm.controls.tatCategories.value.CategoryName) {
          d.Category = element.CategoryCode;
          // d.MasterCategory = master
        }
      })
      this.addDonation(d);
    }
    else {
      //   d.MasterCategory = 0;
      this.addDonation(d);
    }
  }
  donorDetails() {
    this.donor = !this.donor
    this.donation = !this.donation
  }
 
}

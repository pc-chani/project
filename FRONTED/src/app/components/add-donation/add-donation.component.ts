import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { donation } from 'src/app/shared/models/Donations.model';
import { CategoriesService } from 'src/app/shared/services/categories.service';
import { DonationService } from 'src/app/shared/services/donation.service';
import { GmhService } from 'src/app/shared/services/gmh.service';

@Component({
  selector: 'app-add-donation',
  templateUrl: './add-donation.component.html',
  styleUrls: ['./add-donation.component.css']
})
export class AddDonationComponent implements OnInit {
  donationForm:FormGroup
  categories: Array<CategoryGMH>
  filteredCategories: Observable<CategoryGMH[]>;
  tatCategories:Array<CategoryGMH>
  filteredTatCategories: Observable<CategoryGMH[]>;
  adress;
  formData:FormData=new FormData();
  url;
  donationCode;
  constructor(private gmhService:GmhService,private donationService:DonationService ) { }

  ngOnInit(): void {
    this.gmhService.getCategoryGmach().subscribe(
      res =>{ this.categories = res;this.filter()
      
      },
      err => console.log(err)
    );
    this.donationForm = new FormGroup({
      donationName: new FormControl('', Validators.required),
      Categories: new FormControl('', Validators.required),
      tatCategories:new FormControl({ value: '', disabled: true }),
      comments:new FormControl(''),
      donorName: new FormControl('', Validators.required),
      donorEmail: new FormControl('', Validators.email),
      adress: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.pattern('[0-9]{9,10}'))
    })
    
  }
  filter(){
    console.log(this.categories);
    
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
  displayFn(c: CategoryGMH): string {
    return c && c.CategoryName ? c.CategoryName : '';
  }
  handleDestinationChange(a: Address) {
    console.log(a)
    this.adress=a.formatted_address;
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
getCategoriesForGmach() {
  let masterCategory
  this.donationForm.controls['tatCategory'].enable();
  console.log(this.donationForm.controls.category.value);
  this.categories.forEach(element => {
    if (element.CategoryName == this.donationForm.controls.category.value)
      masterCategory = element;
  });
  this.gmhService.getCategoriesForGmach(masterCategory).subscribe(res => {
    this.tatCategories = res;
    console.log(res);
    err => { console.log(err); }
  });
}
addDonation(){
let d=new donation()
d.Adress=this.adress;
d.MasterCategory=this.donationForm.controls.Categories.value.CategoryCode;
d.Description=this.donationForm.controls.comments.value;
//d.Category=this.donationForm.controls.tatCategories.value.CategoryCode;
d.Phone=this.donationForm.controls.phone.value;
d.donationName=this.donationForm.controls.donationName.value;
d.donorName=this.donationForm.controls.donorName.value;
d.donorEmail=this.donationForm.controls.donorEmail.value;

this.formData.append('donation',JSON.stringify(d))
this.donationService.addDonation(this.formData).subscribe(
  res=>{console.log(res)
 this.donationCode=res;
 if(res!=0)
  alert("התקבלה בהצלחה תודה!"+res+"תרומה") }
)
}
handleFileInput(etf) {
  this.formData.append('Image',etf[0]);
  console.log(etf);
  if (etf && etf[0]) {
    for (const f of etf) {
      const file = f;
      const reader = new FileReader();
      reader.onload = e => this.url=reader.result;
      reader.readAsDataURL(file);
    }
  //  console.log(this.url);
  }
}
}

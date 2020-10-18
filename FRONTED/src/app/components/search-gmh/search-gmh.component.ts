import { Component, OnInit } from '@angular/core';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-gmh',
  templateUrl: './search-gmh.component.html',
  
  styleUrls: ['./search-gmh.component.css'],
})
export class SearchGMHComponent implements OnInit {
 gmhForSearch:CategoryGMH;
  masterCategoryName:string;
  masterCategory:CategoryGMH;
 searchForm : FormGroup;
 gmhs:GMH[];
 categories:CategoryGMH[];
 tatCategories:CategoryGMH[];

  constructor(private userService: UserService,private router:Router,private gmhService:GmhService) { }
   
  ngOnInit(): void {
    this.searchForm = new FormGroup({
      textSearch: new FormControl('', Validators.required),
      category: new FormControl('', Validators.required),
      tatCategory: new FormControl({value:'',disabled:true}, Validators.required),
      location: new FormControl('', Validators.required),
     });
     this. getCategoryGmh();
   
  }
 

  

//search(){
//this.gmhService.search().subscribe(
//  res=> console.log(res)
//)
//}

getCategoryGmh() 
{
this.gmhService.getCategoryGmach().subscribe(res => {
    this.categories = res;
    console.log(res);
    err => { console.log(err); }
  });
  
}


getCategoriesForGmach()
{

this.searchForm.controls['tatCategory'].enable();

  console.log(this.searchForm.controls.category.value);
  this.categories.forEach(element => {
    if(element.CategoryName==this.searchForm.controls.category.value)
    this.masterCategory=element;
   });
    this.gmhService.getCategoriesForGmach(this.masterCategory).subscribe(res => {
    this.tatCategories = res;
    console.log(res);
    err => { console.log(err); }
    });
  
}

search(){
  this.tatCategories.forEach(element => {
    if(element.CategoryName==this.searchForm.controls.tatCategory.value)
    this.gmhForSearch=element;
   });
  this.gmhService.search(this.gmhForSearch).subscribe(res => {
    this.gmhs = res;
    this.gmhs.forEach(e => {
      this.gmhService.getUser(e).subscribe(res => {
        e.User=res;
    err => { console.log(err); }
  });
    });
    
    console.log(res);
    err => { console.log(err); }
  });
}

showGMHS(a){
  console.log(a);
     //this.userService.showGMHS(a,this.gmhs);
     this.router.navigate(['/gmh',this.gmhs]);

  }

}
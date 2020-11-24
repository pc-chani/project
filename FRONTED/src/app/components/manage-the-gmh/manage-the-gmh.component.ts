import { Component, OnInit, } from '@angular/core';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { User } from 'src/app/shared/models/User.model';
import { UserService } from 'src/app/shared/services/user.service';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { CategoriesService } from 'src/app/shared/services/categories.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Address } from 'ngx-google-places-autocomplete/objects/address';

@Component({
  selector: 'app-manage-the-gmh',
  templateUrl: './manage-the-gmh.component.html',
  styleUrls: ['./manage-the-gmh.component.css'],

})
export class ManageTheGMHComponent implements OnInit {
  currentUser: User
  myGmhim: GMH[]
  currentgmh: GMH = undefined
  open: boolean = false;
  newgmh = false;
  gmhForm: FormGroup;
  editGmhForm: FormGroup;
  categories: Array<CategoryGMH>
  filteredCategories: Observable<CategoryGMH[]>;
  adress: string;
  masterCategory: CategoryGMH;
  tatC: CategoryGMH;
  tatCategories: CategoryGMH[];
  filteredTatCategories: Observable<CategoryGMH[]>;
  constructor(private gmhService: GmhService, private userService: UserService, private categoriesService: CategoriesService) { }

  ngOnInit(): void {
    // this.categoriesService.getCategories().subscribe(
    //   res => this.categories = res,
    //   err => console.log(err)
    //);
    this.myGmhim = JSON.parse(localStorage.getItem('gmhim'));
    this.gmhService.setMyGmhim(this.myGmhim)
    this.gmhForm = new FormGroup({
      GmhName: new FormControl(),
      category: new FormControl(),
      newCategory: new FormControl(),
      tatCategory: new FormControl(),
      newTatCategory: new FormControl(),
      comments: new FormControl()
    })
    this.gmhForm.controls["newCategory"].disable();
    this.gmhForm.controls["newTatCategory"].disable();

    this.editGmhForm = new FormGroup({
      GmhhName: new FormControl(),
      Phone: new FormControl(Validators.pattern('[0-9]{9}')),
      E_mail: new FormControl(Validators.email),
      Comments: new FormControl(),
      Adress: new FormControl(),
      userName: new FormControl()
    });
    this.currentUser = this.userService.CurrentUser;
    this.getMyGmhim()
    //console.log(this.myGmhim)
  }
  private _filter(name: string): CategoryGMH[] {
    const filterValue = name.toLowerCase();
    return this.categories.filter(c => c.CategoryName.toLowerCase().indexOf(filterValue) === 0);
  }
  getMyGmhim() {
    this.gmhService.getMyGmhim(this.userService.CurrentUser).subscribe(
      res => {
        this.gmhService.setMyGmhim(res); this.myGmhim = res;
        localStorage.setItem('gmhim', JSON.stringify(res));

        this.myGmhim.forEach(g =>
          this.gmhService.getUser(g).subscribe(
            res => g.User = res
          ))
        //console.log(res);
      },
      err => { console.log(err); }
    )
  }
  delete(gmh) {
    if (confirm("Are you sure to delete")) {
      this.gmhService.delete(gmh).subscribe(
        res => console.log(res),
        err => console.log(err)
      );
    }
  }
  close() {
    this.open = false;
  }
  edit(g: GMH) {
    this.open = true
    this.currentgmh = g;
    this.adress=this.currentgmh.Adress;
    this.editGmhForm.controls.GmhhName.setValue(g.GmhName)
    this.editGmhForm.controls.Adress.setValue(g.Adress)
    this.editGmhForm.controls.Phone.setValue(g.Phone)
    this.editGmhForm.controls.Comments.setValue(g.comments)
    this.editGmhForm.controls.E_mail.setValue(g.e_mail)
  }
  saveChange() {
    let g = new GMH();
    g.Adress = this.adress;
    g.GmhCode=this.currentgmh.GmhCode;
    g.GmhName = this.editGmhForm.controls.GmhhName.value;
    g.Phone = this.editGmhForm.controls.Phone.value;
    g.comments = this.editGmhForm.controls.Comments.value;
    g.e_mail= this.editGmhForm.controls.E_mail.value;
    this.open = false
    console.log(g);
    
    this.gmhService.saveChange(g).subscribe(
      res => {
        console.log(res)
        if (res) this.getMyGmhim();
      }
    )
  }
  new() {
    this.newgmh = true;
    this.getCategoryGmh();

  }
  closeNew() {
    this.newgmh = false
  }
  setCategoty() {
    let g = new GMH();
    let master;
    if (this.gmhForm.controls["category"].value != null)//נבחרה קגורית אב
    {
      this.categories.forEach(element => {
        console.log(element.CategoryName, this.gmhForm.controls.category.value.CategoryName);

        if (element.CategoryName == this.gmhForm.controls.category.value.CategoryName) {
          g.CategoryCode = element.CategoryCode;
          master = element.CategoryCode
        }
      })

    }
    else if (this.gmhForm.controls["newCategory"].value != null) {//קטגורית אב חדשה
      console.log(this.gmhForm.controls["newCategory"].value);

      let c = new CategoryGMH();
      c.CategoryName = this.gmhForm.controls["newCategory"].value;
      this.categoriesService.addCategory(c).subscribe(
        res => {
          console.log(res);
          g.CategoryCode = res;
          master = res;
        }
      )
    }
    if (this.gmhForm.controls["newTatCategory"].value != null)//תת קטגוריה חדשה
    {
      console.log(this.gmhForm.controls["newTatCategory"].value);

      let c = new CategoryGMH();
      c.CategoryName = this.gmhForm.controls["newTatCategory"].value;
      c.MasterCategoryCode = master;
      this.categoriesService.addCategory(c).subscribe(
        res => {
          console.log(res);
          g.CategoryCode = res;
          console.log(g);

        }
      )
      this.addGmh(g);
    }
    else if (this.gmhForm.controls["tatCategory"].value != null) {//נבחרה תת קטגוריה
      this.tatCategories.forEach(element => {
        if (element.CategoryName === this.gmhForm.controls.tatCategory.value.CategoryName) {
          g.CategoryCode = element.CategoryCode;
          master = element.CategoryCode
        }
      })
      this.addGmh(g);
    }
  }
  addGmh(g) {
    g.GmhName = this.gmhForm.controls.GmhName.value;
    g.Adress = this.currentUser.Adress;
    g.Phone = this.currentUser.Phone;
    g.e_mail = this.currentUser.E_mail;
    g.UserCode = this.currentUser.UserCode;
    g.comments = this.gmhForm.controls.comments.value;
    console.log(g);
    this.gmhService.add(g).subscribe(
      res => {
        console.log(res)
        if (res) {
          this.getMyGmhim();
          this.closeNew();
          this.getCategoryGmh();
          this.gmhForm.reset()
          alert('נוסף בהצלחה')
        }
        else {
          alert('error, try again')
        }
      }
    )
  }
  displayFn(c: CategoryGMH): string {
    return c && c.CategoryName ? c.CategoryName : '';
  }
  getCategoryGmh() {
    this.gmhService.getCategoryGmach().subscribe(res => {
      this.categories = res, console.log(res);
      this.filteredCategories = this.gmhForm.controls.category.valueChanges
        .pipe(
          startWith(''),
          map(value => typeof value === 'string' ? value : value.CategoryName),
          map(name => name ? this._filter(name) : this.categories.slice())
        );
    }
      ,
      err => { console.log(err); }
    );
  }
  getTatCategoriesForGmh(c) {
    //console.log(this.gmhForm.controls["newTatCategory"].disabled);

    if (this.gmhForm.controls["newTatCategory"].disabled) {

      this.gmhService.getCategoriesForGmach(c.option.value).subscribe(res => {
        this.tatCategories = res;
        console.log(res),

          this.filteredTatCategories = this.gmhForm.controls.tatCategory.valueChanges
            .pipe(
              startWith(''),
              map(value => typeof value === 'string' ? value : value.CategoryName),
              map(name => name ? this._filter(name) : this.tatCategories.slice())
            );
        err => { console.log(err); }
      });
    }
  }
  handleDestinationChange(a: Address) {
    this.adress = a.formatted_address;
    // console.log(a)
  }
  newcategory() {
    this.gmhForm.controls["newCategory"].enable();
    this.gmhForm.controls["category"].disable();
    this.gmhForm.controls["tatCategory"].disable();
    this.gmhForm.controls["category"].setValue('');

  }
  newtatcategory() {
    this.gmhForm.controls["newTatCategory"].enable();
    this.gmhForm.controls["tatCategory"].disable();
    this.gmhForm.controls["tatCategory"].setValue('');

    // this.tatCategories=new Array<CategoryGMH>();
  }
  choosetatcategory() {
    this.gmhForm.controls["newTatCategory"].disable();
    this.gmhForm.controls["tatCategory"].enable();
    this.gmhForm.controls["newTatCategory"].setValue('')
  }
  choosecategory() {
    this.gmhForm.controls["newCategory"].disable();
    this.gmhForm.controls["category"].enable();
    this.gmhForm.controls["newCategory"].setValue('')
  }
}

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-edit-owner-details',
  templateUrl: './edit-owner-details.component.html',
  styleUrls: ['./edit-owner-details.component.css']
})
export class EditOwnerDetailsComponent implements OnInit {
editDetailsForm:FormGroup
currentUser
adress
  constructor(private userService:UserService) { }

  ngOnInit(): void {
    this.currentUser=this.userService.CurrentUser;
    this.editDetailsForm = new FormGroup({
      Cell_Phone: new FormControl(),
      Phone: new FormControl(Validators.pattern('[0-9]{9}')),
      E_mail: new FormControl(Validators.email),
      password: new FormControl(),
      Adress: new FormControl(),
      userName: new FormControl()
    });
    this.setDetails();
  }
  setDetails(){
    this.adress=this.currentUser.Adress;
    this.editDetailsForm.controls.userName.setValue(this.currentUser.Name)
    this.editDetailsForm.controls.Adress.setValue(this.currentUser.Adress)
    this.editDetailsForm.controls.Phone.setValue(this.currentUser.Phone)
    this.editDetailsForm.controls.password.setValue(this.currentUser.password)
    this.editDetailsForm.controls.E_mail.setValue(this.currentUser.E_mail)
    
  }
  handleDestinationChange(a: Address) {
    this.adress = a.formatted_address;
    // console.log(a)
  }
  saveChange(){

  }
}

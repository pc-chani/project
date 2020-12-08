import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { User } from 'src/app/shared/models/User.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-edit-owner-details',
  templateUrl: './edit-owner-details.component.html',
  styleUrls: ['./edit-owner-details.component.css']
})
export class EditOwnerDetailsComponent implements OnInit {
  editDetailsForm: FormGroup
  currentUser
  adress
  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.currentUser = this.userService.CurrentUser;
    console.log(this.currentUser);

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
  setDetails() {
    this.adress = this.currentUser.Adress;
    this.editDetailsForm.controls.userName.setValue(this.currentUser.Name)
    this.editDetailsForm.controls.Adress.setValue(this.currentUser.Adress)
    this.editDetailsForm.controls.Phone.setValue(this.currentUser.Phone)
    this.editDetailsForm.controls.Cell_Phone.setValue(this.currentUser.Cell_Phone)

    this.editDetailsForm.controls.password.setValue(this.currentUser.password)
    this.editDetailsForm.controls.E_mail.setValue(this.currentUser.E_mail)
  }
  handleDestinationChange(a: Address) {
    this.adress = a.formatted_address;
    // console.log(a)
  }
  saveChange() {
    let u = new User();
    u.UserCode = this.currentUser.UserCode;
    u.E_mail = this.editDetailsForm.controls.E_mail.value;
    u.Cell_Phone = this.editDetailsForm.controls.Cell_Phone.value;
    u.Phone = this.editDetailsForm.controls.Phone.value;
    //  u.Password = this.editDetailsForm.controls.password.value;
    u.Password = this.currentUser.Password
    u.Adress = this.editDetailsForm.controls.Adress.value;
    u.Name = this.editDetailsForm.controls.userName.value;
    console.log(u);

    this.userService.saveChanges(u).subscribe(
      res => {
        console.log(res)
      
        if (res) {
          if (confirm("רוצה לשנות את הפרטים גם בגמחים")) {
            this.userService.saveChangesInGmhim(u).subscribe(//לשנות תגמחים
              res => {
                console.log(res)
              }
            )
          }
          this.userService.getuser(this.currentUser.UserCode).subscribe(
            res => {
              this.currentUser = res;
              this.userService.setCurrentUser(res);
              localStorage.setItem('user', JSON.stringify(this.userService.CurrentUser));
              console.log(res);
            }
          )
          this.router.navigate(['/manageTheGMH'])
        }
      }
    )
  }
}

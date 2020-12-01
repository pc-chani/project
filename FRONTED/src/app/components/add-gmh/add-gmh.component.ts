import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/User.model';
import { UserService } from 'src/app/shared/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CheckPassword } from 'src/app/validators/valid';
import { Address } from 'ngx-google-places-autocomplete/objects/address';

@Component({
  selector: 'app-add-gmh',
  templateUrl: './add-gmh.component.html',
  styleUrls: ['./add-gmh.component.css']
})
export class AddGMHComponent implements OnInit {
  registerForm: FormGroup;
  adrres: string;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      adress: new FormControl('', Validators.required),
      cell_phone: new FormControl('', Validators.pattern('[0-9]{9}')),
      phone: new FormControl('', Validators.pattern('[0-9]{10}')),
      e_mail: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirm: new FormControl('', Validators.required),
      permission: new FormControl('', Validators.required),
      accept: new FormControl('', Validators.requiredTrue),
    }, { validators: CheckPassword('password', 'confirm') });
  }
  addUser() {
    let user = new User();
    user.Name = this.registerForm.controls.Name.value;
    user.Adress = this.adrres;
    user.Cell_Phone = this.registerForm.controls.cell_phone.value;
    user.Phone = this.registerForm.controls.phone.value;
    user.E_mail = this.registerForm.controls.e_mail.value;
    user.Password = this.registerForm.controls.password.value;
    user.Permission=  this.registerForm.controls.permission.value;
    
   // console.log(user);
    
    this.userService.addUser(user).subscribe(
      res => { console.log(res); },
      err => { console.log(err); }
    )
  }
  handleDestinationChange(a: Address) {
    this.adrres=a.formatted_address;
  //  console.log(a)
  }
  
}

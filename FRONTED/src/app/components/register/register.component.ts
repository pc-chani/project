import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';
import { CheckPassword } from 'src/app/validators/valid';
import { User } from 'src/app/shared/models/User.model';
import { Address } from 'ngx-google-places-autocomplete/objects/address';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private userService: UserService) { }
  registerForm: FormGroup;
  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      adress: new FormControl('', Validators.required),
      cell_phone: new FormControl('', Validators.pattern('[0-9]{9}')),
      phone: new FormControl('', Validators.pattern('[0-9]{10}')),
      e_mail: new FormControl('', Validators.email),
      password: new FormControl('', Validators.required),
      confirm: new FormControl('', Validators.required),
      accept: new FormControl('', Validators.requiredTrue),
    }, { validators: CheckPassword('password', 'confirm') });
  }
  addUser() {
    let user = new User();
    user.FirstName = this.registerForm.controls.firstName.value;
    user.LastName = this.registerForm.controls.lastName.value;
    user.Adress = this.registerForm.controls.adress.value;
    user.Cell_Phone = this.registerForm.controls.cell_phone.value;
    user.Phone = this.registerForm.controls.phone.value;
    user.E_mail = this.registerForm.controls.e_mail.value;
    user.Password = this.registerForm.controls.password.value;
    user.Permission=  'בעל גמ"ח'
    
    console.log(user);
    
    this.userService.addUser(user).subscribe(
      res => { console.log(res); },
      err => { console.log(err); }
    )
  }
  getUser() {
    this.userService.getUser().subscribe(
      res => { console.log(res); },
      err => { console.log(err); }
    )
  }
  handleDestinationChange(a: Address) {
    console.log(a)
  }
}




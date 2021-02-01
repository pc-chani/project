import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';
import { CheckPassword } from 'src/app/validators/valid';
import { User } from 'src/app/shared/models/User.model';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  show: boolean = false;


  constructor(private userService: UserService, private cookieService: CookieService, private router: Router) { }
  registerForm: FormGroup;
  adress
  isExist: boolean;
  ngOnInit(): void {
    this.registerForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      adress: new FormControl('', Validators.required),
      cell_phone: new FormControl('', Validators.compose([Validators.required, Validators.pattern('[0-9]{9}')])),
      phone: new FormControl('', Validators.compose([Validators.required, Validators.pattern('[0-9]{10}')])),
      e_mail: new FormControl('', Validators.compose([Validators.required, , Validators.email])),
      password: new FormControl('', Validators.required),
      confirm: new FormControl('', Validators.required),
      //permission: new FormControl('', Validators.required),
      accept: new FormControl('', Validators.requiredTrue),
    }, { validators: CheckPassword('password', 'confirm') });
    this.isExist = false;
  }
  handleDestinationChange(a: Address) {
    this.adress = a.formatted_address
    console.log(a)
  }
  addUser() {

    let user = new User();
    user.Name = this.registerForm.controls.Name.value;
    user.Adress = this.adress
    user.Cell_Phone = this.registerForm.controls.cell_phone.value;
    user.Phone = this.registerForm.controls.phone.value;
    user.E_mail = this.registerForm.controls.e_mail.value;
    user.Password = this.registerForm.controls.password.value;
    user.Permission = 'בעל גמ"ח'

    console.log(user);
    console.log(this.userService.checkUser(user))
    this.userService.checkUser(user).subscribe(
      res => {
        console.log(res);
        if (res == null) {
          // console.log("here!!!!!!!!!")
          // this.isExist = true;
          confirm("משתמש זה קיים כבר ")
          this.router.navigate(['/signIn'])

        }
        else {
          this.userService.addUser(user).subscribe(
            res => {
              console.log(res);
              // this.cookieService.set('userName', this.userService.CurrentUser.Name);
              // localStorage.setItem('user', JSON.stringify(this.userService.CurrentUser));
              confirm("משתש נוסף בהצלחה")              
              this.router.navigate(['/manageTheGMH'])
            },
            err => {
              confirm("המערכת נתקלה בבעיה בהוספת המשתמש נסה שוב")              
              console.log(err); }
          )
        }
        // this.cookieService.set('userName', this.userService.CurrentUser.Name);
        // localStorage.setItem('user', JSON.stringify(this.userService.CurrentUser));
        this.router.navigate(['/manageTheGMH'])
      },
      err => { console.log(err); }
    )
    
    
  }
  

  toshow() {
    this.show = !this.show
  }
}




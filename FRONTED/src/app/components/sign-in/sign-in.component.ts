import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/User.model';
import { AppComponent } from 'src/app/app.component';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  

  constructor(private userService: UserService, private router: Router,private cookieService: CookieService) { }
  signInForm: FormGroup;
  show:boolean=false;
  ngOnInit(): void {
    this.signInForm = new FormGroup({
      e_mail: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    })
    
  }
  checkUser() {
    let user = new User();
    user.E_mail = this.signInForm.controls.e_mail.value;
   user.Password = this.signInForm.controls.password.value;
    this.userService.checkUser(user).subscribe(
      res => { this.userService.setCurrentUser(res);
        this.cookieService.set('userName', this.userService.CurrentUser.Name);
        localStorage.setItem('user', JSON.stringify(this.userService.CurrentUser));

         console.log(res); 
        if (res != undefined)
         this.router.navigate(['/manageTheGMH'])
      },
    err => { console.log(err); }

    )
  }
  toshow(){
this.show=!this.show
  }
}



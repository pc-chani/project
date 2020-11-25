import { Component } from '@angular/core';
import { from } from 'rxjs';
import { User } from './shared/models/User.model';
import { UserService } from './shared/services/user.service';
import { CookieService } from 'ngx-cookie-service'
import { GmhService } from './shared/services/gmh.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  show = false;
  currentUser: User = this.userService.CurrentUser;
  cookieValue;

  constructor(public userService: UserService, private cookieService: CookieService) { }
  ngOnInit(): void {

    this.cookieValue = this.cookieService.get('userName');
    
    this.currentUser = JSON.parse(localStorage.getItem('user'));
    this.userService.setCurrentUser(this.currentUser)
    console.log(this.cookieValue);
  // if(this.currentUser!=null)
   // this.currentUser.Name = this.cookieValue;
    console.log(this.currentUser);
  }
  message() {
    this.show = true
  }
}

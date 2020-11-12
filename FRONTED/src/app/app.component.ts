import { Component } from '@angular/core';
import { User } from './shared/models/User.model';
import { UserService } from './shared/services/user.service';
import getBrowserFingerprint from 'get-browser-fingerprint';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  show=false;
  currentUser:User=this.userService.CurrentUser;;
  
  constructor( public userService: UserService) { }
  ngOnInit(): void {
    const fingerprint = getBrowserFingerprint();
console.log(fingerprint);

    this.currentUser=this.userService.CurrentUser;
    console.log(this.currentUser);
  }
  message(){
this.show=true
  }
}

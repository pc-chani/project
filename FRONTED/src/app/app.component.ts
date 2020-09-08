import { Component } from '@angular/core';
import { User } from './shared/models/User.model';
import { UserService } from './shared/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  currentUser:User
  
  constructor( private userService: UserService) { }
  ngOnInit(): void {
    this.currentUser=this.userService.currentUser;
    console.log(this.currentUser);
  }
}

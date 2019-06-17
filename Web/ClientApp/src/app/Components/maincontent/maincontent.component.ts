import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd';
import { UserModel } from 'src/app/Models/UserModel';

@Component({
  selector: 'app-maincontent',
  templateUrl: './maincontent.component.html',
  styleUrls: ['./maincontent.component.scss']
})
export class MaincontentComponent implements OnInit {

  cartCount;
  constructor(public authService: AuthenticationService, private router: Router, private message: NzMessageService) {}
     currentUser: UserModel = this.authService.currentUserId;

  ngOnInit() {
  }
  loggedIn() {
    return this.authService.decodedToken;
  }

onLogoutClicked() {
    this.authService.logout();
    this.message.info('logged out');
    this.router.navigate(['/']);
  }

}

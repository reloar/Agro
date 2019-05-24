import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../Services/authentication.service';
import { Router } from '@angular/router';
import { Utility } from '../Services/utility';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  returnUrl: string;
  submitted = false;
  error = '';


  form: FormGroup;
  public RegistrationType: any;
  constructor(private fb: FormBuilder, private message: NzMessageService,
              public authService: AuthenticationService,
              private router: Router, ) { }
  ngOnInit() {
    this.getUserTypes();
    this.form = this.fb.group({
      FirstName: new FormControl('', [Validators.required]),
      LastName: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required]),
      Password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      ConfirmPassword: new FormControl('', [Validators.required, Validators.minLength(6)]),
      RegistrationType: new FormControl('', [Validators.required]),
      PhoneNumber: new FormControl('', [Validators.required])
    });
    this.loginForm = this.fb.group({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    });
  }


  get f() { return this.form.controls; }

  RegisterUser() {
    const user = this.form.value;
    this.authService.Register(user).subscribe(result => {
      if (result) {
        this.message.success('Registration successful');
        this.router.navigate(['/'], { queryParams: { brandNew: true, email: user.Email } });
      }
    }, error => {
      if (Utility.checkNoNetwork(error)) {
        // todo display error message for no network
        this.message.error('Please check your network connection');
      } else {
        const errorMessage = Utility.findHttpResponseMessage('error_description', error);
        if (errorMessage) {
          this.message.error(errorMessage);
        } else {
          this.message.error('Could not retrieve banks');
        }
      }
    });
  }
  getUserTypes() {
    this.authService.GetUserType().subscribe(res => {
      this.RegistrationType = res as any;
    });
  }
  get loginFunction() { return this.loginForm.controls; }
  login() {

    const userLogin = this.loginForm.value;
    console.log(userLogin);
    this.authService.login(userLogin).subscribe((response) => {
      this.loginForm.reset();
      if (response) {
        console.log(this.authService.decodedToken);
        this.message.success('login successful');
        this.router.navigate(['/']);
      }
        }, error => {
          if (Utility.checkNoNetwork(error)) {
            // todo display error message for no network
            this.message.error('Please check your network connection');
          } else {
            const errorMessage = Utility.findHttpResponseMessage('error_description', error);
            if (errorMessage) {
              this.message.error(errorMessage);
            } else {
              this.message.error('Could not login');
            }
          }
        });

  }

  onLogoutClicked() {
    this.authService.logout();
  }
}

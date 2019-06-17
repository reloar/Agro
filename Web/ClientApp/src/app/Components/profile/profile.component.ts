import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { UserModel } from 'src/app/Models/UserModel';
import { environment } from 'src/environments/environment';
import { NzMessageService } from 'ng-zorro-antd';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { Router } from '@angular/router';
import { BankService } from 'src/app/Services/bank.service';
import { Utility } from 'src/app/Services/utility';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  updateForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  updateProfile: UserModel;

  isLoading = false;
  public returnUrl = environment.baseUrl;
  banks: Array<{ label: string; value: string }> = [];
  public RegistrationType: any;
  constructor(private fb: FormBuilder, private message: NzMessageService,
              public authService: AuthenticationService,
              private router: Router, private bankService: BankService) { }

  userId: UserModel = this.authService.currentUserId;
  roles: UserModel = this.authService.userRole;
  username: UserModel = this.authService.userName;
  email: UserModel = this.authService.email;

  ngOnInit() {
    this.getBanks();
    this.updateForm = this.fb.group({
      subAccountName: new FormControl(),
      email: new FormControl({ value: this.email, disabled: true }),
      roles: new FormControl(this.roles),
      fullName: new FormControl({ value: this.username, disabled: true }),
      bankAccount: new FormControl(),
      userId: new FormControl(this.userId),
      bankSortCode: new FormControl(this.banks),
      contactAddress: new FormControl(),
    });
  }

  getBanks() {
    this.bankService.getBanks().subscribe((response) => {
      const bankItem = new Array();
      response.data.forEach((element: { name: any; code: any; }) => {
        bankItem.push({
          label: element.name,
          value: element.code
        });
      //response.data.forEach((element: { bankName: any; bankCode: any; }) => {
      //  bankItem.push({
      //    label: element.bankName,
      //    value: element.bankCode
      //  });
      });
      this.banks = bankItem;
    },
      error => {

        if (Utility.checkNoNetwork(error)) {
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

  get update() { return this.updateForm.controls['message'].value; }
  done() {
    this.isLoading = true;
    this.updateProfile = this.updateForm.value;
    console.log('value', this.updateProfile);
    this.bankService.updateProfile(this.updateProfile).subscribe(response => {
      if (response) {
        this.isLoading = false;
        console.log(response.data.roles);
        if (response.data.roles === 'Farmer') {
          this.router.navigate(['/product']);
        } else {
          this.router.navigate(['/']);
        }
      }
    });
  }

}

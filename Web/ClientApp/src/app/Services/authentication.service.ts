import { UserModel } from './../Models/UserModel';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { JwtHelper } from './jwt-helper';
// import { tokenNotExpired } from 'angular2-jwt';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  url = environment.baseUrl;
  apiurl = environment.defaultRoute;
  userToken: any;
  decodedToken: any;
  jwtHelper: JwtHelper = new JwtHelper();
  currentUserId: any;
  userRole: any;
  email: any;
  userName: any;
  constructor(private httpClient: HttpClient) { }


  Register(user: UserModel): Observable<UserModel> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const options = { headers };

    return this.httpClient.post<UserModel>(`${this.url + this.apiurl}/Account/register`, user)
      .pipe(
        catchError(this.handleError));
  }

  login(model: UserModel) {
    return this.httpClient.post<any>(`${this.url + this.apiurl}/Account/login/`, model)
      .pipe(
        map(user => {
        if (user && user.tokenString) {
          this.decodedToken = this.jwtHelper.decodeToken(user.tokenString);
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserId = this.decodedToken.nameid;
          this.userRole = this.decodedToken.role;
          this.email = this.decodedToken.email;
          this.userName = this.decodedToken.given_name;
        }
        return user;
        }));
  }

  get isLoggedIn(): boolean {
    return this.currentUserId != null;
  }

  GetUserType() {
    return this.httpClient.get(`${this.url + this.apiurl}/Account/register`)
     .pipe(
       catchError(this.handleError));
 }
  logout() {
    localStorage.removeItem('currentUser');
  }
  // loggedIn() {
  //   return tokenNotExpired('currentUser');
  //  }
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      return throwError(
        'We are having issues connecting. Please contact support and describe your issue.'
      );
    } else if (error.status >= 400) {
      console.log(error);
      return throwError(
        'The requested server is unavailable. Please contact  support and describe your issue.'
      );
    } else if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }

    return throwError('Something bad happened. Please try again later.');
  }
}

import { catchError } from 'rxjs/operators';
import { UserModel } from './../Models/UserModel';
import { throwError, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BankService {

  url = environment.baseUrl;
  apiRoute = environment.defaultRoute;
  constructor(private httpClient: HttpClient) { }
  
  getBanks(): Observable<any> {
    return this.httpClient
    .get<any>(`${this.url + this.apiRoute}/user/bank/`)
         .pipe(catchError(this.handleError));
  }

  updateProfile(model: UserModel): Observable<any>
  {
    return this.httpClient.post<any>(`${this.url + this.apiRoute}/user/updateProfile`, model)
    .pipe(
      catchError(this.handleError)
    );
  }
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      return throwError(
        'We are having issues connecting. Please contact support and describe your issue.'
      );
    } else if (error.status >= 400) {
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

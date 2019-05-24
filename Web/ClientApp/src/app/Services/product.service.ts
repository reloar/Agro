import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

import { catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { HttpClient,  HttpErrorResponse } from '@angular/common/http';
import { ProductModel } from '../Models/ProductModel';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  url = environment.baseUrl;
  apiRoute = environment.defaultRoute;
  navbarCartCount = 0;
  constructor(private httpClient: HttpClient) { }

  AddProduct(product: ProductModel): Observable<ProductModel> {

    return this.httpClient.post<ProductModel>(`${this.url + this.apiRoute}/Product/addProduct`, product)
      .pipe(
        catchError(this.handleError));
  }

  GetAllProducts(): Observable<any> {
    return this.httpClient.get(`${this.url + this.apiRoute}/Product/getProducts`)
      .pipe(
        catchError(this.handleError));
  }


  GetOneProduct(productId: any): Observable<any> {
    return this.httpClient.get(`${this.url + this.apiRoute}/Product/getProduct/` + productId)
      .pipe(
        catchError(this.handleError));
  }


  addToCart(data: ProductModel): void {
    let a: ProductModel[];
    a = JSON.parse(localStorage.getItem('avct_item')) || [];
    a.push(data);
    // add an alert
    setTimeout(() => {
      localStorage.setItem('avct_item', JSON.stringify(a));
      this.calculateLocalCartProdCounts();
    }, 500);
  }

  calculateLocalCartProdCounts() {
    this.navbarCartCount = this.getLocalCartProducts().length;
  }

  getLocalCartProducts(): ProductModel[] {
    const products: ProductModel[] = JSON.parse(localStorage.getItem('avct_item')) || [];

    return products;
  }
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

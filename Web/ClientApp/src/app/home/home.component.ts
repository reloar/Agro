import { ApiResponse } from './../Models/ProductModel';
import { Router } from '@angular/router';
import { ProductService } from './../Services/product.service';
import { Component, OnInit } from '@angular/core';

import { FormControl, Validators } from '@angular/forms';
import { ProductModel } from '../Models/ProductModel';
import { NzMessageService } from 'ng-zorro-antd';
import { Utility } from '../Services/utility';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  products: Array<ProductModel> = [];
  // products: Array<{ productId: number; photoUrl: string; productName: string; price: number; quantity: number }> = [];
  // product: any;
  isLoading = false;
   constructor(private router: Router,
               private message: NzMessageService,
               private productService: ProductService) { }

  ngOnInit() {
   this.getProducts();

  }
  getProducts() {
    this.productService.GetAllProducts().subscribe((res: ApiResponse<ProductModel[]>) => {
      if (res.data != null && res.data.length > 0) {
        this.products = res.data;
        console.log('product', this.products);
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
          this.message.error('Could not retrieve publisher adverts');
        }
      }
    });
  }
  getProduct(id: number) {

          console.log(id);
          this.router.navigate([`market/${id}`]);
  }
}

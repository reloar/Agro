import { Router } from '@angular/router';
import { ProductService } from './../Services/product.service';
import { Component, OnInit } from '@angular/core';

import { FormControl, Validators } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  products: Array<{ productId: number; photoUrl: string; productName: string; price: number; quantity: number }> = [];
  product: any;
  isLoading = false;
   constructor(private router: Router, private productService: ProductService) { }

  ngOnInit() {
   this.getProducts();

  }
  getProducts() {
    this.productService.GetAllProducts().subscribe((res) => {
      console.log(res);
      const product = new Array();
      res.data.forEach(r => {
product.push({
          photoUrl: r.photoUrl,
          productName: r.productName,
          price: r.price,
          quantity: r.quantity,
          productId: r.productId
      });
      });
      this.products = product;
    });
  }
  getProduct(id: number) {

          console.log(id);
          this.router.navigate([`market/${id}`]);
  }
}

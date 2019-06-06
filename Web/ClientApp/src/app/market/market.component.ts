import { ProductService } from './../Services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../Services/cart.service';
import { ProductOrderModel } from '../Models/ProductModel';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss']
})
export class MarketComponent implements OnInit {
  product: any;
  buyForm: FormGroup;
  productOrder: ProductOrderModel[] = [];
  public totalSum = 0;

  isLoading = false;
  constructor(
    private route: ActivatedRoute, private fb: FormBuilder, private productService: ProductService,
    private cartService: CartService) { }

  

  ngOnInit() {

    
    const id = parseInt(this.route.snapshot.paramMap.get('id'), 10);
    this.getProduct(id);
    this.buyForm = this.fb.group({
      price: new FormControl(),
      storeCharge: new FormControl(1000),
      fee: new FormControl(),
      quantity: new FormControl(),
      charges: new FormControl(1000),
      total: new FormControl()
    });


  }


get purchaseFunction() { return this.buyForm.controls; }

  Buy() {
    const purchase = this.buyForm.value;
    
    console.log(purchase);
    console.log(purchase);
  }

  stockAdd(stocks: ProductOrderModel) {
    stocks.added = true;
    this.cartService.addStock(stocks);
  }


  getProduct(id: number) {
    this.productService.GetOneProduct(id).subscribe(
      (response) => {
        if (response.data != null && response.data != null) {
          this.product = response.data;
          console.log(this.product);
        }
        this.isLoading = false;

        return true;
      },
      error => {
        return false;
      }
    );
  }
}

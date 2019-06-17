import { ProductModel, ApiResponse } from './../Models/ProductModel';
import { UserModel } from './../Models/UserModel';
import { AuthenticationService } from './../Services/authentication.service';
import { ProductService } from './../Services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../Services/cart.service';
import { ProductOrderModel, Product } from '../Models/ProductModel';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss']
})
export class MarketComponent implements OnInit {
  public title = 'Agrovesto';
  tRef = '';
  product: ProductModel;
  buyForm: FormGroup;
  productOrder: ProductOrderModel[] = [];
  //public total = 500000;
  //email = '';
  private items: Product[] = [];

  isLoading = false;
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private productService: ProductService,
    private cartService: CartService, public authService: AuthenticationService) { }

  currentUser: UserModel = this.authService.currentUserId;
  //currentUserEmail: string = this.authService.email;
  setRandomPaymentRef() {
    this.tRef = `${Math.random() * 10000000000000}`;
  }
  ngOnInit() {
    this.setRandomPaymentRef();
    const id = parseInt(this.route.snapshot.paramMap.get('id'), 10);
    this.getProduct(id);
    this.buyForm = this.fb.group({
      price: new FormControl(),
      storeCharge: new FormControl(1000),
      fee: new FormControl(),
      quantity: new FormControl(),
      charges: new FormControl(1000),
      total: new FormControl(),
      userId: new FormControl(this.currentUser),
      reference: new FormControl(this.setRandomPaymentRef())
    });


  }

//loadCart(): void {
//  this.total = 0;
//  this.items = [];
//  let cart = JSON.parse(localStorage.getItem('cart'));
//  for (var i =0; i < cart.length; i++) {
//    let item = JSON.parse(cart[i]);
//    this.items.push({
//      product: item.product,
//      quantity: item.quantity
//    });
//    this.total += item.product.price * item.quantity;
//  }
//}
//remove(id: number): void {
//  let cart: any = JSON.parse(localStorage.getItem('cart'));
//  let index: number = -1;
//  for (var i = 0; i < cart.length; i++) {
//    let item: Product = JSON.parse(cart[i]);
//    if (item.product.productId === id) {
//      cart.splice(i, 1);
//      break;
//    }
//  }
//  localStorage.setItem("cart", JSON.stringify(cart));
//  this.loadCart();
//}
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

  paymentDone(ref: any) {
    this.title = 'Payment successfull';
    console.log(this.title, ref);
  }

  paymentCancel() {
    this.title = 'Payment failed';
    console.log(this.title);
  }
  getProduct(id: number) {
    this.productService.GetOneProduct(id).subscribe(
      (response: ApiResponse<ProductModel>) => {
        if (response.data != null && response.data != null) {
          this.product = response.data;
          console.log(this.product.price);
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

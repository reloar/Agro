
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CartState, ProductOrderModel } from '../Models/ProductModel';
import { NzMessageService } from 'ng-zorro-antd';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  private cartSubject = new Subject<CartState>();
  productOrder: ProductOrderModel[] = [];

  CartState = this.cartSubject.asObservable();
  constructor(private message: NzMessageService) { }

  addStock(product: any) {
    const currentstock = this.productOrder.find(x => x.id === product.id);
    if (!currentstock) {
      this.productOrder.push(product);
      this.cartSubject.next({ loaded: true, productOrder: this.productOrder } as CartState);
    }
    else {
      this.message.error('product added to cart');
    }
  }

  removeProduct(id: number) {
    this.productOrder = this.productOrder.filter((x) => x.id !== id);
    this.cartSubject.next({ loaded: false, productOrder: this.productOrder } as CartState);
  }

  addProductUpdate(item: ProductOrderModel) {
    const currentstock = this.productOrder.find(x => x.id === item.id);
    if (currentstock) {
      currentstock.store = item.store;
      currentstock.quantity = item.quantity;
      currentstock.commission = item.commission;
      this.cartSubject.next({ loaded: true, productOrder: this.productOrder } as CartState);


    }

  }
}

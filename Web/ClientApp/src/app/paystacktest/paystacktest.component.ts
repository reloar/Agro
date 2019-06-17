import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { PaystackOptions } from '../Models/PaystackOptions';

interface MyWindow extends Window {
  PaystackPop: any;
}
declare var window: MyWindow;

@Component({
  selector: 'app-paystacktest',
  templateUrl: './paystacktest.component.html',
  styleUrls: ['./paystacktest.component.scss']
})
export class PaystacktestComponent implements OnInit, OnChanges {
  @Input() text: string;
  @Input() key = 'pk_test_6da80bb925e2a4559e9ab5f428435505c6f3ced7';
  @Input() email = 'aremu@gmail.com';
  @Input() amount = 50000;
  @Input() metadata: {};
  @Input() ref = 're23456';
  @Input() currency = 'NGN';
 // @Input() plan: string;
  @Input() quantity = '4';
  @Input() channels: string[];
 // @Input() subaccount: string;
  // tslint:disable-next-line:variable-name
  @Input() transaction_charge: number;
  @Input() bearer: string;
  @Input() class: string;
  @Input() style: object;
  @Output() paymentInit: EventEmitter<any> = new EventEmitter<any>();
  @Output() close: EventEmitter<any> = new EventEmitter<any>();
  @Output() callback: EventEmitter<any> = new EventEmitter<any>();
  private paystackOptions: PaystackOptions;
  private isPaying = false;
  constructor() {}

  async pay() {
    if (!this.checkInput()) { return; }
    await this.loadScript();
    this.setUp();
    if (this.isPaying) { return; }
    if (this.paymentInit.observers.length) {
      this.paymentInit.emit();
    }
    const payment = window.PaystackPop.setup(this.paystackOptions);
    payment.openIframe();
    this.isPaying = true;
  }
  checkInput() {
    if (!this.key) { return console.error('ANGULAR-PAYSTACK: Paystack key cannot be empty'); }
    if (!this.email) { return console.error('ANGULAR-PAYSTACK: Paystack email cannot be empty'); }
    if (!this.amount) { return console.error('ANGULAR-PAYSTACK: Paystack amount cannot be empty'); }
    if (!this.ref) { return console.error('ANGULAR-PAYSTACK: Paystack ref cannot be empty'); }
    if (!this.callback.observers.length) {
      return console.error(
        `ANGULAR-PAYSTACK: Insert a callback output like so (callback)='PaymentComplete($event)' to check payment status`
      );
    }
    return true;
  }

  setUp() {
    this.paystackOptions = {
      key: 'pk_test_6da80bb925e2a4559e9ab5f428435505c6f3ced7', // this.key ,
      email: this.email ,
      amount: this.amount ,
      ref: this.ref ,
      metadata: this.metadata || {},
      currency: this.currency || 'NGN' ,
     // plan: this.plan || '' ,
      channels: this.channels || ['card', 'bank'],
      quantity: this.quantity || '' ,
     // subaccount: this.subaccount || '' ,
      transaction_charge: this.transaction_charge || 0 ,
      bearer: this.bearer || '' ,
      callback: (res) => {
        this.isPaying = false;
        this.callback.emit(res);
      },
      onClose: () => {
        this.isPaying = false;
        this.close.emit();
      },
    };
  }

  loadScript(): Promise<void> {
    return new Promise(resolve => {
      if (window.PaystackPop && typeof window.PaystackPop.setup === 'function') {
        resolve();
        return;
      }
      const script = window.document.createElement('script');
      window.document.head.appendChild(script);
      const onLoadFunc = () => {
        script.removeEventListener('load', onLoadFunc);
        resolve();
      };
      script.addEventListener('load', onLoadFunc);
      script.setAttribute('src', 'https://js.paystack.co/v1/inline.js');
    });
  }

  ngOnChanges() {
    this.setUp();
  }

  ngOnInit() {
    if (this.text) {
      console.error(
        'Paystack Text input is deprecated. Add text into textnode like so <angular4-paystack>Pay With Paystack</angular4-paystack>'
      );
    }
  }


}

import { ProductService } from './../../Services/product.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { NzMessageService } from 'ng-zorro-antd';
import { Utility } from 'src/app/Services/utility';



@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  form: FormGroup;

  isLoading = false;
constructor(private fb: FormBuilder, private productService: ProductService,
            public authservice: AuthenticationService,
            private message: NzMessageService, private cd: ChangeDetectorRef) {
  }
  ngOnInit() {
    this.form = this.fb.group({
      userId: new FormControl(this.authservice.currentUserId),
      quantity: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
      productName: new FormControl('', [Validators.required]),
      photoUrl: new FormControl('', [Validators.required])
    });
  }

  onFileChange(event) {
    let reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.form.patchValue({
          file: reader.result
        });

        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

  get f() { return this.form.controls; }

  addProduct() {
    const product = this.form.value;
    console.log(product);
    this.productService.AddProduct(product).subscribe(res => {
     this.form.reset();
     if(res) {
       this.message.success('Product Added succesfully');
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
         this.message.error('Could not add product to catalog');
       }
     }
    });
  }
}

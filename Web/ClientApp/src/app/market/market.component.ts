import { ProductService } from './../Services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss']
})
export class MarketComponent implements OnInit {
  product: any;
  isLoading = false;
  constructor(
    private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit() {
    const id = parseInt(this.route.snapshot.paramMap.get('id'), 10);
    this.getProduct(id);
 
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

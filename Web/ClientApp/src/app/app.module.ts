import { BankService } from './Services/bank.service';
import { ProductService } from './Services/product.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule, NZ_I18N, en_US } from 'ng-zorro-antd';

import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponent } from './Components/product/product.component';
import { ContactComponent } from './contact/contact.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { TeamComponent } from './team/team.component';
import { FeaturesComponent } from './features/features.component';
import { AboutComponent } from './about/about.component';
import { HowItWorksComponent } from './how-it-works/how-it-works.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MarketComponent } from './market/market.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationService } from './Services/authentication.service';
import { CartService } from './Services/cart.service';
import { ProfileComponent } from './Components/profile/profile.component';
import { MaincontentComponent } from './Components/maincontent/maincontent.component';
import { SidemenuComponent } from './Components/sidemenu/sidemenu.component';
import { Angular4PaystackModule } from 'angular4-paystack';
import { PaystacktestComponent } from './paystacktest/paystacktest.component';
import { StoreOrderComponent } from './Components/store-order/store-order.component';
import { DeliverOrderComponent } from './Components/deliver-order/deliver-order.component';
// import { SidemenuComponent } from './Components/sidemenu/sidemenu.component';
// import { MainContentComponent } from './Components/main-content/main-content.component';
@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ContactComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    TeamComponent,
    FeaturesComponent,
    AboutComponent,
    HowItWorksComponent,
    MarketComponent,
     ProfileComponent,
     MaincontentComponent,
     SidemenuComponent,
     PaystacktestComponent,
     StoreOrderComponent,
     DeliverOrderComponent
    // MainContentComponent,
    // SidemenuComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgZorroAntdModule,
    BrowserAnimationsModule,
    Angular4PaystackModule,
    MDBBootstrapModule.forRoot()
  ],
  providers: [
    ProductService,
    AuthenticationService,
    CartService,
    BankService,
    { provide: NZ_I18N, useValue: en_US },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

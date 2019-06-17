import { HowItWorksComponent } from './how-it-works/how-it-works.component';
import { AboutComponent } from './about/about.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './Components/product/product.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { MarketComponent } from './market/market.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { PaystacktestComponent } from './paystacktest/paystacktest.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'product', component: ProductComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'about', component: AboutComponent},
  {path: 'market/:id', component: MarketComponent},
  { path: 'howitworks', component: HowItWorksComponent},
  { path: 'user/profile', component: ProfileComponent },
  { path: 'paystack', component: PaystacktestComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

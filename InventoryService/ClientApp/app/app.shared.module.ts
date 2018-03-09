import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent } from './components/app/app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './components/home/home.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductService } from './components/products/productservice';
import { ProductEditComponent } from './components/products/product-edit.component';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ProductsComponent,
        ProductEditComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        AppRoutingModule
    ],
    providers: [ProductService]
})
export class AppModuleShared {
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ProductService } from '../products/ProductService';
import { IProduct } from '../shared/Product';

@Component({
    templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {

    pageTitle = "Products"
    products: IProduct[] = [];
    filteredProducts: IProduct[];

    _listFilter: string;
    get listFilter(): string {
        return this._listFilter;
    }
    set listFilter(value: string) {
        this._listFilter = value;
        this.filteredProducts = this.listFilter ? this.performFilter(this.listFilter) : this.products;
    }

    constructor(private router: Router,
        private productService: ProductService) { }

    ngOnInit() {
        this.listFilter = '';
        this.getProducts();
    }

    getProducts() {
        this.productService.getProducts()
            .subscribe((response: IProduct[]) => {
                this.products = response;
                this.filteredProducts = this.products;
            },
            (err: any) => console.log(err),
            () => console.log('getProductsPage() retrieved customers'));
    }

    performFilter(filterBy: string): IProduct[] {
        filterBy = filterBy.toLocaleLowerCase();
        return this.products.filter((product: IProduct) =>
            product.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
    }
}
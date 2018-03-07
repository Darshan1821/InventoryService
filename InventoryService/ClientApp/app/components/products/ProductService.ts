import { Injectable } from '@angular/core';
import { IProduct } from '../shared/IProduct';
import { Http, Headers, Response, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class ProductService {

    baseUrl: string = '/api/products';

    constructor(private http: Http) {}

    getProducts(): Observable<IProduct[]> {
        return this.http.get(this.baseUrl)
            .map((res: Response) => {
                let products = res.json();
                return products;
            });
    }

    getProduct(id: number): Observable<IProduct> {
        return this.http.get(this.baseUrl)
            .map((res: Response) => {
                let products = res.json();
                return products;
            });
    }
}
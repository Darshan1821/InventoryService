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
        return this.http.get(this.baseUrl + "/" + id)
            .map((res: Response) => {
                let products = res.json();
                return products;
            });
    }

    createProduct(product: IProduct): Observable<IProduct> {
        return this.http.post(this.baseUrl, product)
            .map((res: Response) => {
                const data = res.json();
                console.log("Product created: " + data.status);
                return data.product;
            })
            .catch(this.handleError);
    }

    updateProduct(product: IProduct): Observable<IProduct> {
        return this.http.put(this.baseUrl + "/" + product.id, product)
            .map((res: Response) => {
                const data = res.json();
                console.log("Product updated: " + data.status);
                return data.product;
            })
            .catch(this.handleError);
    }

    deleteProduct(id?: number): Observable<boolean> {
        return this.http.delete(this.baseUrl + "/" + id)
            .map((res: Response) => {
                const data = res.json();
                console.log("Product deleted: " + data.status);
                return data.status;
            })
            .catch(this.handleError);
    }

    private handleError(error: any) {
        console.error('server error:', error);
        if (error instanceof Response) {
            let errMessage = "";
            try {
                errMessage = error.json().error;
                if (errMessage == null) {
                    errMessage = error.statusText !== null ? error.statusText : "";
                }
            } catch (err) {
                errMessage = error.statusText !== null ? error.statusText : "";
            }
            return Observable.throw(errMessage);
        }
        return Observable.throw(error || 'ASP.NET Core server error');
    }
}
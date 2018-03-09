import { OnInit, Component } from "@angular/core";
import { IProduct, ProductType } from "../shared/IProduct";
import { Router, ActivatedRoute } from "@angular/router";
import { ProductService } from "./productservice";

@Component({
    templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {

    product: IProduct = {
        name: '',
        price: 0,
        quantity: 0,
        type: ProductType.Cruciferous
    };

    errorMessage: string;
    operationText: string = 'Add';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private productService: ProductService) { }

    ngOnInit(): void {
        const id = this.route.snapshot.params['id'];
        if (id !== '0') {
            this.operationText = 'Update';
            this.getProduct(id);
        }
    }

    cancel(event: Event) {
        event.preventDefault();
        this.router.navigate(['/products']);
    }

    getProduct(id: number): void {
        this.productService.getProduct(id).subscribe((product: IProduct) => {
            this.product = product;
        }, (err: any) => console.log(err));
    }

    delete(event: Event): void {
        event.preventDefault();
        this.productService.deleteProduct(this.product.id).subscribe((status: boolean) => {
            if (status) {
                this.router.navigate(['/products']);
            } else {
                this.errorMessage = "Unable to delete product";
            }
        }), (err: any) => console.log(err);
    }

    submit(): void {

        if (this.product.id) {
            this.productService.updateProduct(this.product).subscribe((product: IProduct) => {
                if (product) {
                    this.router.navigate(['/products']);
                } else {
                    this.errorMessage = "Unable to save product";
                }
            }), (err: any) => console.log(err);
        } else {
            this.productService.createProduct(this.product).subscribe((product: IProduct) => {
                if (product) {
                    this.router.navigate(['/products']);
                } else {
                    this.errorMessage = "Unable to add product";
                }
            }), (err: any) => console.log(err);
        }
    }
}
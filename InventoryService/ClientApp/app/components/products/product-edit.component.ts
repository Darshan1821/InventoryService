import { OnInit, Component } from "@angular/core";
import { IProduct, ProductType } from "../shared/IProduct";
import { Router, ActivatedRoute } from "@angular/router";
import { ProductService } from "./ProductService";

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
    deleteMessageEnabled: boolean;
    operationText: string = 'Create';
    productType: string[] = ['Cruciferous', 'LeafyGreen', 'Marrow', 'Root'];

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

    getProduct(id: number): void {
        this.productService.getProduct(id).subscribe((product: IProduct) => {
            this.product = product;
        }, (err: any) => console.log(err));
    }
}
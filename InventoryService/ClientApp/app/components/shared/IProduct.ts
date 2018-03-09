export interface IProduct {
    id?: number;
    name: string;
    price: number;
    quantity: number;
    type: ProductType;
}

export enum ProductType {
    Cruciferous,
    LeafyGreen,
    Marrow,
    Root
}
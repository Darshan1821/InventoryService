export interface IProduct {
    id?: string;
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
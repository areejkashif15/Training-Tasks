export interface Product{
    id: number; 
    name: string;
    description: string;
    price: number;
    categoryId: number;
    categoryName?: string; // Optional, can be populated when fetching product details
}

export interface CreateProduct {
    name: string;
    description?: string;
    price: number;
    categoryId: number;
}

export interface ProductUpdate {
    name: string;
    description?: string;
    price: number;
    categoryId: number;
}

export interface UpdateProductPrice {
    price: number;
}


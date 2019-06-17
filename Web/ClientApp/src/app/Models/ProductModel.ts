export class ProductModel {
  price: number;
  productName: string;
  photoUrl: string;
  quantity: number;
  productId: number;
  userId: string;
}


export class Product {
  product: ProductModel;
  quantity: number;
}
export interface CartState {

  loaded: boolean;
  productOrder: ProductOrderModel[];

}
export interface ProductOrderModel {
  id: number;
  name: string;
  productId: number;
  quantity: number;
  price: number;
  added: boolean;
  buyerId: number;
  store: boolean;
  commission: number;
}
export interface ApiResponse<T> {
  statusCode?: number | undefined;
  message?: string | undefined;
  data?: T | undefined;
  errors?: string[] | undefined;
}

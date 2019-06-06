export class ProductModel {
  price: number;
  productName: string;
  photoUrl: string;
  quantity: string;
  id: number;
  userId: string;
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

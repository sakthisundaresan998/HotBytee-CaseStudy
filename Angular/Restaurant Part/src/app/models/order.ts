export interface OrderItem {
  orderItemId: number;
  menuItemId: number;
  itemName: string;
  unitPrice: number;
  quantity: number;
}

export interface Order {
  orderId: number;
  userId: number;
  totalAmount: number;
  orderDate: string;
  status: string;
  shippingAddress: string;
  items: OrderItem[];
}

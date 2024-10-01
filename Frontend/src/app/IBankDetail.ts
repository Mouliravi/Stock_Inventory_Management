export interface IBankDetail {
  bankDetailsId?: number;
  bankName: string;
  ifsc: string;
  mcir: number;
  accountNumber: number;
  accountBalance: number;
  createdAt?: Date;
  updatedAt?: Date;
}

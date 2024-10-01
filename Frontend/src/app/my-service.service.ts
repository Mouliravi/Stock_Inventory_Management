import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserDetail } from './IUserDetail';
import { IBankDetail } from './IBankDetail';
@Injectable({
  providedIn: 'root'
})
export class MyServiceService {
private baseUrl1 = 'http://localhost:5233/api/UserDetailAPI';
private baseUrl2 = 'http://localhost:5233/api/BankDetailAPI';
userId : any;
userDetail : any;
bankDetail : any;
bankDetailsId : any;
statusMssg : any;
constructor(private httpClient : HttpClient) { }
sendUserId(userId : any) : void{
  this.userId = userId;
}
sendBankDetailsId(bankDetailsId : any) : void{
  this.bankDetailsId = bankDetailsId;
}
receiveBankDetailsId():any{
  return this.bankDetailsId;
}
receiveUserId():any{
  return this.userId;
}
sendUserDetail(userDetail : any) : void{
  this.userDetail = userDetail;
}
sendBankDetail(bankDetail : any) : void{
  this.bankDetail = bankDetail;
}
receiveUserDetail():any{
  return this.userDetail;
}
receiveBankDetail():any{
  return this.bankDetail;
}
addUserDetail(userDetail: any): Observable<any> {
  return this.httpClient.post<any>(`${this.baseUrl1}`, userDetail);
}
addBankDetail(bankDetail: any): Observable<any> {
  return this.httpClient.post<any>(`${this.baseUrl2}`, bankDetail);
}
fetchUserDetailById(Id : any) : Observable<any> {
  return this.httpClient.get<any>(`${this.baseUrl1}/${Id}`)
}
fetchUserDetails() : Observable<any []>{
  return this.httpClient.get<any []>(this.baseUrl1)
}
fetchBankDetails(): Observable<any []>{
  return this.httpClient.get<any []>(this.baseUrl2)
}
fetchBankDetailById(Id : any) : Observable<any> {
  return this.httpClient.get<any>(`${this.baseUrl2}/${Id}`)
}
editBankDetails(bankDetail : any) : Observable<any>{
  return this.httpClient.put<any>(`${this.baseUrl2}/${bankDetail.BankDetailsId}`, bankDetail)
}
editUserDetails(userDetail : any, userId : any) : Observable<any>{
  return this.httpClient.put<any>(`${this.baseUrl1}/${userId}`, userDetail)
}
deleteBankDetails(bankDetailsId : any):Observable<any>{
  return this.httpClient.delete<any>(`${this.baseUrl2}/${bankDetailsId}`)
}
deleteUserDetails(userId : any):Observable<any>{
  return this.httpClient.delete<any>(`${this.baseUrl1}/${userId}`)
}
filterByAnnualIncome(annualIncome: any): Observable<any[]> {
  return this.httpClient.get<any[]>(`${this.baseUrl1}/filterByAnnualIncome?annualIncome=${annualIncome}`);
}
filterByState(stateId: any): Observable<any[]> {
  return this.httpClient.get<any[]>(`${this.baseUrl1}/filterByState?StateId=${stateId}`);
}
filterByAge(age: any): Observable<any[]> {
  return this.httpClient.get<any[]>(`${this.baseUrl1}/filterByAge?Age=${age}`);
}
filterByGender(genderId: any): Observable<any[]> {
  return this.httpClient.get<any[]>(`${this.baseUrl1}/filterByGender?GenderId=${genderId}`);
}
filterByMaritalStatus(maritalStatusId: any): Observable<any[]> {
  return this.httpClient.get<any[]>(`${this.baseUrl1}/filterByMaritalStatus?MaritalStatusId=${maritalStatusId}`);
}
userOrderDetail(id : any):Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/userOrderDetail?Id=${id}`)
}
addStockDetail(id : any, stockDetail : any): Observable<any>{
  return this.httpClient.post<any>(`${this.baseUrl1}/addStockDetails?Id=${id}`,stockDetail)
}
roles() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/roles`);
}
nationalities() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/nationalities`);
}
cities() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/cities`);
}
states() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/states`);
}
countries() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/countries`);
}
genders() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/genders`);
}
maritalStatuses() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/marritalStatuses`);
}
brokers() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/brokers`);
}
providers() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/providers`);
}
investmentTypes() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/investmentTypes`);
}
stocks() : Observable<any []>{
  return this.httpClient.get<any []>(`${this.baseUrl1}/orders`);
}
sendTransactionStatus(mssg : any){
  this.statusMssg = mssg;
}
receiveTransactionStatus(): string {
  const currentStatus = this.statusMssg;
  this.statusMssg = "";  // Reset the status after returning it
  return currentStatus;
}
}


import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { user } from '../model/user.model';
import {JwtHelperService} from  '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AdminloginService {
  apiUrl:string='http://localhost:5275/api';
  imageUrl:string='http://localhost:5275';

  currentUser : BehaviorSubject<any> = new BehaviorSubject(null);
  currentUserName : BehaviorSubject<any> = new BehaviorSubject(null);
  currentUserData:any;
  public userPayLoad:any;
  jwthelperService = new JwtHelperService();

  constructor(public http:HttpClient) {
    this.userPayLoad = this.decodedToken();
  }

  registerUser(user:user){
      return this.http.post(`${this.apiUrl}/AdminUser/AddUser`,user,{responseType:'json'});
  }

  GetUserById(id: number): Observable<user> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.get<user>(`${this.apiUrl}/AdminUser/UserById`,{params,observe:'body'});
  }
  
  UpdateUser(data: user) {
    //debug
    console.log(data);
    return this.http.put(`${this.apiUrl}/AdminUser/UpdateUser`, data,{responseType:'json'});
  }

  loginUser(loginInfo:Array<string>){
    return this.http.post(`${this.apiUrl}/Login`,{
      Email : loginInfo[0],
      Password : loginInfo[1]
    },{responseType:'json'});
  }

  ForgotPasswordEmailCheck(data:any){
    return this.http.post(`${this.apiUrl}/ForgotPassword`,data);
  }

  ResetPassword(data:any){
      return this.http.post(`${this.apiUrl}/ResetPassword`,data,{responseType:'text'});
  }

  ChangePassword(data:any){
    return this.http.post(`${this.apiUrl}/ChangePassword`,data);
}

  getToken(){
    return localStorage.getItem("access_Token");
  }
  setToken(token:string){
    localStorage.setItem("access_Token",token);
  }

  isLoggedIn():boolean{
      return localStorage.getItem("access_Token") ? true : false;
  }

  LoggedOut(){
    localStorage.removeItem("access_Token");
  }

  public getCurrentUser(){
    return this.currentUser.asObservable();
  }
  public setCurrentUser(userDetail:any){
    return this.currentUser.next(userDetail);
  }
  decodedToken(){
    const token = this.getToken();
    return this.jwthelperService.decodeToken(token);
  }
  getUserFullName(){
    if(this.userPayLoad)
      return this.userPayLoad.fullName;
  }
  getUserDetail(){
    if(this.userPayLoad)
      return this.userPayLoad;
  }

}

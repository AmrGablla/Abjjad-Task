import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { baseUrl } from 'src/environments/environment';
import { User } from 'src/Models/User';
import { UserForAuthenticationDto } from 'src/_interfaces/UserForAuthenticationDto';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  prefix = 'accounts';

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  public loginUser = (body: UserForAuthenticationDto) => {
    return this.http.post<ApiResponse>(`${baseUrl}/${this.prefix}/Login`, body)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user.data);
        return user;
      }));
  }

  islogedIn() {

  }


  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

}

export interface ApiResponse<T = any> {
  pageNumber: number;
  pageSize: number;
  succeeded: boolean;
  message: string;
  errors: string;
  data: T;
}


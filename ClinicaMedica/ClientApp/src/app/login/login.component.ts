import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import * as jwtDecode from 'jwt-decode';
import * as moment from 'moment';
import { AuthService } from '../shared/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private baseURL: string;
  constructor(private httpClient: HttpClient,
    private authService: AuthService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) { 
      this.baseURL = baseUrl;
    }

  ngOnInit() {
  }

  async onCheckLogin(formData: any) {
    this.httpClient.post(this.baseURL + 'api/user/login', { login: formData.login, senha: formData.password})
    .toPromise()
    .then((result: any) => {
        if (result.token) {
          //this.updateUserNameStorage(objLogin.userName, objLogin.rememberUser);
          this.authService.setSession(result);
          this.router.navigate([''])
      }
    });
  }
}

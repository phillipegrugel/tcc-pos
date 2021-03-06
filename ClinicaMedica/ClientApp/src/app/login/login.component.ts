import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import * as jwtDecode from 'jwt-decode';
import * as moment from 'moment';
import { AuthService } from '../shared/auth.service';

declare var $: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private baseURL: string;
  public loading: Boolean = true;
  constructor(private httpClient: HttpClient,
    private authService: AuthService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) { 
      this.baseURL = baseUrl;
    }

  ngOnInit() {
    $(document).ready(() => {
      $('.po-page-background-footer-select').remove();
    });
  }

  async onCheckLogin(formData: any) {
    this.loading = false;
    this.httpClient.post(this.baseURL + 'api/user/login', { login: formData.login, senha: formData.password})
    .toPromise()
    .then((result: any) => {
        if (result.token) {
          //this.updateUserNameStorage(objLogin.userName, objLogin.rememberUser);
          this.authService.setSession(result);
          this.router.navigate(['']);
          this.loading = true;
      }
    })
    .catch((error) => {
      this.loading = true;
    });
  }
}

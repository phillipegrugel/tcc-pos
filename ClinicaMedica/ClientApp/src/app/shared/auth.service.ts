
import * as moment from 'moment';
import { Injectable } from '@angular/core';
import * as jwtDecode from 'jwt-decode';
/**
 * @description
 *
 * Serviço de autenticação do usuário
 */
@Injectable()
export class AuthService {


    public isLogged() {
        return localStorage.getItem('token') !== null && !this.tokenExpired();
      }
    
      getExpiration() {
        const expiration = localStorage.getItem('token_expires_at');
        const expiresAt = JSON.parse(expiration);
    
        return moment(expiresAt);
      }

      public setSession(result: any): void {
        localStorage.setItem('token', result.token);
        const payload: JWTPayload = jwtDecode(result.token) as JWTPayload;
        const expiresAt = moment.unix(payload.exp);
        localStorage.setItem('token_expires_at', JSON.stringify(expiresAt.valueOf()));
      }

      tokenExpired() {
        return !moment().isBefore(this.getExpiration());
    }

    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('token_expires_at');
    }
}

interface JWTPayload {
    sub: string;
    nbf: number;
    exp: number;
  }
  
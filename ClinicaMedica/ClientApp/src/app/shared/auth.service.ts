
import * as moment from 'moment';
import { Injectable, Output, EventEmitter } from '@angular/core';
import * as jwtDecode from 'jwt-decode';
/**
 * @description
 *
 * Serviço de autenticação do usuário
 */
@Injectable()
export class AuthService {

  @Output() fezLogin: EventEmitter<any> = new EventEmitter();


    public isLogged() {
        return localStorage.getItem('token') !== null && !this.tokenExpired();
      }

    public isMedico() {
      return localStorage.getItem('user_role') !== null && localStorage.getItem('user_role') == 'medico';
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
        localStorage.setItem('user_role', result.usuario.role);
        this.fezLogin.emit();
      }

      tokenExpired() {
        return !moment().isBefore(this.getExpiration());
    }

    logout() {
      if (this.isLogged()) {
        localStorage.removeItem('token');
        localStorage.removeItem('token_expires_at');
        localStorage.removeItem('user_role');
      }
    }
}

interface JWTPayload {
    sub: string;
    nbf: number;
    exp: number;
  }
  
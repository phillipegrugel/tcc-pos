import { Component, Inject } from '@angular/core';

import { PoMenuItem } from '@portinari/portinari-ui';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './shared/auth.service';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  readonly menus: Array<PoMenuItem> = [
    { label: 'Home', link: '/', shortLabel: 'Home', icon: 'po-icon-home' },
    { label: 'Profissional', link: '/profissional', shortLabel: 'Profissional', icon: 'po-icon-users' },
    { label: 'Paciente', link: '/paciente', shortLabel: 'Paciente', icon: 'po-icon-user' },
    { label: 'Remedio', link: '/remedio', shortLabel: 'Remedio', icon: 'po-icon-server' },
    { label: 'Consulta', link: '/consulta', shortLabel: 'Consulta', icon: 'po-icon-clock' },
    { label: 'Exame', link: '/exame', shortLabel: 'Exame', icon: 'po-icon-exam' },
    { label: 'Consulta rápida', link: '/consulta-rapida', shortLabel: 'Rápida', icon: 'po-icon-change' },
    { label: 'Sair', action: this.logOut.bind(this), icon: 'po-icon-exit', shortLabel: 'Sair' }
  ];

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private authService: AuthService) {
  }

  private logOut() {
    this.authService.logout();
  }

  public isLogged() {
    return this.authService.isLogged();
  }
}

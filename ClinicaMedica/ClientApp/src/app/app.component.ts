import { Component, Inject } from '@angular/core';

import { PoMenuItem } from '@portinari/portinari-ui';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  readonly menus: Array<PoMenuItem> = [
    { label: 'Profissional', link: '/profissional', shortLabel: 'Profissional', icon: 'po-icon-users' },
    { label: 'Paciente', link: '/paciente', shortLabel: 'Paciente', icon: 'po-icon-user' }
  ];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  private onClick() {
    alert('Clicked in menu item')
  }

}

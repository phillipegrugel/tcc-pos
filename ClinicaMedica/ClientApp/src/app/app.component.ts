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
    { label: 'Home', link: '/profissional', shortLabel: 'Home', icon: 'po-icon-home' }
  ];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  private onClick() {
    alert('Clicked in menu item')
  }

}

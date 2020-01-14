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
    // var profissional = {
    //   numeroCarteiraTrabalho:null,
    //   crm:null,
    //   tipo:0,
    //   id:0,
    //   nome:"Phillipe",
    //   cpf:"090.593.666-38",
    //   dataNascimento:"1989-12-25T00:00:00",
    //   email:null,
    //   telefone:null
    // };
    // http.post(baseUrl + 'api/profissional', profissional).subscribe(result => {
    // }, error => console.error(error));
  }

  private onClick() {
    alert('Clicked in menu item')
  }

}

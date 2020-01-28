import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProfissionalModel } from 'src/app/Models/profissional.module';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotification, PoNotificationService } from '@portinari/portinari-ui';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profissional-list',
  templateUrl: './profissional-list.component.html',
  styleUrls: ['./profissional-list.component.css']
})
export class ProfissionalListComponent implements OnInit {

  public profissionais: ProfissionalModel[];
  public columns: PoTableColumn[] = [
    { property: 'id' },
    { property: 'nome' },
    { property: 'cpf', label: 'CPF' },
    { property: 'dataNascimento', label: 'Data de nascimento', type: 'date' },
    { property: 'email', type:'link', action: this.sendMail.bind(this) },
    { property: 'tipoString', type: 'subtitle', subtitles: [
        { value: '0', color: 'color-01', content: 'M', label: 'Médico' },
        { value: '1', color: 'color-04', content: 'R', label: 'Recepcionista' }
      ] }
  ];
  public loading = true;
  public readonly actions: Array<PoPageAction> = [
    { action: this.onNewProfissional.bind(this), label: 'Cadastrar', icon: 'po-icon-user-add' }
  ];

  public readonly tableActions: Array<PoTableAction> = [
    { action: this.onEditProfissional.bind(this), label: 'Editar' },
    { action: this.onRemoveProfissional.bind(this), label: 'Remover', type: 'danger', separator: true }
  ];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router,
    private poNotification: PoNotificationService) { 
    this.httpClient.get<ProfissionalModel[]>(this.baseUrl + 'api/profissional').subscribe(result => {
      console.log(result);
      this.profissionais = result;
      this.profissionais.forEach(profissional => {
        profissional.tipoString = profissional.tipo.toString();
      });
      this.loading = false;
    }, error => console.error(error));

    var profissional = {
      numeroCarteiraTrabalho:null,
      crm:null,
      tipo:0,
      id:0,
      nome:"Phillipe",
      cpf:"090.593.666-38",
      dataNascimento:"1989-12-25T00:00:00",
      email:null,
      telefone:null
    };
   //httpClient.post(baseUrl + 'api/profissional', profissional).subscribe(result => {
    //}, error => console.error(error));
   }

  ngOnInit() {
  }

  private onEditProfissional(profissional) {
    this.router.navigateByUrl(`/profissional/edit/${profissional.id}`);
  }

  private sendMail(email, customer) {
    const body = `Olá ${customer.name}, `;
    const subject = 'Contato';

    window.open(`mailto:${email}?subject=${subject}&body=${body}`, '_self');
    
  }

  private onNewProfissional() {
    this.router.navigateByUrl('/profissional/new');
  }

  private onRemoveProfissional(profissional) {
    this.httpClient.delete(this.baseUrl + 'api/profissional/'+profissional.id).subscribe(result => {
      this.poNotification.warning('Profissional apagado com sucesso.');
      this.profissionais.splice(this.profissionais.indexOf(profissional), 1);
    }, error => console.error(error));
  }

}

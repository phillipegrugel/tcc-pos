import { Component, OnInit, Inject } from '@angular/core';
import { PacienteModel } from 'src/app/models/paciente.model';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotificationService, PoPageFilter, PoDialogService } from '@portinari/portinari-ui';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-paciente-list',
  templateUrl: './paciente-list.component.html',
  styleUrls: ['./paciente-list.component.css']
})
export class PacienteListComponent implements OnInit {

  public pacientes: PacienteModel[];
  public columns: PoTableColumn[] = [
    { property: 'id' },
    { property: 'nome' },
    { property: 'cpf', label: 'CPF' },
    { property: 'dataNascimento', label: 'Data de nascimento', type: 'date' },
    { property: 'email', type:'link', action: this.sendMail.bind(this) }
  ];
  public loading = true;
  public readonly actions: Array<PoPageAction> = [
    { action: this.onNewPaciente.bind(this), label: 'Cadastrar', icon: 'po-icon-user-add' }
  ];

  public readonly tableActions: Array<PoTableAction> = [
    { action: this.onEditPaciente.bind(this), label: 'Editar' },
    { action: this.onRemovePaciente.bind(this), label: 'Remover', type: 'danger', separator: true }
  ];


  public searchTerm: string = '';

  public readonly filter: PoPageFilter = {
    action: this.loadData.bind(this),
    ngModel: 'searchTerm',
    placeholder: 'Nome para pesquisar...'
  };

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private poNotification: PoNotificationService,
    private poDialogService: PoDialogService) { 
      this.loadData();
   }

   public loadData() {
    this.httpClient.get<PacienteModel[]>(this.baseUrl + 'api/paciente').subscribe(result => {
      this.pacientes = result;
      this.loading = false;
      if (this.searchTerm.length > 0) {
        this.pacientes = this.pacientes.filter(p => p.nome.includes(this.searchTerm));
      }
    }, error => console.error(error));
   }

  ngOnInit() {
  }

  private onEditPaciente(paciente) {
    this.router.navigateByUrl(`/paciente/edit/${paciente.id}`);
  }

  private sendMail(email, customer) {
    const body = `Olá ${customer.name}, `;
    const subject = 'Contato';

    window.open(`mailto:${email}?subject=${subject}&body=${body}`, '_self');
    
  }

  private onNewPaciente() {
    this.router.navigateByUrl('/paciente/new');
  }

  private onRemovePaciente(paciente) {

    this.poDialogService.confirm({
      title: 'Excluir paciente',
      message: `O paciente ${paciente.nome} será excluído, confirmar?`,
      confirm: () => this.excluirPaciente(paciente)
    });
  }

  private excluirPaciente(paciente) {
    this.httpClient.delete<any>(this.baseUrl + 'api/paciente/'+paciente.id).subscribe(result => {
      if (result.result.error) {
        this.poNotification.error(result.result.mensagem);
      } else {
        this.poNotification.success(result.result.mensagem);
        this.pacientes.splice(this.pacientes.indexOf(paciente), 1);
      }
    }, error => console.error(error));
  }

}

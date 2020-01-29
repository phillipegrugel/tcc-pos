import { Component, OnInit, Inject } from '@angular/core';
import { PacienteModel } from 'src/app/models/paciente.model';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotificationService } from '@portinari/portinari-ui';
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

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router,
    private poNotification: PoNotificationService) { 
    this.httpClient.get<PacienteModel[]>(this.baseUrl + 'api/paciente').subscribe(result => {
      console.log(result);
      this.pacientes = result;
      this.loading = false;
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
    this.httpClient.delete(this.baseUrl + 'api/paciente/'+paciente.id).subscribe(result => {
      this.poNotification.warning('Paciente apagado com sucesso.');
      this.pacientes.splice(this.pacientes.indexOf(paciente), 1);
    }, error => console.error(error));
  }

}

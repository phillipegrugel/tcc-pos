import { Component, OnInit, Inject } from '@angular/core';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotificationService } from '@portinari/portinari-ui';
import { ConsultaModel } from 'src/app/models/consulta.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-consulta-list',
  templateUrl: './consulta-list.component.html',
  styleUrls: ['./consulta-list.component.css']
})
export class ConsultaListComponent implements OnInit {

  public consultas: ConsultaModel[];
  public columns: PoTableColumn[] = [
    { property: 'id' },
    { property: 'data', label: 'Data', type: 'date' },
    { property: 'horariolist', label: 'Horário' },
    { property: 'nomepacientelist', label: 'Paciente' },
    { property: 'nomemedicolist', label: 'Medico' }
  ];
  public loading = true;
  public readonly actions: Array<PoPageAction> = [
    { action: this.onNewConsulta.bind(this), label: 'Cadastrar', icon: 'po-icon-user-add' }
  ];

  public readonly tableActions: Array<PoTableAction> = [
    { action: this.onRemoveConsulta.bind(this), label: 'Remover', type: 'danger', separator: true }
  ];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router,
    private poNotification: PoNotificationService) { 
    this.httpClient.get<ConsultaModel[]>(this.baseUrl + 'api/consulta').subscribe(result => {
      this.consultas = result;
      this.consultas.forEach(consulta => {
        consulta.nomepacientelist = consulta.paciente.nome;
        consulta.nomemedicolist = consulta.medico.nome;
        consulta.horariolist = consulta.horario.label;
      });
      this.loading = false;
    }, error => console.error(error));
   }


  ngOnInit() {
  }

  private onEditConsulta(consulta) {
    this.router.navigateByUrl(`/consulta/edit/${consulta.id}`);
  }

  private onNewConsulta() {
    this.router.navigateByUrl('/consulta/new');
  }

  private onRemoveConsulta(consulta) {
    this.httpClient.delete(this.baseUrl + 'api/consulta/'+consulta.id).subscribe(result => {
      this.poNotification.warning('Consulta apagado com sucesso.');
      this.consultas.splice(this.consultas.indexOf(consulta), 1);
    }, error => console.error(error));
  }

}

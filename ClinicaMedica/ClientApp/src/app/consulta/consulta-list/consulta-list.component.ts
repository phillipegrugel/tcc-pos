import { Component, OnInit, Inject } from '@angular/core';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotificationService, PoPageFilter, PoDialogService } from '@portinari/portinari-ui';
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
  public isMedico: boolean = false;
  public dataFiltro: Date = new Date();
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

  public tableActions: Array<PoTableAction> = [
    { action: this.onRemoveConsulta.bind(this), label: 'Cancelar', type: 'danger', separator: true }
  ];

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private poNotification: PoNotificationService,
    private poDialogService: PoDialogService) { 
    this.loadData();
    this.httpClient.get<boolean>(this.baseUrl + 'api/user/isMedico').subscribe(result => {
      this.isMedico = result;
      if(result) {
        this.tableActions.push({
          action: this.onExecuteConsulta.bind(this),
          label: 'Executar',
          type: 'danger',
          separator: true
        });
      }
    }, error => console.error(error));
   }


  ngOnInit() {
  }

  public readonly filter: PoPageFilter = {
    action: this.loadData.bind(this),
    ngModel: 'searchTerm',
    placeholder: 'Paciente para pesquisar...'
  };

  public searchTerm: string = '';

  public loadData() {
    this.httpClient.get<ConsultaModel[]>(this.baseUrl + 'api/consulta').subscribe(result => {
      this.consultas = result;
      this.consultas.forEach(consulta => {
        consulta.nomepacientelist = consulta.paciente.nome;
        consulta.nomemedicolist = consulta.medico.nome;
        consulta.horariolist = consulta.horario.label;
        if (this.searchTerm.length > 0) {
          this.consultas = this.consultas.filter(p => p.paciente.nome.includes(this.searchTerm));
        }
      });
      this.loading = false;
    }, error => console.error(error));
  }
  public onExecuteConsulta(consulta) {
    this.router.navigateByUrl(`/consulta/execute/${consulta.id}`);
  }

  private onEditConsulta(consulta) {
    this.router.navigateByUrl(`/consulta/edit/${consulta.id}`);
  }

  private onNewConsulta() {
    this.router.navigateByUrl('/consulta/new');
  }

  private onRemoveConsulta(consulta) {
    this.poDialogService.confirm({
      title: 'Cancelar consulta',
      message: `Confirmar cancelamento da consulta?`,
      confirm: () => this.excluirConsulta(consulta)
    });
    
  }

  private excluirConsulta(consulta) {
    this.httpClient.delete<any>(this.baseUrl + 'api/consulta/'+consulta.id).subscribe(result => {
      if (result.result.error) {
        this.poNotification.error(result.result.mensagem);
      } else {
        this.poNotification.success(result.result.mensagem);
        this.consultas.splice(this.consultas.indexOf(consulta), 1);
      }
    }, error => console.error(error));
  }

}

import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConsultaModel, RemedioReceitaModel, ExameModel, PedidoExameModel } from 'src/app/models/consulta.model';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { PoLookupColumn, PoModalAction, PoModalComponent, PoNotificationService } from '@portinari/portinari-ui';
import { RemedioLookupService } from 'src/app/shared/remedio-lookup.service';
import { RemedioModel } from 'src/app/models/remedio.model';
import { ExameLookupService } from 'src/app/shared/exame-lookup.service';

@Component({
  selector: 'app-consulta-execute',
  templateUrl: './consulta-execute.component.html',
  styleUrls: ['./consulta-execute.component.css']
})
export class ConsultaExecuteComponent implements OnInit {

  public consulta: ConsultaModel;
  public remedioLookupId: any;
  public remedioLookup: any;
  public observacoesRemedioLookup: string = "";
  public exameLookupId: any;

  confirm: PoModalAction = {
    action: () => {
      this.adicionaRemedioReceita();
    },
    label: 'Adicionar'
  };

  public readonly columnsLookupRemedio: Array<PoLookupColumn> = [
    { property: 'nome', label: 'Nome' },
    { property: 'nomeGenerico', label: 'Nome genérico' }
  ];

  @ViewChild('modalRemedio', { static: true }) poModal: PoModalComponent;
  @ViewChild('modalExame', { static: true }) poModalExame: PoModalComponent;

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private route: ActivatedRoute,
    public remedioService: RemedioLookupService,
    public exameService: ExameLookupService,
    private poNotification: PoNotificationService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.loadData(params['id']);
      }
    })
  }

  fieldFormat(value) {
    return value.nome;
  }

  loadData(id) {
    this.httpClient.get<ConsultaModel>(`${this.baseUrl}api/consulta/${id}`).subscribe(result => {
      this.consulta = result;
      this.consulta.paciente.dataNascimento = new Date(result.paciente.dataNascimento);
      this.consulta.data = new Date(result.data);
    });
  }

  adicionaRemedio() {
    this.httpClient.get<RemedioModel>(`${this.baseUrl}api/remedio/${this.remedioLookupId}`).subscribe(result => {
      this.remedioLookup = result;
    });
  }

  adicionaRemedioReceita() {
    let remedioReceita: RemedioReceitaModel ={
      id: 0,
      remedio: this.remedioLookup,
      observacoes: this.observacoesRemedioLookup,
      receita: null
    }
    this.consulta.historicoClinico.receita.remedios.push(remedioReceita);
    this.remedioLookup = {};
    this.remedioLookupId = null;
    this.observacoesRemedioLookup = '';
    this.poModal.close();
  }

  removerRemedio(remedioReceita) {
    this.consulta.historicoClinico.receita.remedios.splice(this.consulta.historicoClinico.receita.remedios.indexOf(remedioReceita), 1);
  }

  adicionaExames() {
    this.httpClient.get<ExameModel>(`${this.baseUrl}api/exame/${this.exameLookupId}`).subscribe(result => {
      let exameSolicitado: PedidoExameModel = {
       id: 0,
       entreguePaciente: false,
       resultado: '',
       exame: result,
       nomeexamelist: '',
       nomepacientelist: '',
       historicoClinico: null
      };
      this.exameLookupId = null;
      this.consulta.historicoClinico.exames.push(exameSolicitado);
    });
    this.poModalExame.close();
  }

  removerExame(exame) {
    this.consulta.historicoClinico.exames.splice(this.consulta.historicoClinico.exames.indexOf(exame), 1);
  }

  salvar() {
    this.httpClient.post<any>(`${this.baseUrl}api/consulta/salvarHistorico`, this.consulta).subscribe(result => {
      if (result.result.error) {
        this.poNotification.error(result.result.mensagem);
      } else {
        this.poNotification.success(result.result.mensagem);
        this.router.navigateByUrl('/consulta');
      }
    }, error => {

      for (var prop in error.error.errors) { 
        this.poNotification.error(error.error.errors[prop]); 
      }
    });
  }

  imprimirReceita() {
    this.router.navigateByUrl(`/consulta/imprimir-receita/${this.consulta.id}`);
  }

}

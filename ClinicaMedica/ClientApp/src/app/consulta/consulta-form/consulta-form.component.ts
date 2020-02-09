import { Component, OnInit, Inject } from '@angular/core';
import { ConsultaModel } from 'src/app/models/consulta.model';
import { TipoProfissional } from 'src/app/Models/tipo.profissional.enum';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { PacienteLookupService } from 'src/app/shared/paciente-lookup.service';
import { PoLookupColumn } from '@portinari/portinari-ui';
import { MedicoLookupService } from 'src/app/shared/medico-lookup.service';

@Component({
  selector: 'app-consulta-form',
  templateUrl: './consulta-form.component.html',
  styleUrls: ['./consulta-form.component.css']
})
export class ConsultaFormComponent implements OnInit {

  public isNewConsulta = true;
  public carregandoHorarios: boolean = true;
  public horariosHabilitados: boolean = true;

  public listMedicos = [];
  public medicoOptions = [];

  public horariosOptions = [];

  public readonly columns: Array<PoLookupColumn> = [
    { property: 'nome', label: 'Nome' },
    { property: 'cpf', label: 'CPF' }
  ];

  fieldFormat(value) {
    return value.nome;
  }

  public carregaHorarios() {
    if (this.consulta.medico.id > 0 && this.consulta.data) {
      this.carregandoHorarios = false;
      var param = {
        idMedico: this.consulta.medico.id,
        data: this.consulta.data
      }
      this.httpClient.post<Array<any>>(this.baseURL+"api/HorariosDisponiveis", param)
        .subscribe(result => {
          this.horariosOptions = result;
          this.carregandoHorarios = true;
          this.horariosHabilitados = false;
        });
    }
  }

  public consulta: ConsultaModel = {
    id: 0,
    data: null,
    nomemedicolist: '',
    nomepacientelist: '',
    horariolist: '', 
    medico: {
      nome: '',
      cpf: '',
      crm: '',
      dataNascimento: new Date(),
      email: '',
      id: 0,
      numeroCarteiraTrabalho: '',
      telefone: '',
      tipo: TipoProfissional.Medico,
      tipoString: TipoProfissional.Medico.toString(),
      usuario: {
        confirmarSenha: '',
        id: 0,
        login: '',
        senha: ''
      }
    },
    paciente: {
      nome: '',
      cpf: '',
      dataNascimento: new Date(),
      email: '',
      id: 0,
      telefone: '',
      nomeConvenio: '',
      numeroCarteirinha: '',
      possuiConvenio: false
    },
    horario: {
      label: '',
      value: 0
    }
  };
  private paramsSub: Subscription;
  private baseURL: string;

  constructor(public service: PacienteLookupService,
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private route: ActivatedRoute,
    public serviceMedico: MedicoLookupService) {
    this.baseURL = baseUrl;
   }

  ngOnInit() {
    debugger;
    this.paramsSub = this.route.params.subscribe(params => {
      debugger;
      if (params['id']) {
        this.isNewConsulta = false;
        this.loadData(params['id']);
      }
    })
  }

  loadData(id) {
    this.httpClient.get<any>(`${this.baseURL}api/consulta/${id}`).subscribe(result => {
      this.consulta = result;
      this.consulta.data = new Date(result.dataNascimento);
    });
  }

  save() {
    if (this.isNewConsulta) {
      debugger;
      this.horariosOptions.forEach(horario => {
        if (horario.value == this.consulta.horario)
          this.consulta.horario = horario;
      });
      this.httpClient.post(this.baseURL + 'api/consulta', this.consulta).subscribe(result => {
        this.router.navigateByUrl('/consulta');
      }, error => console.error(error));
    } else {
      this.httpClient.put(this.baseURL + 'api/consulta', this.consulta).subscribe(result => {
        this.router.navigateByUrl('/consulta');
      }, error => console.error(error));
    }
  }

  cancel() {
    this.router.navigateByUrl('/consulta');
  }

}

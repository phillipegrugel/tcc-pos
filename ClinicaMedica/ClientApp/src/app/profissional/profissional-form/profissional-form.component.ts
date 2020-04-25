import { Component, OnInit, Inject } from '@angular/core';
import { ProfissionalModule } from '../profissional.module';
import { HttpClient } from '@angular/common/http';
import { PoSelectOption, PoNotificationService } from '@portinari/portinari-ui';
import { ProfissionalModel } from 'src/app/Models/profissional.module';
import { TipoProfissional } from 'src/app/Models/tipo.profissional.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-profissional-form',
  templateUrl: './profissional-form.component.html',
  styleUrls: ['./profissional-form.component.css']
})
export class ProfissionalFormComponent implements OnInit {

  public isNewProfissional = true;

  public profissional: ProfissionalModel = {
    nome: '',
    cpf: '',
    crm: '',
    dataNascimento: null,
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
  };
  private paramsSub: Subscription;
  private baseURL: string;
  public tipoOptions = [
    { value: TipoProfissional.Medico, label: 'Médico' },
    { value: TipoProfissional.Recepcionista, label: 'Recepcionista' }
  ];

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private route: ActivatedRoute,
    private poNotification: PoNotificationService) {
    this.baseURL = baseUrl;
   }

  ngOnInit() {
    this.paramsSub = this.route.params.subscribe(params => {
      if (params['id']) {
        this.isNewProfissional = false;
        this.loadData(params['id']);
      }
    })
  }

  loadData(id) {
    this.httpClient.get<any>(`${this.baseURL}api/profissional/${id}`).subscribe(result => {
      this.profissional = result;
      this.profissional.dataNascimento = new Date(result.dataNascimento);
      this.profissional.usuario.confirmarSenha = this.profissional.usuario.senha;
    });
  }

  save() {
    if (this.isNewProfissional) {

      if (this.profissional.nome.length == 0) {
        this.poNotification.error("Campo nome obrigatório.");
      }

      if (this.profissional.dataNascimento == null) {
        this.poNotification.error("Campo data de nascimento obrigatório.")
      }

      if (this.profissional.nome.length != 0 && this.profissional.dataNascimento != null) {
        this.httpClient.post<any>(this.baseURL + 'api/profissional', this.profissional).subscribe(result => {
          if (result.result.error) {
            this.poNotification.error(result.result.mensagem);
          } else {
            this.poNotification.success(result.result.mensagem);
            this.router.navigateByUrl('/profissional');
          }
        }, error => {

          for (var prop in error.error.errors) { 
            this.poNotification.error(error.error.errors[prop]); 
          }
        });
      }
    } else {
      this.httpClient.put<any>(this.baseURL + 'api/profissional', this.profissional).subscribe(result => {
        if (result.result.error) {
          this.poNotification.error(result.result.mensagem);
        } else {
          this.poNotification.success(result.result.mensagem);
          this.router.navigateByUrl('/profissional');
        }
      }, error => this.poNotification.error(error));
    }
  }

  cancel() {
    this.router.navigateByUrl('/profissional');
  }

  isMedico() {
    return this.profissional.tipo == TipoProfissional.Medico;
  }
}

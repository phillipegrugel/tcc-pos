import { Component, OnInit, Inject } from '@angular/core';
import { ProfissionalModule } from '../profissional.module';
import { HttpClient } from '@angular/common/http';
import { PoSelectOption } from '@portinari/portinari-ui';
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

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router, private route: ActivatedRoute) {
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
      this.httpClient.post(this.baseURL + 'api/profissional', this.profissional).subscribe(result => {
        this.router.navigateByUrl('/profissional');
      }, error => console.error(error));
    } else {
      this.httpClient.put(this.baseURL + 'api/profissional', this.profissional).subscribe(result => {
        this.router.navigateByUrl('/profissional');
      }, error => console.error(error));
    }
  }

  cancel() {
    this.router.navigateByUrl('/profissional');
  }

}

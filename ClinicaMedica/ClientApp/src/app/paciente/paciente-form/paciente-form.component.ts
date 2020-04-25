import { Component, OnInit, Inject } from '@angular/core';
import { PacienteModel } from 'src/app/models/paciente.model';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { PoNotificationService } from '@portinari/portinari-ui';

@Component({
  selector: 'app-paciente-form',
  templateUrl: './paciente-form.component.html',
  styleUrls: ['./paciente-form.component.css']
})
export class PacienteFormComponent implements OnInit {

  public isNewPaciente = true;

  public paciente: PacienteModel = {
    nome: '',
    cpf: '',
    dataNascimento: null,
    email: '',
    id: 0,
    telefone: '',
    nomeConvenio: '',
    numeroCarteirinha: '',
    possuiConvenio: false
  };
  private paramsSub: Subscription;
  private baseURL: string;

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
        this.isNewPaciente = false;
        this.loadData(params['id']);
      }
    })
  }

  loadData(id) {
    this.httpClient.get<any>(`${this.baseURL}api/paciente/${id}`).subscribe(result => {
      this.paciente = result;
      this.paciente.dataNascimento = new Date(result.dataNascimento);
    });
  }

  save() {
    if (this.isNewPaciente) {

      if (this.paciente.dataNascimento == null) {
        this.poNotification.error("Campo data de nascimento é obrigatório.");
        return;
      }

      this.httpClient.post<any>(this.baseURL + 'api/paciente', this.paciente).subscribe(result => {
        debugger;
        if (result.result.error) {
          this.poNotification.error(result.result.mensagem);
        } else {
          this.poNotification.success(result.result.mensagem);
          this.router.navigateByUrl('/paciente');
        }
      }, error => {

        for (var prop in error.error.errors) { 
          this.poNotification.error(error.error.errors[prop]); 
        }
      });
    } else {
      this.httpClient.put<any>(this.baseURL + 'api/paciente', this.paciente).subscribe(result => {
        if (result.result.error) {
          this.poNotification.error(result.result.mensagem);
        } else {
          this.poNotification.success(result.result.mensagem);
          this.router.navigateByUrl('/paciente');
        }
      }, error => {

        for (var prop in error.error.errors) { 
          this.poNotification.error(error.error.errors[prop]); 
        }
      });
    }
  }

  cancel() {
    this.router.navigateByUrl('/paciente');
  }
}

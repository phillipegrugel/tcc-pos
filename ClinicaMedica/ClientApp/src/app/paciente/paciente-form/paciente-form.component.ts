import { Component, OnInit, Inject } from '@angular/core';
import { PacienteModel } from 'src/app/models/paciente.model';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

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

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router, private route: ActivatedRoute) {
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
      this.httpClient.post(this.baseURL + 'api/paciente', this.paciente).subscribe(result => {
        this.router.navigateByUrl('/paciente');
      }, error => console.error(error));
    } else {
      this.httpClient.put(this.baseURL + 'api/paciente', this.paciente).subscribe(result => {
        this.router.navigateByUrl('/paciente');
      }, error => console.error(error));
    }
  }

  cancel() {
    this.router.navigateByUrl('/paciente');
  }
}

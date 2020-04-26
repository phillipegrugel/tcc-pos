import { Component, OnInit, Inject } from '@angular/core';
import { PoLookupColumn, PoNotificationService } from '@portinari/portinari-ui';
import { PacienteLookupService } from '../shared/paciente-lookup.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-consulta-rapida',
  templateUrl: './consulta-rapida.component.html',
  styleUrls: ['./consulta-rapida.component.css']
})
export class ConsultaRapidaComponent implements OnInit {

  public baseURL: string;

  public idPaciente: number;
  constructor(public service: PacienteLookupService, private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private poNotification: PoNotificationService) {
      this.baseURL = baseUrl;
     }

  public readonly columns: Array<PoLookupColumn> = [
    { property: 'nome', label: 'Nome' },
    { property: 'cpf', label: 'CPF' }
  ];

  fieldFormat(value) {
    return value.nome;
  }

  ngOnInit() {
  }

  public cancel() {

  }

  public save() {
    this.httpClient.post<any>(`${this.baseURL}api/consulta/GeraConsultaRapida`, this.idPaciente).subscribe(result => {
      this.poNotification.success(result.message);
    }, error => {

      for (var prop in error.error.errors) { 
        this.poNotification.error(error.error.errors[prop]); 
      }
    });
  }

}

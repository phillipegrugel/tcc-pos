import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { RemedioLookupService } from 'src/app/shared/remedio-lookup.service';
import { ExameLookupService } from 'src/app/shared/exame-lookup.service';
import { PoNotificationService } from '@portinari/portinari-ui';
import { ConsultaModel } from 'src/app/models/consulta.model';

@Component({
  selector: 'app-emitir-receita',
  templateUrl: './emitir-receita.component.html',
  styleUrls: ['./emitir-receita.component.css']
})
export class EmitirReceitaComponent implements OnInit {

  public consulta: ConsultaModel;

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
    });
  }

  loadData(id) {
    this.httpClient.get<ConsultaModel>(`${this.baseUrl}api/consulta/${id}`).subscribe(result => {
      this.consulta = result;
      this.consulta.paciente.dataNascimento = new Date(result.paciente.dataNascimento);
      this.consulta.data = new Date(result.data);
    });
  }

  imprimir() {
    window.print();
  }

}

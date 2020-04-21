import { Component, OnInit, Inject } from '@angular/core';
import { PedidoExameModel } from 'src/app/models/consulta.model';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-exame-resultado',
  templateUrl: './exame-resultado.component.html',
  styleUrls: ['./exame-resultado.component.css']
})
export class ExameResultadoComponent implements OnInit {

  public exame: PedidoExameModel = {
    entreguePaciente: false,
    exame: null,
    historicoClinico: null,
    id: 0,
    nomeexamelist: '',
    nomepacientelist: '',
    resultado: ''
  }

  public baseUrl;
  public paramsSub: any;

  public titulo = "Inserir resultado para o exame";

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private route: ActivatedRoute,) {
      this.baseUrl = baseUrl;
     }

  ngOnInit() {
    this.paramsSub = this.route.params.subscribe(params => {
      if (params['id']) {
        this.loadData(params['id']);
      }
    })
  }
  loadData(id) {
    this.httpClient.get<any>(`${this.baseUrl}api/exame/GetExamePendente/${id}`).subscribe(result => {
      this.exame = result;
    });
  }

  public save() {
    this.httpClient.post(`${this.baseUrl}api/exame/SalvaResultadoExame`, this.exame).subscribe(result => {
    });
  }

  public cancel() {

  }
}

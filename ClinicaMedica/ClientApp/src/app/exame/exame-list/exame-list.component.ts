import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { PedidoExameModel } from 'src/app/models/consulta.model';
import { PoTableColumn, PoTableAction } from '@portinari/portinari-ui';

@Component({
  selector: 'app-exame-list',
  templateUrl: './exame-list.component.html',
  styleUrls: ['./exame-list.component.css']
})
export class ExameListComponent implements OnInit {

  public pedidosExame: PedidoExameModel[];

  public columns: PoTableColumn[] = [
    { property: 'id' },
    { property: 'nomepacientelist', label: 'Paciente' },
    { property: 'nomeexamelist', label: 'Exame' }
  ];

  public tableActions: Array<PoTableAction> = [
    { action: this.onInserirResultado.bind(this), label: 'Inserir resultado', type: 'danger', separator: true }
  ];

  public onInserirResultado(exame) {
    this.router.navigateByUrl(`/exame/resultado/${exame.id}`);
  }

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
    this.httpClient.get<PedidoExameModel[]>(this.baseUrl + 'api/exame/GetExamesPendentes').subscribe(result => {
      this.pedidosExame = result;
      this.pedidosExame.forEach(pedido => {
        pedido.nomepacientelist = pedido.historicoClinico.consulta.paciente.nome;
        pedido.nomeexamelist = pedido.exame.nome;

      });
    }, error => console.error(error));
   }

  ngOnInit() {
  }

}

import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { PedidoExameModel } from 'src/app/models/consulta.model';
import { PoTableColumn, PoTableAction, PoPageFilter, PoNotificationService } from '@portinari/portinari-ui';
import { AuthService } from 'src/app/shared/auth.service';
import { Location } from '@angular/common';

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

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private authService: AuthService,
    private poNotification: PoNotificationService,
    private _location: Location) {

      if (this.authService.isMedico())
      {
        this.poNotification.error("Você não possui permissão para acessar este menu.");
        this._location.back();
      } else {
        this.loadData();
      }
   }

  public searchTerm: string = '';

  public readonly filter: PoPageFilter = {
    action: this.loadData.bind(this),
    ngModel: 'searchTerm',
    placeholder: 'Paciente para pesquisar...'
  };

  loadData() {
    this.httpClient.get<PedidoExameModel[]>(this.baseUrl + 'api/exame/GetExamesPendentes').subscribe(result => {
      this.pedidosExame = result;
      this.pedidosExame.forEach(pedido => {
        pedido.nomepacientelist = pedido.historicoClinico.consulta.paciente.nome;
        pedido.nomeexamelist = pedido.exame.nome;
      });
      if (this.searchTerm.length > 0) {
        this.pedidosExame = this.pedidosExame.filter(p => p.nomepacientelist.includes(this.searchTerm));
      }
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

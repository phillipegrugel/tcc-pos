import { Component, OnInit, Inject } from '@angular/core';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotificationService } from '@portinari/portinari-ui';
import { RemedioModel } from 'src/app/models/remedio.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-remedio-list',
  templateUrl: './remedio-list.component.html',
  styleUrls: ['./remedio-list.component.css']
})
export class RemedioListComponent implements OnInit {

  public remedios: RemedioModel[];
  public columns: PoTableColumn[] = [
    { property: 'id' },
    { property: 'nome' },
    { property: 'nomeGenerico', label: 'Nome genérico' },
    { property: 'fabricante' }
  ];
  public loading = true;
  public readonly actions: Array<PoPageAction> = [
    { action: this.onNewRemedio.bind(this), label: 'Cadastrar', icon: 'po-icon-user-add' }
  ];

  public readonly tableActions: Array<PoTableAction> = [
    { action: this.onEditRemedio.bind(this), label: 'Editar' },
    { action: this.onRemoveRemedio.bind(this), label: 'Remover', type: 'danger', separator: true }
  ];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router,
    private poNotification: PoNotificationService) { 
    this.httpClient.get<RemedioModel[]>(this.baseUrl + 'api/remedio').subscribe(result => {
      console.log(result);
      this.remedios = result;
      this.loading = false;
    }, error => console.error(error));
   }


  ngOnInit() {
  }

  private onEditRemedio(remedio) {
    this.router.navigateByUrl(`/remedio/edit/${remedio.id}`);
  }

  private onNewRemedio() {
    this.router.navigateByUrl('/remedio/new');
  }

  private onRemoveRemedio(remedio) {
    this.httpClient.delete(this.baseUrl + 'api/remedio/'+remedio.id).subscribe(result => {
      this.poNotification.warning('Paciente apagado com sucesso.');
      this.remedios.splice(this.remedios.indexOf(remedio), 1);
    }, error => console.error(error));
  }

}

import { Component, OnInit, Inject } from '@angular/core';
import { PoTableColumn, PoPageAction, PoTableAction, PoNotificationService, PoPageFilter, PoDialogService } from '@portinari/portinari-ui';
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

  public searchTerm: string;

  public readonly filter: PoPageFilter = {
    action: this.loadData.bind(this),
    ngModel: 'searchTerm',
    placeholder: 'Nome para pesquisar...'
  };

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private poNotification: PoNotificationService,
    private poDialogService: PoDialogService) { 
    this.loadData();
   }

  public loadData() {
    this.httpClient.get<RemedioModel[]>(this.baseUrl + 'api/remedio').subscribe(result => {
      console.log(result);
      this.remedios = result;
      this.loading = false;
      if (this.searchTerm.length > 0) {
        this.remedios = this.remedios.filter(p => p.nome.includes(this.searchTerm));
      }
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

    this.poDialogService.confirm({
      title: 'Excluir remédio',
      message: `O remédio ${remedio.nome} será excluído, confirmar?`,
      confirm: () => this.excluirRemedio(remedio)
    });
  }

  private excluirRemedio(remedio) {
    this.httpClient.delete<any>(this.baseUrl + 'api/remedio/'+remedio.id).subscribe(result => {
      if (result.error) {
        this.poNotification.error(result.mensagem);
      } else {
        this.poNotification.success(result.mensagem);
        this.remedios.splice(this.remedios.indexOf(remedio), 1);
      }
    }, error => console.error(error));
  }

}

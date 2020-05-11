import { Component, OnInit, Inject } from '@angular/core';
import { RemedioModel } from 'src/app/models/remedio.model';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { PoNotificationService } from '@portinari/portinari-ui';
import { Location } from '@angular/common';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-remedio-form',
  templateUrl: './remedio-form.component.html',
  styleUrls: ['./remedio-form.component.css']
})
export class RemedioFormComponent implements OnInit {
  
  public isNewRemedio = true;

  public remedio: RemedioModel = {
    nome: '',
    fabricante: '',
    id: 0,
    nomeGenerico: ''
  };
  
  private paramsSub: Subscription;
  private baseURL: string;

  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private route: ActivatedRoute,
    private poNotification: PoNotificationService,
    private authService: AuthService,
    private _location: Location) {
    this.baseURL = baseUrl;
    if(!this.authService.isMedico()){
      this._location.back();
      this.poNotification.error("Você não possui permissão para acessar este menu.");
    }
   }

  ngOnInit() {
    this.paramsSub = this.route.params.subscribe(params => {
      if (params['id']) {
        this.isNewRemedio = false;
        this.loadData(params['id']);
      }
    })
  }

  loadData(id) {
    this.httpClient.get<any>(`${this.baseURL}api/remedio/${id}`).subscribe(result => {
      this.remedio = result;
    });
  }

  save() {
    if (this.isNewRemedio) {
      this.httpClient.post<any>(this.baseURL + 'api/remedio', this.remedio).subscribe(result => {
        if (result.error) {
          this.poNotification.error(result.mensagem);
        } else {
          this.poNotification.success(result.mensagem);
          this.router.navigateByUrl('/remedio');
        }
      }, error => {
        for (var prop in error.error.errors) { 
          this.poNotification.error(error.error.errors[prop]); 
        }
      });
    } else {
      this.httpClient.put<any>(this.baseURL + 'api/remedio', this.remedio).subscribe(result => {
        if (result.error) {
          this.poNotification.error(result.mensagem);
        } else {
          this.poNotification.success(result.mensagem);
          this.router.navigateByUrl('/remedio');
        }
      }, error => {

        for (var prop in error.error.errors) { 
          this.poNotification.error(error.error.errors[prop]); 
        }
      });
    }
  }

  cancel() {
    this.router.navigateByUrl('/remedio');
  }

}

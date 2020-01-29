import { Component, OnInit, Inject } from '@angular/core';
import { RemedioModel } from 'src/app/models/remedio.model';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

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

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router, private route: ActivatedRoute) {
    this.baseURL = baseUrl;
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
      this.httpClient.post(this.baseURL + 'api/remedio', this.remedio).subscribe(result => {
        this.router.navigateByUrl('/remedio');
      }, error => console.error(error));
    } else {
      this.httpClient.put(this.baseURL + 'api/remedio', this.remedio).subscribe(result => {
        this.router.navigateByUrl('/remedio');
      }, error => console.error(error));
    }
  }

  cancel() {
    this.router.navigateByUrl('/remedio');
  }

}

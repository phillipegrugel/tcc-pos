import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoaderStateModel } from './loader.module';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private loaderSubject = new Subject<LoaderStateModel>();
  loaderState = this.loaderSubject.asObservable();

  show() {
    this.loaderSubject.next({ show: true } as LoaderStateModel);
  }

  hide() {
    this.loaderSubject.next({ show: false } as LoaderStateModel);
  }

}

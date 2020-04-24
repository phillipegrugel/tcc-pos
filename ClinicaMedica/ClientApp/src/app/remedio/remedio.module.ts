import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RemedioRoutingModule } from './remedio-routing.module';
import { RemedioListComponent } from './remedio-list/remedio-list.component';
import { RemedioFormComponent } from './remedio-form/remedio-form.component';

import { PoModule } from '@portinari/portinari-ui';
import { PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../shared/auth.interceptor';
import { LoaderInterceptor } from '../shared/loader.interceptor';
import { LoaderModule } from '../shared/Components/loader/loader.module';


@NgModule({
  declarations: [RemedioListComponent, RemedioFormComponent],
  imports: [
    CommonModule,
    RemedioRoutingModule,
    PoModule,
    FormsModule,
    PoFieldModule,
    LoaderModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    }
  ]
})
export class RemedioModule { }

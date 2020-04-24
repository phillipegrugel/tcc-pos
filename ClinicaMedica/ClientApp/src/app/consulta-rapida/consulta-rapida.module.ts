import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConsultaRapidaRoutingModule } from './consulta-rapida-routing.module';
import { ConsultaRapidaComponent } from './consulta-rapida.component';
import { PoModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../shared/auth.interceptor';
import { PacienteLookupService } from '../shared/paciente-lookup.service';


@NgModule({
  declarations: [ConsultaRapidaComponent],
  imports: [
    CommonModule,
    ConsultaRapidaRoutingModule,
    PoModule,
    FormsModule,
    ConsultaRapidaRoutingModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    PacienteLookupService
  ]
})
export class ConsultaRapidaModule { }

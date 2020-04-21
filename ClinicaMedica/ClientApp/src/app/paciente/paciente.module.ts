import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PacienteRoutingModule } from './paciente-routing.module';
import { PacienteListComponent } from './paciente-list/paciente-list.component';

import { PoModule } from '@portinari/portinari-ui';
import { PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';
import { PacienteFormComponent } from './paciente-form/paciente-form.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../shared/auth.interceptor';


@NgModule({
  declarations: [PacienteListComponent, PacienteFormComponent],
  imports: [
    CommonModule,
    PacienteRoutingModule,
    PoModule,
    FormsModule,
    PoFieldModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ]
})
export class PacienteModule { }

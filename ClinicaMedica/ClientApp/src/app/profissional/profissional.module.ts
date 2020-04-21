import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfissionalRoutingModule } from './profissional-routing.module';
import { ProfissionalListComponent } from './profissional-list/profissional-list.component';
import { PoModule } from '@portinari/portinari-ui';
import { ProfissionalFormComponent } from './profissional-form/profissional-form.component';
import { PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../shared/auth.interceptor';


@NgModule({
  declarations: [ProfissionalListComponent, ProfissionalFormComponent],
  imports: [
    CommonModule,
    ProfissionalRoutingModule,
    PoModule,
    FormsModule,
    PoFieldModule
  ],
  exports: [
    ProfissionalListComponent
  ],
  bootstrap: [
    ProfissionalListComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ]
})
export class ProfissionalModule { }

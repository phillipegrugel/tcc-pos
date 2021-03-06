import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConsultaRoutingModule } from './consulta-routing.module';
import { ConsultaListComponent } from './consulta-list/consulta-list.component';
import { PoModule, PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';
import { ConsultaFormComponent } from './consulta-form/consulta-form.component';
import { PacienteLookupService } from '../shared/paciente-lookup.service';
import { MedicoLookupService } from '../shared/medico-lookup.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../shared/auth.interceptor';
import { ConsultaExecuteComponent } from './consulta-execute/consulta-execute.component';
import { RemedioLookupService } from '../shared/remedio-lookup.service';
import { ExameLookupService } from '../shared/exame-lookup.service';
import { LoaderInterceptor } from '../shared/loader.interceptor';
import { LoaderModule } from '../shared/Components/loader/loader.module';
import { EmitirReceitaComponent } from './emitir-receita/emitir-receita.component';



@NgModule({
  declarations: [ConsultaListComponent, ConsultaFormComponent, ConsultaExecuteComponent, EmitirReceitaComponent],
  imports: [
    CommonModule,
    ConsultaRoutingModule,
    PoModule,
    FormsModule,
    PoFieldModule,
    LoaderModule
  ],
  providers: [
    PacienteLookupService,
    RemedioLookupService,
    ExameLookupService,
    MedicoLookupService,
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
export class ConsultaModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConsultaRoutingModule } from './consulta-routing.module';
import { ConsultaListComponent } from './consulta-list/consulta-list.component';
import { PoModule, PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';
import { ConsultaFormComponent } from './consulta-form/consulta-form.component';
import { PacienteLookupService } from '../shared/paciente-lookup.service';
import { MedicoLookupService } from '../shared/medico-lookup.service';



@NgModule({
  declarations: [ConsultaListComponent, ConsultaFormComponent],
  imports: [
    CommonModule,
    ConsultaRoutingModule,
    PoModule,
    FormsModule,
    PoFieldModule
  ],
  providers: [
    PacienteLookupService,
    MedicoLookupService
  ]
})
export class ConsultaModule { }

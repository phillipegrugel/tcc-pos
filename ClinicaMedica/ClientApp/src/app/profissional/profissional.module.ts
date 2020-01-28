import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfissionalRoutingModule } from './profissional-routing.module';
import { ProfissionalListComponent } from './profissional-list/profissional-list.component';
import { PoModule } from '@portinari/portinari-ui';
import { ProfissionalFormComponent } from './profissional-form/profissional-form.component';
import { PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';


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
  ]
})
export class ProfissionalModule { }

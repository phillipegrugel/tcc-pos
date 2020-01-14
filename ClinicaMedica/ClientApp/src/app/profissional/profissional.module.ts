import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppModule } from './../app.module'

import { ProfissionalRoutingModule } from './profissional-routing.module';
import { ProfissionalListComponent } from './profissional-list/profissional-list.component';


@NgModule({
  declarations: [ProfissionalListComponent],
  imports: [
    CommonModule,
    ProfissionalRoutingModule
  ]
})
export class ProfissionalModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RemedioRoutingModule } from './remedio-routing.module';
import { RemedioListComponent } from './remedio-list/remedio-list.component';
import { RemedioFormComponent } from './remedio-form/remedio-form.component';

import { PoModule } from '@portinari/portinari-ui';
import { PoFieldModule } from '@portinari/portinari-ui';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [RemedioListComponent, RemedioFormComponent],
  imports: [
    CommonModule,
    RemedioRoutingModule,
    PoModule,
    FormsModule,
    PoFieldModule
  ]
})
export class RemedioModule { }

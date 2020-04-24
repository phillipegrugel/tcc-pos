import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConsultaRapidaComponent } from './consulta-rapida.component';


const routes: Routes = [
  { path: '', component: ConsultaRapidaComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConsultaRapidaRoutingModule { }

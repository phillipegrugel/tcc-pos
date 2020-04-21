import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExameListComponent } from './exame-list/exame-list.component';
import { ExameResultadoComponent } from './exame-resultado/exame-resultado.component';


const routes: Routes = [
    { path: '', component: ExameListComponent },
    { path: 'resultado/:id', component: ExameResultadoComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExameRoutingModule { }

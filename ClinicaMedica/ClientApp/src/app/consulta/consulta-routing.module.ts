import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConsultaListComponent } from './consulta-list/consulta-list.component';
import { ConsultaFormComponent } from './consulta-form/consulta-form.component';
import { ConsultaExecuteComponent } from './consulta-execute/consulta-execute.component';
import { EmitirReceitaComponent } from './emitir-receita/emitir-receita.component';


const routes: Routes = [
    { path: '', component: ConsultaListComponent },
    { path: 'new', component: ConsultaFormComponent },
    { path: 'edit/:id', component: ConsultaFormComponent },
    { path: 'execute/:id', component: ConsultaExecuteComponent },
    { path: 'imprimir-receita/:id', component: EmitirReceitaComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConsultaRoutingModule { }

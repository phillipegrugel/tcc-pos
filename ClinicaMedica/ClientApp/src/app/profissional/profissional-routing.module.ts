import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfissionalListComponent } from './profissional-list/profissional-list.component';
import { ProfissionalFormComponent } from './profissional-form/profissional-form.component';


const routes: Routes = [
  { path: '', component: ProfissionalListComponent },
  { path: 'new', component: ProfissionalFormComponent },
  { path: 'edit/:id', component: ProfissionalFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfissionalRoutingModule { }

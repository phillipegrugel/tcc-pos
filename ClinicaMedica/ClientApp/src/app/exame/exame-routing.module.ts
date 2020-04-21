import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExameListComponent } from './exame-list/exame-list.component';


const routes: Routes = [
    { path: '', component: ExameListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExameRoutingModule { }

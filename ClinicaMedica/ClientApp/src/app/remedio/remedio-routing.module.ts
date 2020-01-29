import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RemedioListComponent } from './remedio-list/remedio-list.component';
import { RemedioFormComponent } from './remedio-form/remedio-form.component';


const routes: Routes = [
  { path: '', component: RemedioListComponent },
  { path: 'new', component: RemedioFormComponent },
  { path: 'edit/:id', component: RemedioFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RemedioRoutingModule { }

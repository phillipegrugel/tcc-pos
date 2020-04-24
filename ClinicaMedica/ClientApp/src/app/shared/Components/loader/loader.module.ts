import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './loader.component';
import { PoLoadingModule } from '@portinari/portinari-ui';



@NgModule({
  declarations: [LoaderComponent],
  imports: [
    PoLoadingModule,
    CommonModule
  ],
  exports: [LoaderComponent]
})
export class LoaderModule { }

export interface LoaderStateModel {
  show: boolean;
}
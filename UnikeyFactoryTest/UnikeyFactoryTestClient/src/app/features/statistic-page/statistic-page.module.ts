import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatisticPageComponent } from './statistic-page.component';
import {FormsModule} from '@angular/forms';




@NgModule({
  declarations: [StatisticPageComponent],
  exports: [
    StatisticPageComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class StatisticPageModule { }

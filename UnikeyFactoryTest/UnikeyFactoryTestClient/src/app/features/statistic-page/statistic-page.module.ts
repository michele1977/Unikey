import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatisticPageComponent } from './statistic-page.component';
import {FormsModule} from '@angular/forms';
import {NgCircleProgressModule} from 'ng-circle-progress';




@NgModule({
  declarations: [StatisticPageComponent],
  exports: [
    StatisticPageComponent
  ],
    imports: [
        CommonModule,
        FormsModule,
        NgCircleProgressModule
    ]
})
export class StatisticPageModule { }

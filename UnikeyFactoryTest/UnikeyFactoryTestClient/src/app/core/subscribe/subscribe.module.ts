import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubscribeComponent } from './subscribe.component';
import {HttpClientModule} from '@angular/common/http';



@NgModule({
  declarations: [SubscribeComponent],
  exports: [
    SubscribeComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ]
})
export class SubscribeModule { }

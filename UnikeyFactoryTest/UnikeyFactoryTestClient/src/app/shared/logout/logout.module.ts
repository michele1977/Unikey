import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LogoutComponent} from './logout.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [LogoutComponent],
  exports: [
    LogoutComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule
  ]
})
export class LogoutModule { }

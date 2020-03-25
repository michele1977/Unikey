import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideBarComponent } from './side-bar.component';
import {RouterModule} from '@angular/router';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [SideBarComponent],
  exports: [
    SideBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule
  ]
})
export class SideBarModule { }

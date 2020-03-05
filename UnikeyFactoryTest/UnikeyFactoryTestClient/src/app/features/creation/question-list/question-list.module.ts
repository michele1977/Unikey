import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionListComponent } from './question-list.component';
import {DragDropModule} from '@angular/cdk/drag-drop';



@NgModule({
  declarations: [QuestionListComponent],
  exports: [
    QuestionListComponent
  ],
  imports: [
    CommonModule,
    DragDropModule
  ]
})
export class QuestionListModule { }

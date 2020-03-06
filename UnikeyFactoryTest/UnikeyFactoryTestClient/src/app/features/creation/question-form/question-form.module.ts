import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionFormComponent } from './question-form.component';
import {FormsModule} from '@angular/forms';



@NgModule({
    declarations: [QuestionFormComponent],
    exports: [
        QuestionFormComponent
    ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class QuestionFormModule { }

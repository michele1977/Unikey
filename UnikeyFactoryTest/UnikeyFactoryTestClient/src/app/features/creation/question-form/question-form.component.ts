import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Test} from '../../../models/test';
import {Question} from '../../../models/question';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent {

  @Output() questionInsert: EventEmitter<Test> = new EventEmitter<Test>();

  question: Question;

  constructor() { }

}

import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import {Test} from '../../../models/test';
import {AnswerState} from '../../../shared/enums/answer-state';
import {Question} from '../../../models/question';

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent {
  @Input() test: Test;
  @Output() showForm: EventEmitter<boolean> = new EventEmitter();

  setVisibility: boolean;

  constructor() { }

  onDrop(event: CdkDragDrop<Question[]>) {
    moveItemInArray(this.test.Questions, event.previousIndex, event.currentIndex);
    for (let i = 0; i < this.test.Questions.length; i++) {
      this.test.Questions[i].Position = i;
    }
  }

  showQuestionForm() {
    this.setVisibility = !this.setVisibility;
    this.showForm.emit(this.setVisibility);
  }
}

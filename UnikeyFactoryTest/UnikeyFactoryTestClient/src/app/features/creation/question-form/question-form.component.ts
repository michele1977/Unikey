import {Component, EventEmitter, Output} from '@angular/core';
import {Question} from '../../../models/question';
import {Answer} from '../../../models/answer';
import {AnswerState} from '../../../shared/enums/answer-state';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent {

  @Output() questionInsert: EventEmitter<Question> = new EventEmitter<Question>();

  question: Question = {
    Answers: [new Answer()]
  };

  constructor() {
  }

  addAnswer(form: NgForm) {
    this.question.Answers[this.question.Answers.length - 1].Text = form.value.answerText;
    this.question.Answers[this.question.Answers.length - 1].IsCorrect = form.value.isCorrect ? AnswerState.Correct : AnswerState.NotCorrect;
    this.question.Answers[this.question.Answers.length - 1].Score = form.value.answerScore;
    console.log(this.question);
    this.question.Answers.push(new Answer());
  }

  addQuestion(form: NgForm) {
    // Event
  }
}

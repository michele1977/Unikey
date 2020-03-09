import {Component, EventEmitter, Output} from '@angular/core';
import {Question} from '../../../models/question';
import {FormArray, FormControl, FormGroup} from '@angular/forms';
import {Answer} from '../../../models/answer';
import {AnswerState} from '../../../shared/enums/answer-state';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent {

  @Output() questionInsert: EventEmitter<Question> = new EventEmitter<Question>();

  question: Question = {
    Id: 0,
    Position: 0,
    Text: '',
    TestId: 0,
    Answers: []
  };
  text: string;
  formArray = new FormArray([]);

  constructor() { }

  addAnswer(formArray: FormArray) {
    const answer = new Answer();
    if (formArray.controls[formArray.controls.length - 1] !== undefined) {
      answer.Text = formArray.controls[formArray.controls.length - 1].value.answerText;
      answer.IsCorrect = formArray.controls[formArray.controls.length - 1].value.isCorrect ? AnswerState.Correct : AnswerState.NotCorrect;
      answer.Score = formArray.controls[formArray.controls.length - 1].value.answerScore;
      this.question.Answers.push(answer);
    }

    console.log(this.question.Answers);

    const myGroup = new FormGroup({
      isCorrect: new FormControl(),
      answerText: new FormControl(),
      answerScore: new FormControl()
    });

    this.formArray.push(myGroup);
  }

  addQuestion(form) {
    this.question.Text = form.value.text;
    this.questionInsert.emit(this.question);
  }
}

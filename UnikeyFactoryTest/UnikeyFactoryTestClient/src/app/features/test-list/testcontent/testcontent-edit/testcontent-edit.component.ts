import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { Answer } from 'src/app/models/answer';
import { Question } from 'src/app/models/question';
import { IconsService } from 'src/app/services/icons.service';

@Component({
  selector: 'app-testcontent-edit',
  templateUrl: './testcontent-edit.component.html',
  styleUrls: ['./testcontent-edit.component.css']
})
export class TestContentEditComponent implements OnInit {
  @Input() question: Question;
  @Input() questionIndex: number;
  @Output() editEvent = new EventEmitter();
  @Output() undoEvent = new EventEmitter();

  questionForm: FormGroup;
  emptyAnswer: Answer = {
    Id: 0,
    Text: '',
    Score: 0,
    IsCorrect: 0,
    QuestionId: 0
  };

  constructor(private fb: FormBuilder, public icons: IconsService) { }


  ngOnInit(): void {

    this.questionForm = this.fb.group({
      questionText: '',
      answers: this.fb.array([])
    });

    this.questionForm.controls.questionText.setValue(this.question.Text);
    for (const answer of this.question.Answers) {
      this.answers.push(this.fb.group(answer));
    }
  }

  get answers(): FormArray {
    return this.questionForm.get('answers') as FormArray;
  }

  addEmptyAnswer() {
    this.answers.push(this.fb.group(this.emptyAnswer));
  }

  edit(question: Question, index: number) {
    this.editEvent.next({question, index});
  }

  undo() {
    this.undoEvent.next();
  }
}


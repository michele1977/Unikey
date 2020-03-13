import {ChangeDetectorRef, Component, DoCheck, EventEmitter, OnInit, Output} from '@angular/core';
import {Question} from '../../../models/question';
import {FormArray, FormBuilder, FormControl, FormGroup, NgForm, Validators} from '@angular/forms';
import {AnswerState} from '../../../shared/enums/answer-state';

@Component({
  selector: 'app-question-form',
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent implements OnInit {

  @Output() questionInsert: EventEmitter<Question> = new EventEmitter<Question>();

  question: Question = {
    Id: 0,
    Position: 0,
    Text: '',
    TestId: 0,
    Answers: []
  };
  text: string;
  orderForm: FormGroup;
  formArray = new FormArray([]);
  checked = true;

  constructor(private fb: FormBuilder, private cd: ChangeDetectorRef) { }

  ngOnInit() {
    this.orderForm = this.fb.group({
      questionText: new FormControl('', [Validators.required]),
      items: this.fb.array([])
    });
  }

  addAnswer() {
    this.addItem();
  }

  addQuestion() {
    this.question.Text = this.orderForm.controls.questionText.value;
    this.formArray = this.orderForm.controls.items as FormArray;
    this.orderForm.controls.items.value.forEach((value, index) => {
      if (!this.formArray.value[index].isCorrect) {
        this.formArray.value[index].answerScore = 0;
      }
      if (this.formArray.value[index].answerScore === null) {
        this.formArray.value[index].answerScore = 1;
      }
      console.log(this.formArray.value[index].answerScore);
      this.question.Answers.push({
        Text: this.formArray.value[index].answerText,
        Score: this.formArray.value[index].answerScore,
        IsCorrect: this.formArray.value[index].isCorrect ? AnswerState.Correct : AnswerState.NotCorrect
      });
    });
    this.questionInsert.emit(this.question);
    this.orderForm.reset();
  }

  createItem(): FormGroup {
    return this.fb.group({
      isCorrect: new FormControl(),
      answerText: new FormControl('', [Validators.required]),
      answerScore: new FormControl()
    });
  }

  addItem(): void {
    this.formArray = this.orderForm.controls.items as FormArray;
    this.formArray.push(this.createItem());
  }

  onChange(form) {
    this.formArray = this.orderForm.controls.items as FormArray;
    this.checked = true;
    this.orderForm.controls.items.value.forEach((value, index) => {
      if (this.formArray.value[index].isCorrect && form.valid) {
        this.checked = false;
      }
    });
  }
}

import {Component, OnInit} from '@angular/core';
import {AnswerState} from '../../shared/enums/answer-state';
import {ExTest} from '../../models/ex-test';
import {AdministratedTestState} from '../../shared/enums/administrated-test-state';
import {EndTestModalComponent} from '../../modals/end-test-modal/end-test-modal.component';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-fruition-test',
  templateUrl: './fruition-test.component.html',
  styleUrls: ['./fruition-test.component.css']
})
export class FruitionTestComponent implements OnInit {

  exTest: ExTest = {
    Id: 0,
    Title: 'ciao',
    Date: '20/03/2020',
    URL: 'pippo sowlo',
    MaxScore: 20,
    TestSubject: 'Andrea Guaragna',
    State: AdministratedTestState.Open,
    AdministratedQuestions: [{
      Id: 0,
      Text: 'domanda 1',
      Position: 0,
      AdministratedTestId: 0,
      AdministratedAnswers: [{
        Id: 0,
        Score: 0,
        Text: 'risposta 1',
        AdministratedQuestionId: 0,
        isCorrect: AnswerState.NotCorrect,
        isSelected: false
      },
      {
        Id: 1,
        Score: 5,
        Text: 'risposta 2',
        AdministratedQuestionId: 0,
        isCorrect: AnswerState.Correct,
        isSelected: false
      },
      {
        Id: 2,
        Score: 0,
        Text: 'risposta 3',
        AdministratedQuestionId: 0,
        isCorrect: AnswerState.NotCorrect,
        isSelected: false
      },
      {
        Id: 3,
        Score: 0,
        Text: 'risposta 4',
        AdministratedQuestionId: 0,
        isCorrect: AnswerState.NotCorrect,
        isSelected: false
      }]
    },
      {
        Id: 1,
        Text: 'domanda 2',
        Position: 1,
        AdministratedTestId: 0,
        AdministratedAnswers: [{
          Id: 0,
          Score: 0,
          Text: 'risposta 1',
          AdministratedQuestionId: 0,
          isCorrect: AnswerState.NotCorrect,
          isSelected: false
        },
          {
            Id: 1,
            Score: 5,
            Text: 'risposta 2',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.Correct,
            isSelected: false
          },
          {
            Id: 2,
            Score: 0,
            Text: 'risposta 3',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.NotCorrect,
            isSelected: false
          },
          {
            Id: 3,
            Score: 0,
            Text: 'risposta 4',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.NotCorrect,
            isSelected: false
          }]
      },
      {
        Id: 2,
        Text: 'domanda 3',
        Position: 2,
        AdministratedTestId: 0,
        AdministratedAnswers: [{
          Id: 0,
          Score: 0,
          Text: 'risposta 1',
          AdministratedQuestionId: 0,
          isCorrect: AnswerState.NotCorrect,
          isSelected: false
        },
          {
            Id: 1,
            Score: 5,
            Text: 'risposta 2',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.Correct,
            isSelected: false
          },
          {
            Id: 2,
            Score: 0,
            Text: 'risposta 3',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.NotCorrect,
            isSelected: false
          },
          {
            Id: 3,
            Score: 0,
            Text: 'risposta 4',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.NotCorrect,
            isSelected: false
          }]
      },
      {
        Id: 3,
        Text: 'domanda 4',
        Position: 3,
        AdministratedTestId: 0,
        AdministratedAnswers: [{
          Id: 0,
          Score: 0,
          Text: 'risposta 1',
          AdministratedQuestionId: 0,
          isCorrect: AnswerState.NotCorrect,
          isSelected: false
        },
          {
            Id: 1,
            Score: 5,
            Text: 'risposta 2',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.Correct,
            isSelected: false
          },
          {
            Id: 2,
            Score: 0,
            Text: 'risposta 3',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.NotCorrect,
            isSelected: false
          },
          {
            Id: 3,
            Score: 0,
            Text: 'risposta 4',
            AdministratedQuestionId: 0,
            isCorrect: AnswerState.NotCorrect,
            isSelected: false
          }]
      }]
  };
  i = 0;
  isLast = false;
  isFirst = true;

  constructor( private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  next() {
    if (this.i === this.exTest.AdministratedQuestions.length - 2) {
      this.isLast = true;
    }
    this.i++;
    if (this.i !== 0) {
      this.isFirst = false;
    }
  }

  end() {
 this.modalService.open(EndTestModalComponent);
  }

  previous() {
    if (this.i === 1) {
      this.isFirst = true;
    }
    this.i--;
    if (this.i < this.exTest.AdministratedQuestions.length - 1) {
      this.isLast = false;
    }
  }

  change(index: number) {
    this.exTest.AdministratedQuestions[this.i].AdministratedAnswers[index].isSelected = !this.exTest.AdministratedQuestions[this.i].AdministratedAnswers[index].isSelected;
    return this.i;
  }
}

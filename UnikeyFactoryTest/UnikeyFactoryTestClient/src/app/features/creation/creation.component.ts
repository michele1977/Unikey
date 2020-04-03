import {Test} from '../../models/test';
import {Component, Input} from '@angular/core';
import {TestService} from '../../services/test.service';
import * as moment from 'moment';
import {Router} from '@angular/router';
import {Question} from '../../models/question';
import {LoaderService} from '../../services/loader.service';

@Component({
  selector: 'app-creation',
  template: `
    <div class="row">
      <div class="col-6">
        <div class="pageTitle">
          <h1 id="pageTitle">Create Test</h1>
        </div>
      </div>
    </div>
    <form #form="ngForm">
      <hr>
      <div class="form-group create-div">
        <input type="text" name="title" class="form-control" placeholder="Test Name.." [ngModel]="title" required>
        <label><b>Date: </b>{{time | date: 'dd/MM/yy H:mm:ss'}}</label>
      </div>
      <br>
      <div class="row create-row">
        <div class="col-6">
          <app-question-list [test]="test" (showForm)='visibility($event)' [enable]="setVisibility"></app-question-list>
        </div>
        <div *ngIf="setVisibility" class="col-6 ">
          <app-question-form (questionInsert)="addQuestion($event)"></app-question-form>
        </div>
      </div>
      <hr>
      <div style="text-align: right">
        <button class="btn btn-outline-primary create-button" (click)="createTest(form)" [disabled]="!form.valid" title="Save Test">
          <i class="fa fa-check"></i>
        </button>
      </div>
    </form>
  `,
  styleUrls: ['./creation.component.css']
})
export class CreationComponent {
  showForm = false;
  title = '';
  time: string;
  result = false;
  test: Test = {
    Title: '',
    URL: '',
    Date: null,
    Id: 0,
    Questions: [],
    NumberOfExTest: 0,
    NumberOfTest: 0,
    OpenedExTestNumber: 0
  };
  setVisibility: boolean;

  constructor(private service: TestService, private router: Router, private loader: LoaderService) {
    this.time = moment().format();
    setInterval(() => {
      this.time = moment().format();
    }, 1000);
  }

  createTest(form) {
    this.loader.publish('show');
    this.test.Title = form.value.title;
    this.test.Date = moment().format('DD MM YY H:mm:ss').toString();

    this.service.createTest(this.test).then(res => {
        this.router.navigateByUrl('testList');
        this.loader.publish('hide');
    }, error => {
        alert('Error while creating');
        this.loader.publish('hide');
    });
    this.loader.publish('hide');
  }

  visibility(setVisibility) {
    this.setVisibility = setVisibility;
  }

  addQuestion(question: Question) {
    question.Position = this.test.Questions.length;
    this.test.Questions.push(question);
    this.setVisibility = false;
  }
}

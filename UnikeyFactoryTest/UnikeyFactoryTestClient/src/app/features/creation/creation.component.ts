import {Test} from '../../models/test';
import {Component, Input} from '@angular/core';
import {TestService} from '../../services/test.service';
import * as moment from 'moment';

@Component({
  selector: 'app-creation',
  template: `
    <form #form="ngForm">
      <div class="form-group create-div">
        <input type="text" name="title" class="form-control" placeholder="Test Name.." [ngModel]="title">
        <label><b>Date: </b>{{time | date: 'dd/MM/yy H:mm:ss'}}</label>
      </div>
      <hr>
      <div class="row create-row">
        <div class="col-6">
          <app-question-list [test]="test" (showForm) = 'visibility($event)'></app-question-list>
        </div>
        <div *ngIf="setVisibility" class="col-6">
          PUT HERE THE QUESTION FORM
        </div>
      </div>
      <hr>
      <div style="text-align: right">
        <button class="btn btn-primary create-button" (click)="createTest(form)">Done</button>
      </div>
    </form>
  `,
  styles: [
    `
    .create-button{
      margin: 10px 0px 0px 45%;
    }
    .create-div{
      width: 50%;
      margin-left: 25%;
    }
    `
  ]
})
export class CreationComponent {
  showForm = false;
  title = '';
  time: string;
  result = false;
  test: Test = {
    Title: '',
    Date: null,
    Id: 0,
    Questions: null,
  };
  setVisibility: boolean;

  constructor(private service: TestService) {
    this.time = moment().format();
    setInterval(() => {
      this.time = moment().format();
    }, 1000);
  }

  createTest(form) {
    this.test.Title = form.value.title;
    this.test.Date = moment().format('DD MM YY H:mm:ss');

    this.service.createTest(this.test).pipe().subscribe( res => {
        this.result = true;
        this.showForm = true;
        alert('Redirect to implement');
    }, error => {
        alert('Error while creating');
    });

  }

  visibility(setVisibility) {
    this.setVisibility = setVisibility;
  }
}

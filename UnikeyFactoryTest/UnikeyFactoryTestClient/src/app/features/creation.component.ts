import { Component } from '@angular/core';
import {Test} from '../models/test';

@Component({
  selector: 'app-creation',
  template: `
    <form #form="ngForm">
      <div class="form-group create-div">
        <input type="text" name="title" class="form-control" [disabled]="showForm" placeholder="Test Name.." [ngModel]="title">
        <label><b>Date: </b>{{time | date: 'dd/MM/yyyy h:m'}}</label>
        <br>
        <button class="btn btn-primary create-button"[disabled]="showForm" (click)="createTest(form)">Create</button>
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
  time: number = Date.now();
  test: Test = {
    Title: '',
    Date: undefined,
    Id: 0,
    Questions: null,
  };
  constructor() { }

  createTest(form) {
    this.test.Title = form.value.title;
    this.test.Date = form.value.time;
    console.log(this.test);
    this.showForm = true;
  }

}

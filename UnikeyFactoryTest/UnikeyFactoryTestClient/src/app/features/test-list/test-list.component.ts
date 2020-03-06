import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {Test} from '../../models/test';
import * as moment from 'moment';
import {IconsService} from '../../services/icons.service';


@Component({
  selector: 'app-test-list',
  template: `
    <div class="main-div">
        <div class="title">
          <h2>Test List</h2>
        </div>
      <div class="create-test-a-div">
        <fa-icon [icon]="icons.faPlus" class="btn btn-success create-test-a" (click)="loadCreatePage()"></fa-icon>
      </div>
    </div>

    <div class="alert alert-danger" *ngIf="showDeleteError" >
        <strong>Holy holy maccaroni!</strong> An error occurred while deleting the test
        <button type="button" class="close" aria-label="Close" (click)="closeErrorAlert()">&times;</button>
    </div>

    <hr>

    <div style="display: flex;">
      <div style="float:left;">
        <form #form="ngForm" style="margin-bottom: 10px;" >
          <div class="btn btn-group">
            <input type="search" [ngModel]="textFilter" name="textFilter" class="form-control">
            <span class="glyphicon glyphicon-remove-circle"></span>
            <button type="submit" class="btn btn-primary" (click)="search(form)">Search</button>
          </div>
        </form>
      </div>
      <div style="float: right; margin-left: auto;">
        <div style="margin-top: 5px;">
        <label>Elements per page</label>
          <select name="pageSizeSelection" (change)="resizePage()">
            <option [ngModel]="pageSize"></option>
          </select>

        </div>
      </div>
    </div>
    <table class="table">
      <tr>
        <th class="text-center">Id</th>
        <th class="text-center">Title</th>
        <th class="text-center">Date</th>
        <th class="text-center">URL</th>
        <th class="text-center">Number of tests</th>
        <th class="text-center">Number of tests opened</th>
        <th class="text-center"></th>
        <th class="text-center">Actions</th>
      </tr>
      <tr *ngFor="let test of tests | filter:textFilter">
        <td class="text-center">{{test.Id}}</td>
        <td class="text-center">{{test.Title}}</td>
        <td class="text-center">{{test.Date}}</td>
        <td class="text-center">Url to add</td>
        <td class="text-center">5</td>
        <td class="text-center">2</td>
        <td class="text-center"></td>
        <td class="text-center" style="width: 20%">
          <div>
            <fa-icon [icon]="icons.faInfo" (click)="showContent()"></fa-icon> |
            <fa-icon [icon]="icons.faTrash" (click)="deleteTest()"></fa-icon> |
            <fa-icon [icon]="icons.faList" (click)="testDetails()"></fa-icon> |
            <fa-icon [icon]="icons.faMailForward" (click)="sendMail()"></fa-icon>
          </div>
        </td>
      </tr>
    </table>
  `,
  styles: [`
    .main-div {
      display: flex;
    }
    .title {
      float: left;
    }
    .create-test-a-div {
      margin-left: 20px;
    }
    .create-test-a{
      float: left;
    }
  `]
})
export class TestListComponent {

  showDeleteError = false;
  pageSize = 0;
  textFilter = '';
  tests: Test[] = [];
  constructor(private router: Router, private icons: IconsService) {
    for (let i = 0; i < 10; i++) {
      const myTest: Test = {
        Date: moment().format('DD/MM/YY H:mm'),
        Title: 'title ' + i,
        Id: i,
        Questions: null
      };
      this.tests.push(myTest);
    }
  }
  loadCreatePage() {
    this.router.navigateByUrl('create');
  }
  search(form) {
    this.textFilter = form.value.textFilter;
  }
  resizePage() {}

  closeErrorAlert() {}

  showContent() {}

  deleteTest() {}

  testDetails() {}

  sendMail() {}
}

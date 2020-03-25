import {Component, Input, OnInit} from '@angular/core';
import {Test} from '../../models/test';
import {ExTestService} from '../../services/exTest.service';
import {ExTest} from '../../models/ex-test';
import {IconsService} from '../../services/icons.service';
import * as moment from 'moment';
import {NgForm} from '@angular/forms';
import {TestList} from '../../models/test-list';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {isUndefined} from 'util';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-test-details-modal',
  templateUrl: './test-details-modal.component.html',
  styleUrls: ['./test-details-modal.component.css']
})
export class TestDetailsModalComponent implements OnInit {

  @Input() myModalTest: Test;

  exTests: ExTest[];
  textFilter = '';
  pageNum = 1;
  pageSize = 10;
  isFirstPage = true;
  isLastPage = true;
  pages: number;
  isEmpty = true;

  constructor(public activeModal: NgbActiveModal,
              private service: ExTestService,
              public icons: IconsService,
              private loader: LoaderService) { }


  ngOnInit(): void {
    this.loader.publish('show');
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).then(value => {
      this.exTests = value as ExTest[];
      console.log();
      if (value[0]) {
        this.pages = Math.ceil(value[0].NumberOfExTests / this.pageSize);
        if (this.pages > this.pageNum) {
          this.isLastPage = false;
        }
        this.isEmpty = false;
      } else {
        this.pages = 1;
      }
    });
    this.loader.publish('hide');
  }

  nextPage() {
    this.loader.publish('show');
    this.pageNum += 1;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).then(value => {
      this.exTests = value as ExTest[];
    });
    if (this.isFirstPage) {
      this.isFirstPage = false;
    }
    if (this.pageNum === this.pages) {
      this.isLastPage = true;
    }
    this.loader.publish('hide');
  }

  previousPage() {
    this.loader.publish('show');
    this.pageNum -= 1;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).then(value => {
      console.log(value);
      this.exTests = value as ExTest[];
    });
    if (this.pageNum === 1) {
      this.isFirstPage = true;
    }
    this.loader.publish('hide');
  }

  firstPage() {
    this.loader.publish('show');
    this.pageNum = 1;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).then(value => {
      this.exTests = value as ExTest[];
    });
    this.isFirstPage = true;
    this.isLastPage = false;
    this.loader.publish('hide');
  }

  lastPage() {
    this.loader.publish('show');
    this.pageNum = this.pages;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).then(value => {
      this.exTests = value as ExTest[];
      this.loader.publish('hide');
    });
    this.isFirstPage = false;
    this.isLastPage = true;
  }

  search(form: NgForm) {
    this.loader.publish('show');
    this.textFilter = form.value.textFilter;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).then(value => {
      this.exTests = value as ExTest[];
    });
    this.loader.publish('hide');
  }
}

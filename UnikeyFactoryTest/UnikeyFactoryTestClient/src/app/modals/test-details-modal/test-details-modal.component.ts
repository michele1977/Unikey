import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {Test} from '../../models/test';
import {ExTestService} from '../../services/exTest.service';
import {ExTest} from '../../models/ex-test';
import {IconsService} from '../../services/icons.service';
import * as moment from 'moment';
import {NgForm} from '@angular/forms';
import {TestList} from '../../models/test-list';

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
  isLastPage = false;
  pages: number;

  constructor(public activeModal: NgbActiveModal, private service: ExTestService, public icons: IconsService) { }


  ngOnInit(): void {
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).subscribe(value => {
      this.exTests = value as ExTest[];
      this.pages = Math.ceil(value[0].NumberOfExTests / this.pageSize);
    });
  }

  nextPage() {
    this.pageNum += 1;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).subscribe(value => {
      this.exTests = value as ExTest[];
    });
    if (this.isFirstPage) {
      this.isFirstPage = false;
    }
    if (this.pageNum === this.pages - 1) {
      this.isLastPage = true;
    }
  }

  previousPage() {
    this.pageNum -= 1;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).subscribe(value => {
      console.log(value);
      this.exTests = value as ExTest[];
    });
    if (this.pageNum > 1) {
      this.isFirstPage = true;
    }
  }

  firstPage() {
    this.pageNum = 1;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).subscribe(value => {
      this.exTests = value as ExTest[];
    });
    this.isFirstPage = true;
  }

  lastPage() {
    this.pageNum = this.pages;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).subscribe(value => {
      this.exTests = value as ExTest[];
    });
    this.isLastPage = true;
  }

  search(form: NgForm) {
    this.textFilter = form.value.textFilter;
    this.service.getExTestByTestId(this.pageNum, this.pageSize, this.textFilter, this.myModalTest.Id).subscribe(value => {
      this.exTests = value as ExTest[];
    });
  }
}

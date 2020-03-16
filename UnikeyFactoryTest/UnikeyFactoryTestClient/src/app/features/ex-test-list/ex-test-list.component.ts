import { Component, OnInit } from '@angular/core';
import { IconsService } from 'src/app/services/icons.service';
import { ExTestList } from 'src/app/models/ex-test-list';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ExTestListService } from 'src/app/services/ex-test-list.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-ex-test-list',
  templateUrl: './ex-test-list.component.html',
  styleUrls: ['./ex-test-list.component.css']
})
export class ExTestListComponent {

  showDeleteError = false;
  pageNum = 1;
  pageSize = 10;
  textFilter = '';
  atLast = false;
  tests: ExTestList;
  pages = 0;
  options: any[] = [10, 20, 40, 50, 60];

    constructor(private router: Router,
                public icons: IconsService,
                private exTestService: ExTestListService,
                private loader: LoaderService) {
                  this.loader.publish('show');
                  this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
        this.tests = data as ExTestList;
        this.loader.publish('hide');
        this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
      }, () => {
        this.loader.publish('hide');
        this.router.navigateByUrl('error'); }
        );
    }

  loadCreatePage() {
    this.router.navigateByUrl('create');
  }

  search(form) {
    this.loader.publish('show');
    this.textFilter = form.value.textFilter;
    this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.loader.publish('hide');
      this.tests = data as ExTestList;
      this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
    }, () => {this.router.navigateByUrl('error');
              this.loader.publish('hide'); });
  }

  seeExTest(id: number) {
    this.router.navigateByUrl('seeExTest/' + id);
  }

  resizePage(event: any) {
    this.loader.publish('show');
    this.pageSize = event.target.value;
    this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as ExTestList;
      this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
      if (this.atLast === true) {
        this.pageNum = this.pages;
      }
      this.loader.publish('hide');
    }, () => {this.router.navigateByUrl('error');
              this.loader.publish('hide'); });
  }

  NextPage() {
    this.loader.publish('show');
    this.pageNum += 1;
    this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as ExTestList;
      this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
      this.loader.publish('hide');
    }, () => this.router.navigateByUrl('error'));
    if (this.pageNum === this.pages) {
      this.atLast = true;
      this.loader.publish('hide');
    }
  }

  lastPage() {
    this.loader.publish('show');
    this.exTestService.getExTests(this.pages, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as ExTestList;
      this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
      this.pageNum = this.pages;
      this.atLast = true;
      this.loader.publish('hide');
    }, () => this.router.navigateByUrl('error'))
    this.loader.publish('hide');
  }

  firstPage() {
    this.loader.publish('show');
    this.exTestService.getExTests(1, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as ExTestList;
      this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
      this.atLast = false;
      this.pageNum = 1;
      this.loader.publish('hide');
    }, () => this.router.navigateByUrl('error'));
    this.loader.publish('hide');
  }

  previousPage() {
    this.loader.publish('show');
    this.pageNum -= 1;
    this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as ExTestList;
      this.pages = Math.ceil(data[0].TotalNumberOfExTests / this.pageSize);
      this.atLast = false;
      this.loader.publish('hide');
    }, () => this.router.navigateByUrl('error'));
    this.loader.publish('hide');
  }

}

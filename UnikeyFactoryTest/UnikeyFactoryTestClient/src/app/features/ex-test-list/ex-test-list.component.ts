import {Component, HostListener, Inject, OnInit} from '@angular/core';
import { IconsService } from 'src/app/services/icons.service';
import { ExTestList } from 'src/app/models/ex-test-list';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ExTestListService } from 'src/app/services/ex-test-list.service';
import { LoaderService } from 'src/app/services/loader.service';
import {DOCUMENT} from '@angular/common';
import {WINDOW} from '../../services/window-ref.service';
import {ExTest} from '../../models/ex-test';

@Component({
  selector: 'app-ex-test-list',
  templateUrl: './ex-test-list.component.html',
  styleUrls: ['./ex-test-list.component.css']
})
export class ExTestListComponent {

  pageNum = 1;
  pageSize = 10;
  textFilter = '';
  tests: ExTest[];
  numberOfTests: number;

    constructor(private router: Router,
                public icons: IconsService,
                private exTestService: ExTestListService,
                private loader: LoaderService,
                @Inject(DOCUMENT) private document: Document,
                @Inject(WINDOW) private window) {
                  this.loader.publish('show');
                  this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).then(data => {
                      this.numberOfTests = data[0].NumberOfExTests;
                      console.log(data[0].NumberOfExTests);
                      this.tests = data;
                      this.loader.publish('hide');
      }, () => {
        this.loader.publish('hide');
        this.router.navigateByUrl('error'); }
        );
    }

  search(form) {
    this.loader.publish('show');
    this.textFilter = form.value.textFilter;
    this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).then(data => {
      this.loader.publish('hide');
      this.numberOfTests = data[0].NumberOfExTests;
      this.tests = data;
    }, () => {this.router.navigateByUrl('error');
              this.loader.publish('hide'); });
  }

  seeExTest(id: number) {
    this.router.navigateByUrl('seeExTest/' + id);
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const pos = document.documentElement.scrollTop + document.documentElement.offsetHeight;
    const max = document.documentElement.scrollHeight;
    if (pos >= max - 1 && pos <= max) {
      console.log(this.numberOfTests);
      if (this.tests.length < this.numberOfTests) {
        console.log('ciao');
        this.loader.publish('show');
        this.pageNum += 1;
        this.exTestService.getExTests(this.pageNum, this.pageSize, this.textFilter).then(data => {
          data.forEach(value => {
            this.tests.push(value);
          });
          this.loader.publish('hide');
        }, () => this.router.navigateByUrl('error'));
      }
    }
  }
}

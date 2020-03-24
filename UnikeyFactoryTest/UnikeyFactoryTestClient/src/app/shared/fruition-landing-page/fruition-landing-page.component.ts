import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestSubject } from 'src/app/models/testSubject';
import { LoaderService } from 'src/app/services/loader.service';
import {ExTest} from '../../models/ex-test';
import {TestService} from '../../services/test.service';
import {ExTestService} from '../../services/exTest.service';

@Component({
  selector: 'app-fruition-landing-page',
  templateUrl: './fruition-landing-page.component.html',
  styleUrls: ['./fruition-landing-page.component.css']
})
export class FruitionLandingPageComponent implements OnInit {
  guid: string;
  subject: TestSubject;
  exTest: ExTest;
  exTestId: number;
  showBeginTestForm = true;
  showFruitionTestForm = false;
  showStatisticPage = false;

  constructor(private route: ActivatedRoute,
              private service: ExTestService,
              private loader: LoaderService) { }

  ngOnInit(): void {
    this.loader.publish('show');

    this.route.queryParams
    .subscribe(params => {
      this.guid = params.guid;
    });

    this.loader.publish('hide');
  }

  toggleFirstQuestion(event) {
    this.loader.publish('show');
    this.subject = JSON.parse(event);
    this.service.getExTestByTestUrl(this.guid, this.subject).subscribe(data => {
      this.exTest = data;
      this.showBeginTestForm = false;
      this.showFruitionTestForm = true;
      this.loader.publish('hide');
    });
  }

  saveExTest(event) {
    this.loader.publish('show');
    this.exTest = event;
    this.service.saveExTest(this.exTest).subscribe(data => {
      this.exTest = data;
      this.exTestId = this.exTest.Id;
      this.showFruitionTestForm = false;
      this.showStatisticPage = true;
      this.loader.publish('hide');
    });
  }
}

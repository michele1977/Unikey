import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Test } from 'src/app/models/test';
import { TestService } from 'src/app/services/test.service';
import { TestSubject } from 'src/app/models/testSubject';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-fruition-landing-page',
  templateUrl: './fruition-landing-page.component.html',
  styleUrls: ['./fruition-landing-page.component.css']
})
export class FruitionLandingPageComponent implements OnInit {
  testUrl: string;
  test: Test;  // Da mandare in input nel child di fruizione del test
  subject: TestSubject;  // Da mandare in input nel child di fruizione del test
  showBeginTestForm = true;  // Diventa false quando l'utente inserisce i suoi dati

  constructor(private route: ActivatedRoute,
              private service: TestService,
              private loader: LoaderService) { }

  ngOnInit(): void {
    this.loader.publish('show');

    this.route.queryParams
    .subscribe(params => {
      this.testUrl = params.guid;
    });
    console.log(this.testUrl);

    this.getTest();

    this.loader.publish('hide');
  }

  getTest() {
    this.service.getTestByUrl(this.testUrl).subscribe(data => this.test = data);
  }

  toggleFirstQuestion(event) {
    this.subject = JSON.parse(event);
    this.showBeginTestForm = false;
  }
}

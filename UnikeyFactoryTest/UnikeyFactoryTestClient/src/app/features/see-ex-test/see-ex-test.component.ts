import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, ParamMap, Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';
import {switchMap} from 'rxjs/operators';
import {ExTestService} from '../../services/exTest.service';
import {ExTest} from '../../models/ex-test';
import * as moment from 'moment';

@Component({
  selector: 'app-see-ex-test',
  templateUrl: './see-ex-test.component.html',
  styleUrls: ['./see-ex-test.component.css']
})
export class SeeExTestComponent implements OnInit {

  exTest: ExTest;

  constructor(
    private service: ExTestService,
    private router: Router,
    public icons: IconsService,
    private route: ActivatedRoute,
  ) {
/*    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.service.getExTestById(parseInt(params.get('id'), 10)))
    ).subscribe(data => {
        this.exTest = data;
      },
      () => this.router.navigateByUrl('error'));*/
    const myExTest: ExTest = {
      Date: moment().format('DD/MM/YY H:mm'),
      Title: 'title 0',
      Id: 0,
      ExQuestions: [{
        Text: 'Prova Domanda',
        Id: 0,
        AdministratedTestId: 0,
        Position: 0,
        ExAnswers: [{
          AdministratedQuestionId: 0,
          Id: 0,
          IsCorrect: 2,
          IsSelected: true,
          Score: 0,
          Text: 'Prova risposta'
        }]
      }],
      Score: 100,
      MaxScore: 100,
      State: 2,
      TestSubject: 'Andrea77'
    };
    this.exTest = myExTest;
  }

  ngOnInit(): void {
  }

}

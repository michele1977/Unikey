import { Component, OnInit } from '@angular/core';
import {Question} from '../../../models/question';
import {TestService} from '../../../services/test.service';
import {Test} from '../../../models/test';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-testcontent',
  templateUrl: './testcontent.component.html',
  styleUrls: ['./testcontent.component.css']
})
export class TestcontentComponent {
test: Test;
maxScore: number;

  constructor(private service: TestService) { this.getQuestions(60); }

  // For debug
  getQuestions(id) {
    this.service.getTest(id).pipe(map((res: Response) => res.json()))
      .subscribe(
        async data => {

          this.test = await data;
        });
  }

  gerMaxScore() {
    const max = this.test.Questions.reduce((previous, current) => {
      return (previous.Answers.filter(a => a.IsCorrect)
        .map(answer => answer.Score)
        .reduce((sum, curr) => sum + curr, 0)
      >
        current.Answers.filter(a => a.IsCorrect)
        .map(answer => answer.Score)
        .reduce((sum, curr) => sum + curr, 0)) ? previous : current;
    });
  }

}

import { Component, OnInit } from '@angular/core';
import {TestService} from '../../../services/test.service';
import {Test} from '../../../models/test';
import {Router, ActivatedRoute, ParamMap} from '@angular/router';
import { IconsService } from 'src/app/services/icons.service';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-testcontent',
  templateUrl: './testcontent.component.html',
  styleUrls: ['./testcontent.component.css']
})
export class TestcontentComponent {
test: Test;
tempTest: Test;
maxScore: number;
areThereModifies = false;
isEditable: boolean[] = [];
isThereAnError: boolean;

  constructor(
    private service: TestService,
    private router: Router,
    public icons: IconsService,
    private route: ActivatedRoute,
    ) {
      this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
      this.service.getTest(parseInt(params.get('id'), 10)))
    ).subscribe(data => {
      this.test = data;
      this.tempTest = JSON.parse(JSON.stringify(this.test));
    },
      () => this.router.navigateByUrl('error'));
    }

    toggle(i: number) {
      this.isEditable[i] = !this.isEditable[i];
    }

  getMaxScore(): number {
    let res = 0;
    for (const question of this.test.Questions) {
      const max = question.Answers.filter(answer => answer.IsCorrect === 1)
      .map(answer => answer.Score)
      .reduce((prev, curr) => prev + curr, 0);
      res += max;
    }
    return res;
  }

  edit(obj) {
    this.test.Questions[obj.index].Text = obj.question.questionText;
    this.test.Questions[obj.index].Answers = obj.question.answers;
    this.areThereModifies = true;
    this.isEditable[obj.index] = false;
  }

  undo() {
    console.log(this.tempTest);
    this.test = this.tempTest;
    this.areThereModifies = false;
  }

  saveChanges(test: Test) {
    this.service.updateTest(test).subscribe(data => {this.tempTest = JSON.parse(JSON.stringify(this.test));
                                                     this.areThereModifies = false; },
      error => this.isThereAnError = true);
  }
}

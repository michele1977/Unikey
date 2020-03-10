import { Component, OnInit, Input } from '@angular/core';
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
export class TestcontentComponent implements OnInit {
test: Test;
maxScore: number;
areThereModifies: boolean;
isEditable: boolean[] = [];

  constructor(
    private service: TestService,
    private router: Router,
    public icons: IconsService,
    private route: ActivatedRoute,
    ) {}


    ngOnInit() {
      this.route.paramMap.pipe(
        switchMap((params: ParamMap) =>
        this.service.getTest(parseInt(params.get('id'), 10)))
      ).subscribe(data => this.test = data,
        () => this.router.navigateByUrl('error'));

      this.maxScore = this.getMaxScore();
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
}

import {Component} from '@angular/core';
import {ExTest} from '../../models/ex-test';
import {ExTestService} from '../../services/exTest.service';
import {ActivatedRoute, ParamMap, Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';
import {switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-statistic-page',
  templateUrl: './statistic-page.component.html',
  styleUrls: ['./statistic-page.component.css']
})
export class StatisticPageComponent {
  exTest = new ExTest();
  time = new Date();
  date = this.time.getTime();

  constructor(
    private service: ExTestService,
    private router: Router,
    public icons: IconsService,
    private route: ActivatedRoute,
  ) {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.service.getExTestById(parseInt(params.get('id'), 10)))
    ).subscribe(data => {
        console.log(data);
        this.exTest = data as ExTest;
      },
      error => {
        console.error(error);
        this.router.navigateByUrl('error');
      });
  }

  percentage() {
    return (this.exTest.Score * 100) / this.exTest.MaxScore;
  }

  timer() {
    const datadaconvertire = this.exTest.Date;
    const datenumber = new Date(datadaconvertire).getTime();
    const datenownumber = new Date(this.date).getTime();
    return (datenownumber - datenumber);
  }

  formatColors = (percentage: number): string => {
    if (percentage >= 80) {
      return '#78C000';
    } else if (percentage >= 50) {
      return '#d6e502';
    } else if (percentage <= 20) {
      return '#e53a00';
    }
  }

  formatSubtitle = (percentage: number): string => {
    if (percentage >= 80) {
      return 'Congratulationi: ';
    } else if (percentage >= 50) {
      return 'Bravo: ';
    } else if (percentage <= 20) {
      return 'Accidenti: ';
    } else {
      return 'Not started';
    }

  }
}

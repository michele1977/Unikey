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
    return (this.exTest.Score / this.exTest.MaxScore) * 100;
  }

  timer() {
    const datadaconvertire = this.exTest.Date;
    const datenumber = new Date(datadaconvertire).getTime();
    const datenownumber = new Date(this.date).getTime();
    return (datenownumber - datenumber);
  }
}

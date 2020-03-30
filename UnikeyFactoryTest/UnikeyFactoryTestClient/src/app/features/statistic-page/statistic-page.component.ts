import {Component, Input, OnInit} from '@angular/core';
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
export class StatisticPageComponent implements OnInit {
  constructor(
    private service: ExTestService,
    public icons: IconsService
  ) { }

  @Input() exTestId: number;

  exTest = new ExTest();
  time = new Date();
  date = this.time.getTime();

  ngOnInit() {
    console.log(this.exTestId);
    this.service.getExTestById(this.exTestId).then(data => {
      this.exTest = data;
    });
  }

  timer() {
    const datadaconvertire = this.exTest.Date;
    const datenumber = new Date(datadaconvertire).getTime();
    const datenownumber = new Date(this.date).getTime();
    return (datenownumber - datenumber);
  }
  percentage() {
    return (this.exTest.Score * 100) / this.exTest.MaxScore;
  }
  formatColors = (percen: number): string => {
    if (percen >= 80) {
      return '#3ac000';
    } else if (percen >= 60) {
      return '#d6e502';
    } else if (percen >= 40) {
      return '#e55e00';
    } else if (percen >= 20) {
      return '#e5001a';
  }
  }

  formatSubtitle = (percentage: number): string => {
    if (percentage >= 80) {
      return 'Congratulationi: ';
    } else if (percentage >= 60) {
      return 'Bravo: ';
    } else if (percentage >= 40) {
      return 'Bene: ';
    } else if (percentage >= 20) {
      return 'Accidenti: ';
    } else if (percentage <= 20) {
      return 'Peccato: ';
    } else {
      return 'Not started';
    }

  }
}

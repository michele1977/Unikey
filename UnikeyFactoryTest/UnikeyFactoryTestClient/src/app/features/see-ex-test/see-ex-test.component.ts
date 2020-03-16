import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, ParamMap, Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';
import {switchMap} from 'rxjs/operators';
import {ExTestService} from '../../services/exTest.service';
import {ExTest} from '../../models/ex-test';
import * as moment from 'moment';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-see-ex-test',
  templateUrl: './see-ex-test.component.html',
  styleUrls: ['./see-ex-test.component.css']
})
export class SeeExTestComponent implements OnInit {

  exTest = new ExTest();

  constructor(
    private service: ExTestService,
    private router: Router,
    public icons: IconsService,
    private route: ActivatedRoute,
    private loader: LoaderService
  ) {
    this.loader.publish('show');

    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.service.getExTestById(parseInt(params.get('id'), 10)))
    ).subscribe(data => {
        console.log(data);
        this.exTest = data as ExTest;
        this.loader.publish('hide');
      },
      error => {
        console.error(error);
        this.loader.publish('hide');
        this.router.navigateByUrl('error');
      });
  }


  ngOnInit(): void { }

}

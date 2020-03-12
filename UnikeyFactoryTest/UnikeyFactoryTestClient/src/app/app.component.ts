import {Component, Input, OnInit, OnDestroy} from '@angular/core';
import {faInfo} from '@fortawesome/free-solid-svg-icons';
import {LoaderService} from './services/loader.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'UnikeyFactoryTestClient';
  showLoader = false;
  private subscription;
  constructor(private loaderService: LoaderService ) { }
  ngOnInit() {
    this.subscription = this.loaderService.on('show').subscribe(() => this.viewLoader());
    this.subscription = this.loaderService.on('hide').subscribe(() => this.hideLoader());
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  viewLoader() {
    this.showLoader = true;
  }
  hideLoader() {
    this.showLoader = false;
  }
}

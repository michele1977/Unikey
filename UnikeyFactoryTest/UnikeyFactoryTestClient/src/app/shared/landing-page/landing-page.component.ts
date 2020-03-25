import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {SideBarService} from '../../services/side-bar.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit, OnDestroy {
  isVisible = true;

  setVisible() {
    this.isVisible = !this.isVisible;
  }
  constructor(public nav: SideBarService) { }

  ngOnInit(): void {
    this.nav.hide();
  }
  ngOnDestroy() {
    this.nav.show();
  }
}

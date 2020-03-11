import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  isVisible = true;

  setVisible(bool: boolean) {
    this.isVisible = bool;
  }
  constructor() { }

  ngOnInit(): void {
  }

}

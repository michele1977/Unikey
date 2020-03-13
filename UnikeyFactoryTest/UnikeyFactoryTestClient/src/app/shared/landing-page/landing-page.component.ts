import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  isVisible = true;

  setVisible() {
    this.isVisible = !this.isVisible;
  }
  constructor() { }

  ngOnInit(): void {
  }

}

import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent {

  constructor(private router: Router) {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
  }

  goToHomePage(){
    this.router.navigateByUrl('testList');
  }
}

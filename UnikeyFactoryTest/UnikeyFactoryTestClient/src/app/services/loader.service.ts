import { Injectable } from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  constructor() { }
  private subjects: Subject<any>[] = [];

  publish(eventName: string) {
    this.subjects[eventName] = this.subjects[eventName] || new Subject();

    this.subjects[eventName].next();
  }

  on(eventName: string): Observable<any> {

    this.subjects[eventName] = this.subjects[eventName] || new Subject();


    return this.subjects[eventName].asObservable();
  }
}

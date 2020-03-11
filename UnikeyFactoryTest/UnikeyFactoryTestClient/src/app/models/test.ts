import {Question} from './question';

export class Test {
  Id: number;
  Date: string;
  URL: string;
  Title: string;
  Questions: Question[];
  NumberOfTest: number;
  NumberOfExTest: number;
  OpenedExTestNumber: number;
}

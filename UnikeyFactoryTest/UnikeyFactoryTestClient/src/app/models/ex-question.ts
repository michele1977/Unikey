import {ExAnswer} from './ex-answer';

export class ExQuestion {
  Id?: number;
  Text: string;
  AdministratedTestId: number;
  Position: number;
  AdministratedAnswers: ExAnswer[];
}

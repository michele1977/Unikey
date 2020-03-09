import {Answer} from './answer';

export class Question {
  Id: number;
  TestId: number;
  Text: string;
  Position: number;
  Answers: Answer[];
}

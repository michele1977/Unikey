import {AnswerState} from '../shared/enums/answer-state';

export class Answer {
  Id: number;
  QuestionId?: number;
  Text: string;
  Score: number;
  IsCorrect: AnswerState;
}

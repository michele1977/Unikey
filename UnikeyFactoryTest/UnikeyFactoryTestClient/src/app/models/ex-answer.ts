import {AnswerState} from '../shared/enums/answer-state';

export class ExAnswer {
  Id?: number;
  Text: string;
  IsCorrect: AnswerState;
  IsSelected: boolean;
  AdministratedQuestionId: number;
  Score: number;
}

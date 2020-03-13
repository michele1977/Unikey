import {AnswerState} from '../shared/enums/answer-state';
import {ExQuestion} from './ex-question';

export class ExAnswer {
  Id?: number;
  Text: string;
  isCorrect: AnswerState;
  isSelected: boolean;
  AdministratedQuestionId: number;
  Score: number;
}

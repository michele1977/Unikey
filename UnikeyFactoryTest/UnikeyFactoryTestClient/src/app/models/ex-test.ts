import {AdministratedTestState} from '../shared/enums/administrated-test-state';
import {ExQuestion} from './ex-question';

export class ExTest {
  Id?: number;
  URL?: string;
  TestId?: number;
  TestSubject: string;
  Date: Date;
  Title: string;
  MaxScore: number;
  State?: AdministratedTestState;
  Score: number;
  ExQuestions: ExQuestion[];
}

import {AdministratedTestState} from '../shared/enums/administrated-test-state';
import {ExQuestion} from './ex-question';

export class ExTest {
  Id?: number;
  URL?: string;
  TestId?: number;
  TestSubject: string;
  Date: string;
  Title: string;
  MaxScore: number;
  State?: AdministratedTestState;
  Score?: number;
  NumberOfExTests?: number;
  AdministratedQuestions: ExQuestion[];
}

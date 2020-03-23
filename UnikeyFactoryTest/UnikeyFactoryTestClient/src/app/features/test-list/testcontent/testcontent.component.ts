import {Component, EventEmitter, Input} from '@angular/core';
import {TestService} from '../../../services/test.service';
import {Test} from '../../../models/test';
import {ActivatedRoute, ParamMap, Router} from '@angular/router';
import {IconsService} from 'src/app/services/icons.service';
import {switchMap} from 'rxjs/operators';
import {Question} from '../../../models/question';

@Component({
  selector: 'app-testcontent',
  templateUrl: './testcontent.component.html',
  styleUrls: ['./testcontent.component.css']
})
export class TestcontentComponent {
  @Input() questionInsert: EventEmitter<Question> = new EventEmitter<Question>();
  test: Test;
tempTest: Test;
question: Question = {
    Id: 0,
    Position: 0,
    Text: '',
    TestId: 0,
    Answers: []
  };
maxScore: number;
areThereModifies = false;
isEditable: boolean[] = [];
isThereAnError: boolean;
text: string;
public isButtonVisible = false;

  constructor(
    private service: TestService,
    private router: Router,
    public icons: IconsService,
    private route: ActivatedRoute,
    ) {
      this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
      this.service.getTest(parseInt(params.get('id'), 10)))
    ).subscribe(data => {
      this.test = data;
      this.tempTest = JSON.parse(JSON.stringify(this.test));
    },
      () => this.router.navigateByUrl('error'));
    }

    toggle(i: number) {
      this.isEditable[i] = !this.isEditable[i];
    }

  getMaxScore(): number {
    let res = 0;
    for (const question of this.test.Questions) {
      const max = question.Answers.filter(answer => answer.IsCorrect === 1)
      .map(answer => answer.Score)
      .reduce((prev, curr) => prev + curr, 0);
      res += max;
    }
    return res;
  }

  deleteQuestion(index: number) {
    this.test.Questions.splice(index, 1);
    this.areThereModifies = true;
  }

  edit(obj) {
    this.test.Questions[obj.index].Text = obj.question.questionText;
    this.test.Questions[obj.index].Answers = obj.question.answers;
    this.areThereModifies = true;
    this.isEditable[obj.index] = false;
  }

  goBack() {
    this.router.navigateByUrl('testList');
  }

  undo() {
    console.log(this.tempTest);
    this.test = this.tempTest;
    this.areThereModifies = false;
  }

  saveChanges(test: Test) {
    this.service.updateTest(test).subscribe(data => {this.tempTest = JSON.parse(JSON.stringify(this.test));
                                                     this.areThereModifies = false; },
      error => this.isThereAnError = true);
  }

  addQuestion(question: Question) {
      question.Position = this.test.Questions.length;
      this.test.Questions.push(question);
      this.saveChanges(this.test);
      this.isButtonVisible = false;
  }

  copyText(test: Test) {
     let s = '';
     let Index = 1;
     for (const question of test.Questions) {
        s += '\n';
        s += Index + '. ' + question.Text;
        s += '\n';
        Index++;
        for (const answer of question.Answers) {
           s += '□ ' + answer.Text;
           if (answer.IsCorrect) {
            s += ' V';
           }
           s += '\n';
      }
     }
     const selBox = document.createElement('textarea');
     selBox.value = 'Title: ' + test.Title + '\n' + 'Date of creation: ' + test.Date + '\n' + s;
     document.body.appendChild(selBox);
     selBox.focus();
     selBox.select();
     document.execCommand('copy');
     document.body.removeChild(selBox);
  }
}

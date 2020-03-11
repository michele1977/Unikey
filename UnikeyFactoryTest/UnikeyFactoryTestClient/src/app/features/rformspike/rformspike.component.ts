import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, FormArray, Form } from '@angular/forms';
import { Question } from 'src/app/models/question';
import { Answer } from 'src/app/models/answer';

@Component({
  selector: 'app-rformspike',
  templateUrl: './rformspike.component.html',
  styleUrls: ['./rformspike.component.css']
})
export class RformspikeComponent implements OnInit {
  answer: Answer = {Id:1, Text: "DIOMERDA", Score: 1}
question: Question = {Id: 1, Text: 'Sed quondam porco dio e la madonna?', Answers: [this.answer]};

  constructor(private fb: FormBuilder) { }
  orderForm: FormGroup;

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      questionText: '',
      answers: this.fb.array([
        this.fb.group(this.answer)
      ])
    });

    this.orderForm.controls.questionText.setValue(this.question.Text);

    // for(const answer of this.question.Answers) {
    //   this.answers.push(this.fb.group({
    //     Id: this.fb.control(answer.Id),
    //     Text: this.fb.control(answer.Text),
    //     Score: this.fb.control(answer.Score)}));
    // }
  }

  get answers(): FormArray {
    return this.orderForm.get('answers') as FormArray;
     }

  addEmptyAnswer() {
    this.answers.push(this.fb.group(new Answer()));

    console.log(this.answers);
  }



  // profileForm = new FormGroup({
  //   firstName: new FormControl(''),
  //   lastName: new FormControl(''),
  //   address: new FormGroup({
  //     street: new FormControl(''),
  //     city: new FormControl(''),
  //     state: new FormControl(''),
  //     zip: new FormControl('')
  // }) });

  // profileForm = this.fb.group({
  //   Text: [''],
  //   Answers: this.fb.group({
  //     Answer: this.fb.group({
  //       Text: this.fb.control(''),
  //     Score: this.fb.control('')
  //     })
  //   }),
  //   aliases: this.fb.array([
  //     this.fb.control('')
  //   ])
  // });

  // get aliases() {
  //   return this.profileForm.get('aliases') as FormArray;
  // }

  // addAlias() {
  //   this.aliases.push(this.fb.control(''));
  // }

  // Per rimpiazzare completamente il valore di un formcontrol da chiamata API
  /*
  updateName() {
  this.name.setValue('Nancy');
}
*/

// Per fare la patch di alcuni valori di un formgroup
// updateProfile() {
//   this.profileForm.patchValue({
//     firstName: 'Nancy',
//     address: {
//       street: '123 Drew Street'
//     }
//   });
// }

// onSubmit() {
//   // TODO: Use EventEmitter with form value
//   console.warn(this.profileForm.value);
// }



  }

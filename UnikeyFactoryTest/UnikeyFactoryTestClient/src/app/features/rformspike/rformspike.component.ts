import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Test } from 'src/app/models/test';

@Component({
  selector: 'app-rformspike',
  templateUrl: './rformspike.component.html',
  styleUrls: ['./rformspike.component.css']
})
export class RformspikeComponent {

  constructor(private fb: FormBuilder) { }

  // profileForm = new FormGroup({
  //   firstName: new FormControl(''),
  //   lastName: new FormControl(''),
  //   address: new FormGroup({
  //     street: new FormControl(''),
  //     city: new FormControl(''),
  //     state: new FormControl(''),
  //     zip: new FormControl('')
  // }) });

  profileForm = this.fb.group({
    Text: [''],
    Answers: this.fb.group({
      Answer: this.fb.group({
        Text: this.fb.control(''),
      Score: this.fb.control('')
      })
    }),
    aliases: this.fb.array([
      this.fb.control('')
    ])
  });

  get aliases() {
    return this.profileForm.get('aliases') as FormArray;
  }

  addAlias() {
    this.aliases.push(this.fb.control(''));
  }

  //Per rimpiazzare completamente il valore di un formcontrol da chiamata API
  /*
  updateName() {
  this.name.setValue('Nancy');
}
*/

// Per fare la patch di alcuni valori di un formgroup
updateProfile() {
  this.profileForm.patchValue({
    firstName: 'Nancy',
    address: {
      street: '123 Drew Street'
    }
  });
} 

onSubmit() {
  // TODO: Use EventEmitter with form value
  console.warn(this.profileForm.value);
}



  }

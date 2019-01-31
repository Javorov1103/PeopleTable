import { Component, OnInit} from '@angular/core';
import { PersonService } from 'src/app/_services/person.service';
import { MatDialog} from '@angular/material';
import { NgxNotificationService } from 'ngx-notification';
import { Country } from 'src/app/_models/Country';
import { FormBuilder, FormControl, FormGroup, FormArray } from '@angular/forms';

@Component({
  selector: 'app-person-create',
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit {
  model: any = {};
  countries: Country[];
  myForm: FormGroup;
  countryFromArray: any;

  getCircularReplacer = () => {
    const seen = new WeakSet();
    return (key, value) => {
      if (typeof value === 'object' && value !== null) {
        if (seen.has(value)) {
          return;
        }
        seen.add(value);
      }
      return value;
    };
  }

  constructor(private personService: PersonService,
    private dialog: MatDialog,
    private ngxNotificationService: NgxNotificationService,
    private formBuilder: FormBuilder) {

    }

  ngOnInit() {
    this.personService.getCountries().subscribe(data => this.countries = data);

    // Create our checklist form
    this.myForm = this.formBuilder.group({
        country: this.formBuilder.array([])
    });
  }

// Using the PersonService we send a post request to the server to create a new Person obj
  create() {
    // We use this check because it is not required a person to check any country
    if (this.countryFromArray !== undefined) {
      this.model.countries = JSON.stringify(this.countryFromArray.value, this.getCircularReplacer());
    }

    this.personService.createPerson(this.model).subscribe( () => {
      this.ngxNotificationService.sendMessage('Successfully added new person', 'success', 'bottom-right');
      this.dialog.closeAll();
    }, error => {
      this.ngxNotificationService.sendMessage(error, 'warning', 'bottom-right');
    });
  }

  // On change in a checkbox we call this method that add or remove visited country
  onChange(country: string, isChecked: boolean) {
    this.countryFromArray = <FormArray>this.myForm.controls.country;

    if (isChecked) {
      this.countryFromArray.push(new FormControl(country));
    } else {
      const index = this.countryFromArray.controls.findIndex(x => x.value === country);
      this.countryFromArray.removeAt(index);
    }
  }



}

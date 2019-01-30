import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { PersonService } from 'src/app/_services/person.service';
import { NgxNotificationService } from 'ngx-notification';
import { Country } from 'src/app/_models/Country';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {
  countries: Country[];
  myForm: FormGroup;
  countryFromArray: any;
  @Output() cancelEdit = new EventEmitter();

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
    @Inject(MAT_DIALOG_DATA) public data: any,
  private dialog: MatDialog,
  private ngxNotificationService: NgxNotificationService,
  private formBuilder: FormBuilder) { }


  ngOnInit() {
    this.personService.getCountries().subscribe(data => this.countries = data);

    // Create our checklist form
    this.myForm = this.formBuilder.group({
      country: this.formBuilder.array([])
  });
  }

  // Using the PersonService we send a put request to the server to update a Person obj in the db
  edit(): void {
    this.personService.editPerson(this.data.id, this.data).subscribe(() => {
      this.ngxNotificationService.sendMessage('Person successfully updated', 'success', 'bottom-right');
      this.dialog.closeAll();
    }, error => {
      this.ngxNotificationService.sendMessage(error, 'warning', 'bottom-right');
    });
  }

  onChange(country: string, isChecked: boolean) {
    if (isChecked) {
      if (this.data.countries === null) {
        this.data.countries = Array<string>();
      }
      this.data.countries.push(country);
    } else {
      const index = this.data.countries.indexOf(country);
      this.data.countries.splice(index, 1);
    }
    console.log(this.data.countries);
  }

  isCountryVisited(country: string): boolean {
    if (this.data.countries === null) {
      return false;
    }
    return this.data.countries.indexOf(country) >= 0;
  }

}

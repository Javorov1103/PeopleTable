import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { PersonService } from 'src/app/_services/person.service';
import { MatDialog} from '@angular/material';

@Component({
  selector: 'app-person-create',
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit {
  model: any = {};

  constructor(private personService: PersonService, private dialog: MatDialog) { }

  ngOnInit() {
  }

// Method for sending data for needed for creating Peson object in the db
  create() {
    this.personService.createPerson(this.model).subscribe( () => {
      console.log('creation succsesfull');
      this.dialog.closeAll();
    }, error => {
      console.log(error);
    });
  }

}

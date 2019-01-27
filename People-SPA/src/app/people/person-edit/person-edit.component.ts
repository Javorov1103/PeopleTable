import { Component, OnInit, Output, EventEmitter, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatTable } from '@angular/material';
import { PersonService } from 'src/app/_services/person.service';
import { Person } from 'src/app/_models/Person';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {
  @Output() cancelEdit = new EventEmitter();

  constructor(private personService: PersonService, @Inject(MAT_DIALOG_DATA) public data: any,
  private dialog: MatDialog) { }


  ngOnInit() {
  }

  // Method for editing a Person Object
  edit(): void {
    this.personService.editPerson(this.data.id, this.data).subscribe(() => {
      console.log('edit succsesfull');
      this.dialog.closeAll();
    }, error => {
      console.log(error);
    });
  }

}

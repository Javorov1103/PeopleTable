import { Component, OnInit, Inject } from '@angular/core';
import { PersonService } from 'src/app/_services/person.service';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';

@Component({
  selector: 'app-person-delete',
  templateUrl: './person-delete.component.html',
  styleUrls: ['./person-delete.component.css']
})
export class PersonDeleteComponent implements OnInit {

  constructor(private personService: PersonService, @Inject(MAT_DIALOG_DATA) public data: any, private dialog: MatDialog) { }

  ngOnInit() {
  }

  delete() {
    this.personService.deletePerson(this.data.id).subscribe(() => {
      console.log('edit succsesfull');
      this.dialog.closeAll();
      window.location.reload();
    }, error => {
      console.log(error);
    });
  }

}

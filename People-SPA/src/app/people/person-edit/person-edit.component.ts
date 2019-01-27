import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { PersonService } from 'src/app/_services/person.service';
import { NgxNotificationService } from 'ngx-notification';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {
  @Output() cancelEdit = new EventEmitter();

  constructor(private personService: PersonService,
    @Inject(MAT_DIALOG_DATA) public data: any,
  private dialog: MatDialog,
  private ngxNotificationService: NgxNotificationService) { }


  ngOnInit() {
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

}

import { Component, OnInit, Inject } from '@angular/core';
import { PersonService } from 'src/app/_services/person.service';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { NgxNotificationService } from 'ngx-notification';

@Component({
  selector: 'app-person-delete',
  templateUrl: './person-delete.component.html',
  styleUrls: ['./person-delete.component.css']
})
export class PersonDeleteComponent implements OnInit {

  constructor(private personService: PersonService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialog,
    private ngxNotificationService: NgxNotificationService) { }

  ngOnInit() {
  }

  // Using the PersonService we send a delete request to the server
  delete() {
      this.personService.deletePerson(this.data.id).subscribe(() => {
      this.ngxNotificationService.sendMessage('Person successfully deleted', 'success', 'bottom-right');
      this.dialog.closeAll();
    }, error => {
      this.ngxNotificationService.sendMessage(error, 'warning', 'bottom-right');
    });
  }

}

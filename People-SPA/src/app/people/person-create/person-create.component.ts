import { Component, OnInit} from '@angular/core';
import { PersonService } from 'src/app/_services/person.service';
import { MatDialog} from '@angular/material';
import { NgxNotificationService } from 'ngx-notification';

@Component({
  selector: 'app-person-create',
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit {
  model: any = {};

  constructor(private personService: PersonService,
    private dialog: MatDialog,
    private ngxNotificationService: NgxNotificationService) { }

  ngOnInit() {
  }

// Using the PersonService we send a post request to the server to create a new Person obj
  create() {
    this.personService.createPerson(this.model).subscribe( () => {
      this.ngxNotificationService.sendMessage('Successfully added new person', 'success', 'bottom-right');
      this.dialog.closeAll();
    }, error => {
      this.ngxNotificationService.sendMessage(error, 'warning', 'bottom-right');
    });
  }

}

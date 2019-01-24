import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { PersonService } from 'src/app/_services/person.service';

@Component({
  selector: 'app-person-create',
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit {
  @Output() cancelCreate = new EventEmitter();
  model: any = {};

  constructor(private personService: PersonService) { }

  ngOnInit() {
  }

  create() {
    this.personService.createPerson(this.model).subscribe( () => {
      console.log('creation succsesfull');
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelCreate.emit(false);
  }

}

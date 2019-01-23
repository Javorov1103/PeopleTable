import { Component, OnInit, ViewChild } from '@angular/core';
import { Person } from 'src/app/_models/Person';
import { PersonService } from 'src/app/_services/person.service';
import { MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-people-table',
  templateUrl: './people-table.component.html',
  styleUrls: ['./people-table.component.css']
})
export class PeopleTableComponent implements OnInit {

  createMode = false;

  // This will be our data table
  people: Person[];

  constructor(private personService: PersonService) { }

  // Setting the colums of the table
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'egn', 'weight', 'height'];

  // Declare our table and the type of object that we will use
  dataSource: MatTableDataSource<Person>;

  // Sets to use default sort parameters
  @ViewChild(MatSort) sort: MatSort;

  // Use the personService to get from the API all the people, set them to the dataSource and set the sorting.
  ngOnInit() {
    this.personService.getPeople().subscribe(res => {
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.sort = this.sort;
    });
  }

  cancelCreateMode(createMode: boolean) {
    this.createMode = createMode;
  }

  registerToggle() {
    this.createMode = true;
  }

}

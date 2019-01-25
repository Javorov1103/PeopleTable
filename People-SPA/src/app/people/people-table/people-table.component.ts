import { Component, OnInit, ViewChild } from '@angular/core';
import { Person } from 'src/app/_models/Person';
import { PersonService } from 'src/app/_services/person.service';
import { MatSort, MatTableDataSource, MatPaginator, MatDialog } from '@angular/material';
import { PersonCreateComponent } from '../person-create/person-create.component';
import { PersonEditComponent } from '../person-edit/person-edit.component';

@Component({
  selector: 'app-people-table',
  templateUrl: './people-table.component.html',
  styleUrls: ['./people-table.component.css']
})
export class PeopleTableComponent implements OnInit {

  // createMode = false;
  editMode = false;

  // This will be our data table
  people: Person[];

  constructor(private personService: PersonService, private dialog: MatDialog) { }

  // Setting the colums of the table
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'egn', 'weight', 'height', 'actions'];

  // Declare our table and the type of object that we will use
  dataSource: MatTableDataSource<Person>;

  // Sets to use default sort parameters
  @ViewChild(MatSort) sort: MatSort;
  // Sets to use default pagination
  @ViewChild(MatPaginator) paginator: MatPaginator;

  // Use the personService to get from the API all the people, set them to the dataSource and set the sorting.
  ngOnInit() {
    this.personService.getPeople().subscribe(res => {
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  addNew() {
    this.dialog.open(PersonCreateComponent);
  }

  editPerson(id: number, firstName: string, lastName: string, egn: string, weight: number, height: number) {
    this.dialog.open(PersonEditComponent, {
      data: {id: id, firstName: firstName, lastName: lastName, egn: egn, weight: weight, height: height}
    });
  }

  private refresh() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }

}

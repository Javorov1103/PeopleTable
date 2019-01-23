import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatSortModule, MatTableModule} from '@angular/material';

import { AppComponent } from './app.component';
import { PersonService } from './_services/person.service';
import { PeopleTableComponent } from './people/people-table/people-table.component';
import { PersonCreateComponent } from './people/person-create/person-create.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    PeopleTableComponent,
    PersonCreateComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatButtonModule,
    MatCheckboxModule,
    MatTableModule,
    FormsModule
  ],
  providers: [
   PersonService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

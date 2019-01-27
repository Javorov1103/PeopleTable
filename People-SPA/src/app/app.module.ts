import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatSortModule, MatToolbarModule,
  MatTableModule, MatPaginatorModule, MatIconModule, MatDialogModule} from '@angular/material';

import { AppComponent } from './app.component';
import { PersonService } from './_services/person.service';
import { PeopleTableComponent } from './people/people-table/people-table.component';
import { PersonCreateComponent } from './people/person-create/person-create.component';
import { FormsModule, FormBuilder } from '@angular/forms';
import { appRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { TwoDigitDecimaNumberDirective } from './_helpers/two-digit-decimal.directive';
import { PersonEditComponent } from './people/person-edit/person-edit.component';
import { PersonDeleteComponent } from './people/person-delete/person-delete.component';
import { NgxNotificationComponent } from 'ngx-notification';

@NgModule({
  declarations: [
    AppComponent,
    PeopleTableComponent,
    PersonCreateComponent,
    TwoDigitDecimaNumberDirective,
    PersonEditComponent,
    PersonDeleteComponent,
    NgxNotificationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatSortModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatTableModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    MatPaginatorModule,
    MatToolbarModule
  ],
  providers: [
   PersonService,
   FormBuilder
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    PersonEditComponent,
    PersonDeleteComponent
  ]
})
export class AppModule { }

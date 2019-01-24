import {Routes} from '@angular/router';
import { PeopleTableComponent } from './people/people-table/people-table.component';
import { PersonCreateComponent } from './people/person-create/person-create.component';


export const appRoutes: Routes = [
    {path: '', component: PeopleTableComponent},
    {path: 'create', component: PersonCreateComponent}
];

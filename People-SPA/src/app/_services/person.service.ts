import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from '../_models/Person';
import { Country } from '../_models/Country';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

constructor(private http: HttpClient) { }


// Get a collection of Person
getPeople(): Observable<Person[]> {
  return this.http.get<Person[]>('http://localhost:5000/api/person');
}

// Crete a Person Object and save it in the DB
createPerson(model: any) {
  return this.http.post('http://localhost:5000/api/person/create', model);
}


// Edit the concrete PersonObject
editPerson(id: number, person: Person) {
  return  this.http.put('http://localhost:5000/api/person/' + id, person);
}

// Delete the concrete PersonObject
deletePerson(id: number) {
  return this.http.delete('http://localhost:5000/api/person/' + id);
}

// Get a collection of all Countries
getCountries(): Observable<Country[]> {
  return this.http.get<Country[]>('http://localhost:5000/api/country');
}

}

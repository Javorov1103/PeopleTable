import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from '../_models/Person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

constructor(private http: HttpClient) { }

// Connect to the API via HttpClient and get a collection of Person
getPeople(): Observable<Person[]> {
  return this.http.get<Person[]>('http://localhost:5000/api/person');
}

createPerson(model: any) {
  return this.http.post('http://localhost:5000/api/person/create', model);
}

}

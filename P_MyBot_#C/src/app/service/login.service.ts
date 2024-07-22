import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Volunteer } from '../classes/Volunteer';
@Injectable({
  providedIn: 'root',
})
export class LoginService {
  baseUrl = 'https://localhost:44375/';
  VolunteerUrl = 'api/volunteers/';
  constructor(public http: HttpClient) {}

  login(id: number, password: string) {
    return this.http.get<Volunteer>(
      this.baseUrl + this.VolunteerUrl + `login?id=${id}&password=${password}`
    );
  }
  signUp(volunteer: Volunteer) {
    return this.http.post<boolean>(
      this.baseUrl + this.VolunteerUrl + `AddVolunteers`,
      volunteer
    );
  }
}

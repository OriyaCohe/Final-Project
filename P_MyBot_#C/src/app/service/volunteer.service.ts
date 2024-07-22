import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class VolunteerService {
  constructor(public http: HttpClient) {}
  UpDateLocationVolunteer() {
    return this.http.get<boolean>(
      `https://localhost:44375/api/SearchVolunteer/UpDateLocation`
    );
  }
  OptimalVolunteerList(address: string, problem: string, idR: number) {
    return this.http.get<string>(
      `https://localhost:44375/api/Volunteer/OptimalVolunteerList?addressClient=${address}&problem=${problem}&idR=${idR}`
    );
  }

  UpStatusNo(id: number) {
    return this.http.get<boolean>(
      `https://localhost:44375/api/volunteers/UpStatusNo?id=${id}`
    );
  }
  UpStatusOk(id: number) {
    return this.http.get<boolean>(
      `https://localhost:44375/api/volunteers/UpStatusOk?id=${id}`
    );
  }
  ConfirmationOfReferral(id: number) {
    return this.http.get<string>(
      `https://localhost:44375/api/volunteers/ConfirmationOfReferral?id=${id}`
    );
  }
}

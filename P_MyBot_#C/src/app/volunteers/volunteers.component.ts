import { Component } from '@angular/core';
import { Volunteer } from '../classes/Volunteer';
import { VolunteerService } from '../service/volunteer.service';
@Component({
  selector: 'app-volunteers',
  templateUrl: './volunteers.component.html',
  styleUrl: './volunteers.component.css',
})
export class VolunteersComponent {
  constructor(public volunteerService: VolunteerService) {}
  volunterr: Volunteer = JSON.parse(localStorage.getItem('Volunteer')!);
  Ok() {
    this.volunteerService.UpStatusOk(this.volunterr.Id!).subscribe((res) => {
      if (res) alert('אישורך התקבך תודה ולהראות');
    });
    //לשנות את הסטטוס לחיובי
  }
  end() {
    console.log(this.volunterr + 'pppppp');
    //שלילי
    this.volunteerService.UpStatusNo(this.volunterr.Id!).subscribe((res) => {
      if (res) alert('תודה ולהראות');
    });
  }

  OkR(Referral:number) {
    this.volunteerService.ConfirmationOfReferral(Referral).subscribe((res) => {
      if (res) alert('תודה רבה תזכה למצוות');
      else alert('מספר פניה שגוי');
    });
  }
}

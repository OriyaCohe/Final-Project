import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { VolunteerService } from './service/volunteer.service';
import { MediaMatcher } from '@angular/cdk/layout';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(
    /*public media: MediaMatcher, */ public VolunteerS: VolunteerService
  ) {
    //this.mobileQuery = media.matchMedia('(max-width: 800px)');
  }
  ngOnInit(): void {
    this.startTimer();
  }

  title = 'ChatBot';
  //mobileQuery: MediaQueryList;
  startTimer() {
    //  הפונקציה שמעדכנת מיקומים למתנדבים שמופעלת כל חצי שעה
    setInterval(() => {
      this.VolunteerS.UpDateLocationVolunteer().subscribe((res) => {
        console.log(res);
      });
    }, 1000 * 60 * 30); // משך זמן במילישניות (כאן 1000 מילישניות = 1 שנייה)
  }
}

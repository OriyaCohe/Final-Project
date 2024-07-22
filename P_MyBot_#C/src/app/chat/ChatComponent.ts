import {
  Component,
  EventEmitter,
  Output,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MessageService } from '../service/message.service';
import { LocationService } from '../service/location.service';
import { VolunteerService } from '../service/volunteer.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
})
export class ChatComponent implements OnInit {
  latitude: number | null = null;
  longitude: number | null = null;
  error: string | null = null;
  @Output() cancelEvent = new EventEmitter();

  // myPlace1 = '';
  myPlace = '';
  status = 0;
  flag = false;
  i = 0;
  tArr: Array<string> = [];
  indexArr: Array<number> = [];
  phoneOfVolunteers: Array<string> = [];
  isUser: boolean = false;
  text = true;
  constructor(
    public messageS: MessageService,
    public VolunteerS: VolunteerService,
    private locationService: LocationService
  ) {
    this.tArr.push('?שלום רב איך אני יכול לעזור');
  }
  cancel() {
    this.cancelEvent.emit();
  }
  idR = 0;
  problem = '';
  send(text: string) {
    if (text != ' ') {
      this.tArr.push(text);
      if (this.status == 0) {
        this.flag = true;
        this.messageS.newRequest().subscribe((res) => {
          this.idR = res;
          this.messageS.SendMessage(text).subscribe((res) => {
            if (res != 'הודעה לא הובנה נסה שנית' && res != '?מה הסיבה') {
              this.problem = res.substring(17);
              if (this.problem[0] == 'ש' && this.problem[1] == 'א')
                this.problem = this.problem.substring(5);
              this.problem = this.problem.substring(
                0,
                this.problem.length - 12
              );
            }
            this.tArr.push(res);
            this.status++;
            this.flag = false;
          });
        });
      } else
        this.messageS.SendMessageContinued(text).subscribe((res) => {
          this.tArr.push(res);
          if (res.startsWith('זהיתי')) {
            this.problem = res.substring(17);
            if (this.problem[0] == 'ש' && this.problem[1] == 'א')
              this.problem = this.problem.substring(5);
            this.problem = this.problem.substring(0, this.problem.length - 12);
          }
          if (res == 'יופי תודה אני מחפש משהו שיבוא לעזור לך') {
            //alert(this.myPlace);
            //להעלים את האופציה של המשך שיח
            this.text = false;
            this.VolunteerS.OptimalVolunteerList(
              this.myPlace,
              this.problem,
              this.idR
            ).subscribe((res) => {
              // this.SendMessage(res);
              alert(res);
            });
            this.SearchPlace();
          }
        });
    }
  }

  SendMessage(arr: Array<string>) {}

  ngOnInit(): void {
    this.SearchPlace();
  }
  getAddress(latitude: number, longitude: number): void {
    this.locationService.getAddress(latitude, longitude).subscribe(
      (response) => {
        if (response.status === 'OK' && response.results.length > 0) {
          this.myPlace = response.results[0].formatted_address;

          // this.SerchVolunteer();
        } else {
          this.error = 'No address found';
        }
      },
      (error) => {
        console.error('Error obtaining address:', error);
        this.error = 'Error obtaining address';
      }
    );
  }
  SearchPlace(): void {
    console.log('Trying to get location...');
    this.locationService
      .getCurrentLocation()
      .then((position) => {
        console.log('Position obtained:', position);
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.getAddress(this.latitude, this.longitude);
      })
      .catch((error) => {
        console.error('Error obtaining position:', error);
        this.error = error.message;
      });
  }
}

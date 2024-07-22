import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { LoginService } from '../service/login.service';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { Volunteer } from '../classes/Volunteer';
import { Observable } from 'rxjs';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-sign-up',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent implements OnInit {
  constructor(private loginService: LoginService, private router: Router) {}
  volunteer: Volunteer = {};
  idErr: boolean = false;
  passErr: boolean = false;
  pErr: boolean = false;
  nErr: boolean = false;
  ok!: boolean;
  ngOnInit() {}
  check(id: string, err: boolean) {
    for (let i = 0; i < id.length; i++) {
      if (id[i] < '0' || id[i] > '9') {
        err = true;
        return false;
      }
    }
    return true;
  }
  checkLen(s: string, maxLen: number, err: boolean) {
    if (s.length < maxLen) err = true;
  }
  signUp(id: any, name: string, phon: string, password: string) {
    //   let e = this.check(id,this.idErr);
    //   if (id.length != 9) this.idErr = true;
    // let p = this.check(phon,this.pErr);
    //    if (phon.length != 10) this.pErr = true;
    //    if (name.length <2) this.nErr = true;
    //     if (password.length < 4) this.passErr = true;
    if (
      (this.idErr =
        this.nErr =
        this.pErr =
          this.passErr == false &&
          id != '' &&
          phon != '' &&
          name != '' &&
          password != '')
    ) {
      this.volunteer.Name = name;
      this.volunteer.Password = password;
      this.volunteer.phone = phon;
      this.volunteer.Id = id;
      this.loginService.signUp(this.volunteer).subscribe((ans) => {
        console.log({ ans });
        if (ans) {
          alert('נוסף בהצלחה');
          this.router.navigate(['login']);
        } else alert('ישנה תקלה נסה מאוחר יותר');
      });
    } else {
      alert(' נסה שוב! ישנם נתונים שגויים');
      this.idErr = this.nErr = this.pErr = this.passErr = false;
    }
  }
}

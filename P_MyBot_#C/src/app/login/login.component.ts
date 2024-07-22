import { Component, OnInit } from '@angular/core';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { LoginService } from '../service/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(private loginService: LoginService, private router: Router) {}

  ngOnInit() {}
  idErr: boolean = false;
  passErr: boolean = false;
  checkId(id: string) {
    for (let i = 0; i < id.length; i++) {
      if (id[i] < '0' || id[i] > '9') {
        this.idErr = true;
        return false;
      }
    }
    return true;
  }
  login(id: string, pass: string) {
    let e = this.checkId(id);
    if (id.length != 9) this.idErr = true;
    if (pass.length < 3) this.passErr = true;
    if (id.length == 9 && e && pass.length >3) {
      this.passErr = false;
      this.idErr = false;
      this.loginService.login(parseInt(id), pass).subscribe(
        (data) => {
          console.log(data);

          if (data.Id != 0) {
            localStorage.setItem('Volunteer', JSON.stringify(data));
            this.router.navigate(['Volunteer']);
          } else {
            alert('לא קיים');
          }
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
}

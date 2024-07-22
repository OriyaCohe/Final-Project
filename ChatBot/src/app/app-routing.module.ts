import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatComponent } from './chat/ChatComponent';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { HomeComponent } from './home/home.component';
import { VolunteersComponent } from './volunteers/volunteers.component';
import { NgbCarousel, NgbModule } from '@ng-bootstrap/ng-bootstrap';

const routes: Routes = [
  { path: 'chat', component: ChatComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signUp', component: SignUpComponent },
  { path: 'Volunteer', component: VolunteersComponent },
  { path: '', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes),NgbModule],
  exports: [RouterModule],
  //schemas:[NgbCarousel,]
})
export class AppRoutingModule {}

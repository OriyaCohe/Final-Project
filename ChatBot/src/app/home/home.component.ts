import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { MediaMatcher } from '@angular/cdk/layout';
 
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  aaa = false;
  aas = false;
  aa3 = false;
  mobileQuery: MediaQueryList;

  constructor(public media: MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 800px)');
  }
  ngOnInit(): void {}
  
}

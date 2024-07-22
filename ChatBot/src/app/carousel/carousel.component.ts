import { Component, Input, OnInit } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';

interface carouselImage {
  imageSrc: string;
  imageAlt: string;
}
@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.scss',
  animations: [
    trigger('carousalAnimation', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)' }),
        animate(
          '300ms ease-in',
          style({
            transform: 'translateX(0%)',
          })
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({
            transform: 'translateX(-100%)',
          })
        ),
      ]),
    ]),
  ],
})
export class CarouselComponent implements OnInit {
  images: carouselImage[] = [
    {
      imageSrc: '../../assets/img/enter1.png',
      imageAlt: 'nature1',
    },
    {
      imageSrc: '../../assets/img/enter2.png',
      imageAlt: 'nature2',
    },
    {
      imageSrc: '../../assets/img/enter3.png',
      imageAlt: 'person1',
    },
    {
      imageSrc: '../../assets/img/enter4.png',
      imageAlt: 'person2',
    },
  ];
  @Input() indicators = true;
  selectedIndex = 0;
  constructor() {}

  ngOnInit(): void {
    this.autoSlideImages();
  }

  selectImage(index: number): void {
    this.selectedIndex = index;
    
  }
  autoSlideImages() {
    if(this.selectedIndex < this.images.length - 1)
      this.selectedIndex++;
    else
    this.selectedIndex = 0;
  }
}

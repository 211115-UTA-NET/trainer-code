import { Component, Input, OnInit } from '@angular/core';
import { MyhttpService } from '../myhttp.service';

@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnInit {
  @Input() data!: string;

  constructor(private service: MyhttpService) { }

  ngOnInit(): void {
  }

}

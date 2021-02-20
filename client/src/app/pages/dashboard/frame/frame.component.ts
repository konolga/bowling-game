import { Component, OnInit, Input } from '@angular/core';
import { FrameData } from '@models/frameData';

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.css']
})
export class FrameComponent implements OnInit {

  @Input() frameData: FrameData;

  constructor() { }

  ngOnInit(): void {
  }

}

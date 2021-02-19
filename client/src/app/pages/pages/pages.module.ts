import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InitScreenComponent } from './init-screen/init-screen.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TopScoresComponent } from './top-scores/top-scores.component';



@NgModule({
  declarations: [InitScreenComponent, DashboardComponent, TopScoresComponent],
  imports: [
    CommonModule
  ]
})
export class PagesModule { }

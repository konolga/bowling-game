import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from '@pages/home/home.component';
import { DashboardComponent } from '@pages/dashboard/dashboard.component';
import { TopScoresComponent } from '@pages/top-scores/top-scores.component';

import { ReactiveFormsModule } from '@angular/forms';
import { FrameComponent } from '@pages/dashboard/frame/frame.component';

@NgModule({
  declarations: [
    HomeComponent,
    DashboardComponent,
    TopScoresComponent,
    FrameComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class PagesModule { }

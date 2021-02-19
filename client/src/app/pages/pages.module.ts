import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from '@pages/home/home.component';
import { DashboardComponent } from '@pages/dashboard/dashboard.component';
import { TopScoresComponent } from '@pages/top-scores/top-scores.component';

import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HomeComponent,
    DashboardComponent,
    TopScoresComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class PagesModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '@services/auth.service';
import { GameService } from '@services/game.service';

@NgModule({
  declarations: [],
  imports: [

  CommonModule
  ],
  providers: [
    AuthService,
    GameService
  ]
})
export class CoreModule { }

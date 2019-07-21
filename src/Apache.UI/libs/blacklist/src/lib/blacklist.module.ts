import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlacklistComponent } from './containers/blacklist/blacklist.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        pathMatch: 'full',
        component: BlacklistComponent
      }
    ])
  ],
  declarations: [BlacklistComponent]
})
export class BlacklistModule {}

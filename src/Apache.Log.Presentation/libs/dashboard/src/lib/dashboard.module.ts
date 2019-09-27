import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OverviewComponent } from './containers/overview/overview.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        redirectTo: 'overview'
      },
      {
        path: 'overview',
        component: OverviewComponent
      }
    ])
  ],
  declarations: [OverviewComponent]
})
export class DashboardModule {}

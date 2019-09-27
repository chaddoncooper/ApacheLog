import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OverviewComponent } from './containers/overview/overview.component';
import { RouterModule } from '@angular/router';
import { OverviewService } from './services/overview.service';

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
  declarations: [OverviewComponent],
  providers: [OverviewService]
})
export class DashboardModule {}

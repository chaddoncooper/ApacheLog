import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OverviewComponent } from './containers/overview/overview.component';
import { RouterModule } from '@angular/router';
import { OverviewService } from './services/overview.service';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzCardModule } from 'ng-zorro-antd/card';

@NgModule({
  imports: [
    CommonModule,
    NzCardModule,
    NzStatisticModule,
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

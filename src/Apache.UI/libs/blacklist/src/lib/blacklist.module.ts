import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlacklistComponent } from './containers/blacklist/blacklist.component';
import { RouterModule } from '@angular/router';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { BlacklistedResourceService } from './services/blacklisted-resource.service';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    RouterModule.forChild([
      {
        path: '',
        pathMatch: 'full',
        component: BlacklistComponent
      }
    ])
  ],
  declarations: [BlacklistComponent],
  providers: [BlacklistedResourceService]
})
export class BlacklistModule {}

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  NgZorroAntdModule,
  NzButtonModule,
  NzInputModule
} from 'ng-zorro-antd';
import { BlacklistComponent } from './containers/blacklist/blacklist.component';
import { BlacklistedResourceService } from './services/blacklisted-resource.service';

import { WhitelistComponent } from './containers/whitelist/whitelist.component';
import { WhitelistedResourceService } from './services/whitelisted-resource.service';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgZorroAntdModule,
    NzInputModule,
    NzButtonModule,
    RouterModule.forChild([
      {
        path: '',
        redirectTo: 'blacklist'
      },
      {
        path: 'blacklist',
        component: BlacklistComponent
      },
      {
        path: 'whitelist',
        component: WhitelistComponent
      }
    ])
  ],
  declarations: [BlacklistComponent, WhitelistComponent],
  providers: [BlacklistedResourceService, WhitelistedResourceService]
})
export class ResourcesModule {}

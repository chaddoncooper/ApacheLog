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
        component: BlacklistComponent
      }
    ])
  ],
  declarations: [BlacklistComponent],
  providers: [BlacklistedResourceService]
})
export class BlacklistModule {}

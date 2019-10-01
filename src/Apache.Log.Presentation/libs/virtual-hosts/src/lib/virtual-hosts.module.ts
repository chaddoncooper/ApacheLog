import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ManageVirtualHostsComponent } from './containers/manage-virtual-hosts/manage-virtual-hosts.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        redirectTo: 'manage'
      },
      {
        path: 'manage',
        component: ManageVirtualHostsComponent
      }
    ])
  ],
  declarations: [ManageVirtualHostsComponent]
})
export class VirtualHostsModule {}

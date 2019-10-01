import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageVirtualHostsComponent } from './containers/manage-virtual-hosts/manage-virtual-hosts.component';

@NgModule({
  imports: [CommonModule],
  declarations: [ManageVirtualHostsComponent]
})
export class VirtualHostsModule {}

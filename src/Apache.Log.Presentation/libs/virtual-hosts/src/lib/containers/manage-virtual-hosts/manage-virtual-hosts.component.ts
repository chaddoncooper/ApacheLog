import { Component, OnInit } from '@angular/core';
import { VirtualHostsService } from '../../services/virtual-hosts.service';

@Component({
  selector: 'apache-log-manage-virtual-hosts',
  templateUrl: './manage-virtual-hosts.component.html',
  styleUrls: ['./manage-virtual-hosts.component.scss']
})
export class ManageVirtualHostsComponent implements OnInit {
  virtualHosts$ = this._virtualHostsService.virtualHosts$;
  value: string;

  constructor(private readonly _virtualHostsService: VirtualHostsService) {}

  ngOnInit() {}

  delete(id: number) {
    this._virtualHostsService.deleteVirtualHost(id);
  }
}

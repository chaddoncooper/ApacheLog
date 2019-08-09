import { Component, OnInit } from '@angular/core';
import { BlacklistedResourceService } from '../../services/blacklisted-resource.service';

@Component({
  selector: 'apache-blacklist',
  templateUrl: './blacklist.component.html',
  styleUrls: ['./blacklist.component.scss']
})
export class BlacklistComponent implements OnInit {
  blacklistedResources$ = this._blacklistedResourceService
    .blacklistedResources$;

  constructor(
    private readonly _blacklistedResourceService: BlacklistedResourceService
  ) {}

  ngOnInit() {}

  delete(id: number) {
    this._blacklistedResourceService.deleteBlacklistedResource(id);
  }
}

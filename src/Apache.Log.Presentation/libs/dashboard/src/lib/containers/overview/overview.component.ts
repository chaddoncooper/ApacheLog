import { Component, OnInit } from '@angular/core';
import { OverviewService } from '../../services/overview.service';

@Component({
  selector: 'apache-log-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {
  countBlacklistedResources = this._overviewService.countBlacklistedResources();

  constructor(private readonly _overviewService: OverviewService) {}

  ngOnInit() {}
}

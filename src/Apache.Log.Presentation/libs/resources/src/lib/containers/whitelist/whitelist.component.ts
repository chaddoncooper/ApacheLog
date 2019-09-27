import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { WhitelistedResourceService } from '../../services/whitelisted-resource.service';

@Component({
  selector: 'apache-log-whitelist',
  templateUrl: './whitelist.component.html',
  styleUrls: ['./whitelist.component.scss']
})
export class WhitelistComponent implements OnInit {
  whitelistedResources$ = this._whitelistedResourceService
    .whitelistedResources$;
  value: string;

  addWhitelistedResourceForm = new FormGroup({
    fullPath: new FormControl()
  });

  constructor(
    private readonly _whitelistedResourceService: WhitelistedResourceService
  ) {}

  ngOnInit() {}

  add() {
    this._whitelistedResourceService.addWhitelistedResource(
      this.addWhitelistedResourceForm.value.fullPath
    );
  }

  delete(id: number) {
    this._whitelistedResourceService.deleteWhitelistedResource(id);
  }
}

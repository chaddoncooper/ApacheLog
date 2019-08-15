import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BlacklistedResourceService } from '../../services/blacklisted-resource.service';

@Component({
  selector: 'apache-blacklist',
  templateUrl: './blacklist.component.html',
  styleUrls: ['./blacklist.component.scss']
})
export class BlacklistComponent implements OnInit {
  blacklistedResources$ = this._blacklistedResourceService
    .blacklistedResources$;
  value: string;

  addBlacklistedResourceForm = new FormGroup({
    fullPath: new FormControl()
  });

  constructor(
    private readonly _blacklistedResourceService: BlacklistedResourceService
  ) {}

  ngOnInit() {}

  add() {
    this._blacklistedResourceService.addBlacklistedResource(
      this.addBlacklistedResourceForm.value.fullPath
    );
  }

  delete(id: number) {
    this._blacklistedResourceService.deleteBlacklistedResource(id);
  }
}

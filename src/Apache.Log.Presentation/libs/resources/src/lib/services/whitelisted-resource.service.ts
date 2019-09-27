import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { WhitelistedResource } from '../models/whitelisted-resource.model';
import { APACHE_LOG_ENV } from '@apache-log/core';
@Injectable({
  providedIn: 'root'
})
export class WhitelistedResourceService {
  private _basePath = `${this._apacheLogEnv.baseUrl}api/WhitelistedResources`;

  private readonly refresh$ = new BehaviorSubject(undefined);
  whitelistedResources$ = this.refresh$.pipe(
    switchMap(() => this._httpClient.get<WhitelistedResource[]>(this._basePath))
  );

  constructor(
    @Inject(APACHE_LOG_ENV) private readonly _apacheLogEnv,
    private readonly _httpClient: HttpClient
  ) {}

  addWhitelistedResource(fullPath: string) {
    this._httpClient
      .post(`${this._basePath}`, { fullPath })
      .subscribe(
        _ => this.refresh$.next(undefined),
        error => console.log(error)
      );
  }

  deleteWhitelistedResource(id: number) {
    this._httpClient
      .delete(`${this._basePath}/${id}`)
      .subscribe(
        _ => this.refresh$.next(undefined),
        error => console.log(error)
      );
  }

  countWhitelistedResources() {
    this._httpClient.get(`${this._basePath}/totalcount`);
  }
}

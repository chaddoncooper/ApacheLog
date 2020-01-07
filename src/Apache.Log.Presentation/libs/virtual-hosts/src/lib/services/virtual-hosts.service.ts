import { Injectable, Inject } from '@angular/core';
import { APACHE_LOG_ENV } from '@apache-log/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { VirtualHost } from '../models/virtual-host.model';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VirtualHostsService {
  private _basePath = `${this._apacheLogEnv.baseUrl}api/virtualhosts`;

  private readonly refresh$ = new BehaviorSubject(undefined);
  virtualHosts$ = this.refresh$.pipe(
    switchMap(() => this._httpClient.get<VirtualHost[]>(this._basePath))
  );

  constructor(
    @Inject(APACHE_LOG_ENV) private readonly _apacheLogEnv,
    private readonly _httpClient: HttpClient
  ) {}

  deleteVirtualHost(id: number) {
    this._httpClient
      .delete(`${this._basePath}/${id}`)
      .subscribe(
        _ => this.refresh$.next(undefined),
        error => console.log(error)
      );
  }
}

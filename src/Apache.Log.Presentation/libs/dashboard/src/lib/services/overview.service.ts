import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { APACHE_LOG_ENV } from '@apache-log/core';
@Injectable({
  providedIn: 'root'
})
export class OverviewService {
  constructor(
    @Inject(APACHE_LOG_ENV) private readonly _apacheLogEnv,
    private readonly _httpClient: HttpClient
  ) {}

  countBlacklistedResources() {
    return this._httpClient.get(
      `${this._apacheLogEnv.baseUrl}/api/BlacklistedResources/totalcount`
    );
  }

  countWhitelistedResources() {
    return this._httpClient.get(
      `${this._apacheLogEnv.baseUrl}/api/WhitelistedResources/totalcount`
    );
  }
}

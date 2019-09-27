import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OverviewService {
  private _basePath = 'https://localhost:5001/api/BlacklistedResources';

  constructor(private readonly _httpClient: HttpClient) {}

  countBlacklistedResources() {
    return this._httpClient.get(`${this._basePath}/totalcount`);
  }
}

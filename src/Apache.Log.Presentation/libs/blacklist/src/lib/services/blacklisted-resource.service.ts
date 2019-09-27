import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { BlacklistedResource } from '../models/blacklisted-resource.model';
@Injectable({
  providedIn: 'root'
})
export class BlacklistedResourceService {
  private _basePath = 'https://localhost:5001/api/BlacklistedResources';

  private readonly refresh$ = new BehaviorSubject(undefined);
  blacklistedResources$ = this.refresh$.pipe(
    switchMap(() => this._httpClient.get<BlacklistedResource[]>(this._basePath))
  );

  constructor(private readonly _httpClient: HttpClient) {}

  addBlacklistedResource(fullPath: string) {
    this._httpClient
      .post(`${this._basePath}`, { fullPath })
      .subscribe(
        _ => this.refresh$.next(undefined),
        error => console.log(error)
      );
  }

  deleteBlacklistedResource(id: number) {
    this._httpClient
      .delete(`${this._basePath}/${id}`)
      .subscribe(
        _ => this.refresh$.next(undefined),
        error => console.log(error)
      );
  }

  countBlacklistedResources() {
    this._httpClient.get(`${this._basePath}/totalcount`);
  }
}

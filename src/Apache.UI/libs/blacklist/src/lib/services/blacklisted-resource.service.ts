import { BlacklistedResource } from '../models/blacklisted-resource.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class BlacklistedResourceService {
  private _basePath = 'https://localhost:5001/api/BlacklistedResources';

  private readonly refresh$ = new BehaviorSubject(undefined);
  blacklistedResources$ = this.refresh$.pipe(
    switchMap(() => this.httpClient.get<BlacklistedResource[]>(this._basePath))
  );

  constructor(private readonly httpClient: HttpClient) {}

  deleteBlacklistedResource(id: number) {
    this.httpClient
      .delete(`${this._basePath}/${id}`)
      .subscribe(
        result => this.refresh$.next(undefined),
        error => console.log(error)
      );
  }
}

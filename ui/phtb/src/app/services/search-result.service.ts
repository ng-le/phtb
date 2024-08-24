import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { map } from 'rxjs/operators';
import { SearchResult } from '../models/search-result.model';


@Injectable({
  providedIn: 'root'
})
export class SearchResultService {
  constructor (
    private apiService: ApiService
  ) {}

  getAll(): Observable<SearchResult[]> {
    return this.apiService.get(`/SearchResult/all`)
      .pipe(map(data => data));
  }

  getSearchResult(keywords: string, targetUrl: string, searchEngineType: number) {
    return this.apiService.get(`/SearchResult?Keywords=${keywords}&TargetUrl=${targetUrl}&SearchEngineType=${searchEngineType}`)
      .pipe(map(data => data));
  }
}

export interface SearchResult {
  id: string;
  searchEngineType: number;
  keywords: string;
  targetUrl: string;
  result: string;
  createdOn: string;
}

export enum SearchEngineType {
  Google = 0,
  Bing = 1
}

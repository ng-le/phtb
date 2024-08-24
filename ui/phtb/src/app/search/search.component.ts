import { Component, EventEmitter, OnInit, Output, ViewChild } from "@angular/core";
import { SearchResultService } from "../services/search-result.service";
import { NgForm } from "@angular/forms";
import { SearchEngineType, SearchResult } from "../models/search-result.model";
import { ApiService } from "../services/api.service";


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  searchFormModel: SearchFormModel = new SearchFormModel();
  isSubmitted: boolean = false;
  searchResult: SearchResult = {
    id: "",
    searchEngineType: 0,
    keywords: "",
    targetUrl: "",
    result: "",
    createdOn: ""
  };

  searchEngineType = SearchEngineType;
  enumKeys: number[] = [];
  selectedOption: SearchEngineType = SearchEngineType.Google;

  @ViewChild("searchForm")
  searchForm!: NgForm;

  @Output() searchCompletedEvent = new EventEmitter<void>();

  constructor(private searchResultService: SearchResultService) {
    this.enumKeys = Object.keys(this.searchEngineType)
      .filter((key) => !isNaN(Number(key)))
      .map((key) => Number(key));
  }

  ngOnInit(): void {

  }

  search(isValid: any) {
    this.isSubmitted = true;
    if (isValid) {
      this.searchResultService.getSearchResult(
          this.searchFormModel.keywords,
          this.searchFormModel.targetUrl,
          this.searchFormModel.searchEngineType)
        .subscribe(data => {
          this.searchResult = data;
          this.isSubmitted = false;
          this.searchCompletedEvent.emit();
        });
    }
  }
}

export class SearchFormModel {
  searchEngineType: number = 0;
  keywords: string = "";
  targetUrl: string = "";
}

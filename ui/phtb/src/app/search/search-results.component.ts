import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { SearchResult } from "../models/search-result.model";
import { SearchResultService } from "../services/search-result.service";

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss']
})
export class SearchResultsComponent implements OnInit, OnChanges {
  @Input() searchCompleted: boolean = false;

  searchResults: SearchResult[] = [];

  constructor(private searchResultService: SearchResultService) {
  }

  ngOnInit(): void {
    this.populateSearchResults();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.populateSearchResults();
  }

  populateSearchResults(): void {
    this.searchResultService.getAll()
      .subscribe(data => this.searchResults = data);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';

import { Observable } from 'rxjs';

import { PoLookupFilter, PoLookupFilteredItemsParams } from '@portinari/portinari-ui';

@Injectable()
export class MedicoLookupService implements PoLookupFilter {

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getFilteredItems(filteredParams: PoLookupFilteredItemsParams): Observable<any> {
    const { page, pageSize } = filteredParams;
    const params = { ...filteredParams, page: page.toString(), pageSize: pageSize.toString() };

    return this.httpClient.get(this.baseUrl+'api/profissional/medicogetlookup');
  }

  getObjectByValue(value: string): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}api/profissional${value}`);
  }
}
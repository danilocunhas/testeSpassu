import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  public apiUrl = 'https://localhost:8081';

  constructor() { }
}
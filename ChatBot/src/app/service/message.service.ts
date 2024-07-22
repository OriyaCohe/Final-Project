import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  constructor(public http: HttpClient) {}
  SendMessage(message: string) {
    return this.http.get<string>(
      `https://localhost:44375/api/Parsing/sentenceAnalysis?message=${message}`
    );
  }
  SendMessageContinued(message: string) {
    return this.http.get<string>(
      `https://localhost:44375/api/Parsing/AnalyzingCustomerResponse?message=${message}`
    );
  }
  newRequest()
  {
     return this.http.get<number>(
       `https://localhost:44375/api/Request/NewRequest`
     );
  }
}

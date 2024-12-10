import { Author } from "./author.model";

export class Book {
    constructor(
      public id: number,
      public bookCode: number,
      public title: string,
      public publisher: string,
      public edition: number,
      public publishYear: number,
      public price: number,
      public authorIds: number[], 
      public subjectIds: number[] 
    ) {}
  }
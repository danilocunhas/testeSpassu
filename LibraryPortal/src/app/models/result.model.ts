export interface IResult {
    isSuccess: boolean;
    value?: any;
    errors?: Error[];
    data?: any; 
  }

  export interface Error {    
    message: string;    
  }
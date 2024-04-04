import { Book } from "./Book.interface";

export interface User {
  id: number;
  name: string;
  username: string,
  password: string,
  tokens_Available:	number
  books_Borrowed:	Book[]
  books_Lent: Book[]	
}

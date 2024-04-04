// import { Category } from "./Category.interface";

import { User } from "./User.interface"

export interface Book{
    name: string,
    author: string,
    description: string,
    rating: number,
    genre: string,
    is_book_available: boolean
    bookImage: string,
    imageFile: File,
    lent_by_user_id: number,
    lentByUser: User,
    currently_borrowed_by_user_id: number,
    
    id: number,
}
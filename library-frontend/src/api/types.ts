export interface Book {
    id: number,
    title: string,
    isbn: string | null,
    authorName: string,
    categoryName: string
}


export interface LoginRequest {
    email: string,
    password: string
}

export interface Member {
    id: number,
    fullName: string,
    email: string,
    role: string
}

export interface AuthResponse {
    token: string,
    member: Member
}

export interface Author {
    id: number,
    fullName: string
}

export interface Category {
    id: number,
    name: string
}

export interface CreateBookRequest {
    title: string,
    isbn: string | null,
    authorId: number,
    categoryId: number
}

export interface Loan {
    id: number,
    bookTitle: string,
    memberName: string,
    loanDate: string,
    returnDate: string | null
}

export interface CreateLoanRequest {
    bookId: number,
    memberId: number
}

export interface CreateAuthorRequest {
    fullName: string
}

export interface CreateCategoryRequest {
    name: string
}

export interface RegisterRequest {
    fullName: string,
    email: string,
    password: string
}

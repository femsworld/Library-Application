export interface User{
    name: string,
    email: string,
    age: number,
    role: "admin" | "client" | "librarian",
    avatar: string
    password: string
    
}
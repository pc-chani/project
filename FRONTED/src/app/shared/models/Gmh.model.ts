import { User } from './User.model'

export class GMH{
    GmhCode :number
    GmhName:string
    Adress:string
    User:User
    UserCode:number

    CategoryCode:number
    CategoryName:string

    Phone:string
    e_mail:string
    comments:string
}

export class needsGmh{
   id :number
   categoryCode :number
   categotyName:string
   Adress:string
}
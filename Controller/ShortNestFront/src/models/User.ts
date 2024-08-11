export class User {
    public id: string = ''
    public name: string = ''
    public lastname: string = ''
    public username: string = ''
    public password: string = ''
    public createdAt: Date = new Date()
    public updatedAt: Date = new Date()
    public email: string = ''
    public roles: string[] = []
}
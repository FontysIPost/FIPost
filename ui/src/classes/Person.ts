export default class Person {
    public id: string = "";
    public name: string = "";
    public email: string = "";

    constructor(id: string, name: string, email: string){
        this.id = id;
        this.name = name;
        this.email = email;
    }
}
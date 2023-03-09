import City from "./City";

export default class Address {
    public city: City;
    public street: string;
    public number: number;
    public addition: string;
    public postalCode: string;


    constructor(city: City, street: string, postalCode: string, number: number, addition: string){
        this.city = city;
        this.street = street;
        this.postalCode = postalCode;
        this.number = number;
        this.addition = addition;
    }
}
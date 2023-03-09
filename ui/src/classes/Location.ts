export default class Location {
    public Id: Number;
    public City: String;
    public Location: String;
    public CollectionPoint: String;

    constructor(id: Number, city: String, location: String, collectionPoint: String){ 
        this.Id = id;
        this.City = city;
        this.Location = location;
        this.CollectionPoint = collectionPoint;
    }
}
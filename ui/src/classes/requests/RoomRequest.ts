export default class RoomRequest {
    public Name: string;
    public BuildingId: string;

    constructor(name: string, buildingId: string){
        this.Name = name;
        this.BuildingId = buildingId;
    }
}
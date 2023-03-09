import AddressRequest from "@/classes/requests/AddressRequest";

export default class BuildingRequest {
    public Name: string;
    public Address: AddressRequest;

    constructor(name: string, address: AddressRequest){
        this.Name = name;
        this.Address = address;
    }
}
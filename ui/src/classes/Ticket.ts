import Room from "@/classes/Room"

export default interface Ticket {
  id: String;
  location: Room;
  finishedAt: number;
  completedByPerson: String;
  receivedByPerson: String;
}
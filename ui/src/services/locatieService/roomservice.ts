import http from '@/services/http';
import Room from "@/classes/Room";
import RoomRequest from "@/classes/requests/RoomRequest";
import {config} from "@fortawesome/fontawesome-svg-core";

export default class RoomService {
  public async post(roomModel): Promise<Room> {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.post(`/api/locations/rooms`, roomModel, config);
    return response.data;
  }

  public async getAll(): Promise<Array<Room>> {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.get(`/api/locations/rooms`, config);
    return response.data;
  }

  public async getById(id: string): Promise<Room> {
    if(!id) {
      throw new Error("");
    }
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.get(`/api/locations/rooms/${id}`, config);
    return response.data;
  }

  public async update(room: RoomRequest, id: string) {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.put(`/api/locations/rooms/${id}`, room, config);
    return response.data;
  }

  public async delete(id: string) {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.delete(`/api/locations/rooms/${id}`, config);
    return response.data;
  }
}

export const roomService = new RoomService();

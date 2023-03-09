import Package from '@/classes/Package';
import http from '@/services/http';
import TicketRequest from '@/classes/requests/TicketRequest';

export default class PakketService {

  public async getAll(): Promise<Array<Package>> {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    console.log(config);
    const response = await http.get(`/api/packages`, config);
    return response.data;
  }
  public async post(packageModel): Promise<Package> {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.post(`/api/packages`, packageModel, config);
    return response.data;
  }

  public async get(id): Promise<Package> {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    const response = await http.get(`/api/packages/${id}`, config);
    return response.data;
  }

  public async createTicket(request: TicketRequest ) : Promise<Package> {
    const config = {
      'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
    }
    console.log(request)
    const response = await http.post(`/api/packages/tickets`, request, config);
    return response.data;
  }
}

// A singleton instance
export const pakketService = new PakketService();

import Person from '@/classes/Person';
import http from '@/services/http';

export default class PersoneelService {

   public async getAll(): Promise<Array<Person>> {
      const config = {
         'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
      }
      const response = await http.get(`/api/persons`, config);
      return response.data;
   }

   public async get(id: string): Promise<Person> {
      if(!id) {
        throw new Error("");
      }
      const config = {
         'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
      }
      const response =  await http.get(`/api/persons/${id}`, config);
      return response.data;
   }

   public async getByFontysId(id: string): Promise<Person> {
      if(!id) {
         throw new Error("");
      }
      const config = {
         'headers': {'Authorization': 'Bearer ' + localStorage.getItem('token')}
      }
      const response =  await http.get(`/api/persons/getbyfontysid/${id}`, config);
      return response.data;
   }
}

// A singleton instance
export const personeelService = new PersoneelService();

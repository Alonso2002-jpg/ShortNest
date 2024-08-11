import axios from "axios";
import {UrlStorage} from "../models/UrlStorage.ts";

export class UrlStorageService {
  
  private readonly route = `${import.meta.env.VITE_BASE_URL}/UrlStorage`;
  
  public async getAllUrlStorage() {
    return await axios.get<UrlStorage>(this.route);
  }
  
  public async getUrlStorageById(id: string) {
    return await axios.get<UrlStorage>(`${this.route}/${id}`);
  }
  
  public async getUrlStorageByUserId(){
    return await axios.get<UrlStorage[]>(`${this.route}/user`);
  }
  
  public async getUrlStorageByShortUrl(shortUrl: string) {
    return await axios.get<UrlStorage>(`${this.route}/urlShort/${shortUrl}`);
  }
  
    public async createUrlStorage(urlStorage: UrlStorage) {
        return await axios.post<UrlStorage>(this.route, urlStorage);
    }
    
    public async updateUrlStorage(urlStorage: UrlStorage) {
        return await axios.put<UrlStorage>(`${this.route}/${urlStorage.id}`, urlStorage);
    }
    
    public async deleteUrlStorage(id: string) {
        return await axios.delete(`${this.route}/${id}`);
    }
}
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
  }
}
export const keepsService = new KeepsService();
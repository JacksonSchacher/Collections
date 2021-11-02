import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async setCurrentKeep(keep) {
    const res = await api.get(`api/keeps/${keep.id}`)
    AppState.currentKeep = new Keep(res.data)
    logger.log('Current Keep', res.data)
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    AppState.keeps.push(new Keep(res.data))
    logger.log("Created Keep", res.data)
  }
}
export const keepsService = new KeepsService();
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async setCurrentKeep(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.currentKeep = new Keep(res.data)
    logger.log('Current Keep', res.data)
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    AppState.keeps.push(new Keep(res.data))
    logger.log("Created Keep", res.data)
  }
  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    let foundKeep = AppState.keeps.findIndex(k => k.id == keepId)
    AppState.keeps.splice(foundKeep, 1)
    logger.log("deleted keep", res.data)
  }
}
export const keepsService = new KeepsService();
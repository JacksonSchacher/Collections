import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { Profile } from "../models/Profile";
import { Vault } from "../models/Vault";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfilesService {
  async setCurrentProfile(creatorId) {
    const res = await api.get(`api/profiles/${creatorId}`)
    AppState.currentProfile = new Profile(res.data)
    logger.log("Current Profile", AppState.currentProfile)
  }
  async getVaults(creatorId) {
    const res = await api.get(`api/profiles/${creatorId}/vaults`)
    AppState.profileVaults = res.data.map(v => new Vault(v))
  }
  async getKeeps(creatorId) {
    const res = await api.get(`api/profiles/${creatorId}/keeps`)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
    logger.log("profile keeps", AppState.profileKeeps)
  }
}
export const profilesService = new ProfilesService();
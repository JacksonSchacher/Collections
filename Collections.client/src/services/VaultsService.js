import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { VaultKeep } from "../models/VaultKeep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService {
  async getMyVaults(userId) {
    const res = await api.get(`api/profiles/${userId}/vaults`)
    AppState.myVaults = res.data.map(v => new Vault(v))
    logger.log("my vaults", res.data)
  }
  async addKeepToVault(vaultId, keepId) {
    let vaultKeepData = new VaultKeep
    vaultKeepData.keepId = keepId
    vaultKeepData.vaultId = vaultId
    const res = await api.post('api/vaultkeeps', vaultKeepData)
    AppState.vaultKeeps = [new VaultKeep(res.data), ...AppState.vaultKeeps]
  }
}
export const vaultsService = new VaultsService();
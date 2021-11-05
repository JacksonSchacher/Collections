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
  async deleteVaultKeep(vaultKeepId, vaultId) {
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    this.getVaultKeeps(vaultId)
  }
  async editVault(vaultData, vaultId) {
    const res = await api.put(`api/vaults/${vaultId}`, vaultData)
    foundVault = AppState.profileVaults.findIndex(v => v.id == vaultId)
    AppState.profileVaults.splice(foundVault, 1, new Vault(res.data))
  }
  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    AppState.myVaults.push(new Vault(res.data))
  }
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.currentVault = new Vault(res.data)
  }
  async getVaultKeeps(vaultId) {
    AppState.vaultKeeps = []
    const res = await api.get(`api/vaults/${vaultId}/vaultKeeps`)
    AppState.vaultKeeps = res.data.map(vk => new VaultKeep(vk))
  }
  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    AppState.currentVault = {}
  }
}
export const vaultsService = new VaultsService();
export class VaultKeep {
  constructor(vaultKeepData = {}) {
    this.id = vaultKeepData.id
    this.creatorId = vaultKeepData.creatorId
    this.vaultId = vaultKeepData.vaultId
    this.keepId = vaultKeepData.keepId
    this.creator = vaultKeepData.creator
    this.keep = vaultKeepData.keep
  }
}
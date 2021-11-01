
class KeepsService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data.map(k => new Keep(k))
    } catch (error) {
      logger.error("Failed to Get Keeps")
    }
  }
}
export const keepsService = new KeepsService();
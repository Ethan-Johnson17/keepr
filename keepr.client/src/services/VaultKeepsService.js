import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async newVK(vK) {
    const res = await api.post('api/vaultkeeps', vK)
    logger.log('VK', res.data)
    AppState.vaultKeeps.push(res.data)
  }
}
export const vaultKeepsService = new VaultKeepsService()

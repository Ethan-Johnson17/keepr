import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async newVK(vK) {
    logger.log('ids', vK)
    const res = await api.post('api/vaultkeeps', vK)
    logger.log('VK', res.data)
    AppState.vaultKeeps.push(res.data)
  }

  async remove(id) {
    const res = await api.delete('api/vaultkeeps/' + id)
    logger.log('Delete', res.data)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.vaultKeepId !== id)
  }
}
export const vaultKeepsService = new VaultKeepsService()

import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async newVault(vault) {
    const res = await api.post('api/vaults', vault)
    AppState.vaults.push(res.data)
  }

  async getAllByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
  }

  async getByAccount() {
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data
  }

  async getById(id, userId) {
    const res = await api.get('api/vaults/' + id)
    // if (res.data.isPrivate = true && userId != res.data.creatorid) {
    //   logger.error('error')
    // }
    AppState.activeVault = res.data
  }
  async getKeepsByVault(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    logger.log('VK', res.data)
    AppState.vaultKeeps = res.data
  }

  async remove(id) {
    await api.delete('api/vaults/' + id)
    AppState.vaults = AppState.vaults.filter(v => v.id !== id)

  }

}
export const vaultsService = new VaultsService()
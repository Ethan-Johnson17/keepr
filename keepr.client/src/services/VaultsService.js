import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async newVault(vault) {
    const res = await api.post('api/vaults', vault)
    logger.log('create', res.data)
    AppState.vaults.push(res.data)
  }

  async getAllByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    logger.log('profile', res.data)
    AppState.vaults = res.data
  }

}
export const vaultsService = new VaultsService()
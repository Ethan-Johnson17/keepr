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

  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

}
export const vaultsService = new VaultsService()
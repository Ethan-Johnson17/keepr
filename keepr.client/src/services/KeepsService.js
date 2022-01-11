import { api } from "./AxiosService"
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"


class KeepsService {

  async getAll(query = '') {
    const res = await api.get(query)
    AppState.keeps = res.data
  }

  async getAllByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    logger.log('profile', res.data)
    AppState.keeps = res.data
  }

  async newKeep(keep) {
    const res = await api.post('api/keeps', keep)
    logger.log('create', res.data)
    AppState.keeps.push(res.data)
  }

  async remove(id) {
    const res = await api.delete('api/keeps/' + id)
    logger.log('Delete', res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)

  }

}
export const keepsService = new KeepsService()
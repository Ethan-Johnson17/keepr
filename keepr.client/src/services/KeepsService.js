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

  async editStats(keep) {
    // logger.log('Edit', keep)
    const res = await api.put('api/keeps/' + keep.id + '/stats', keep)
    // logger.log('Edit', res.data)
    AppState.activeKeep = res.data
  }
  async edit(keep) {
    logger.log('Edit', keep)
    const res = await api.put('api/keeps/' + keep.id, keep)
    logger.log('Edit', res.data)
    AppState.activeKeep = res.data
  }

}
export const keepsService = new KeepsService()
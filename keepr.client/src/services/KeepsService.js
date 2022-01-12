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
    AppState.keeps.push(res.data)
  }

  async remove(id) {
    const res = await api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }

  async editStats(keep) {
    const res = await api.put('api/keeps/' + keep.id + '/stats', keep)
    AppState.activeKeep = res.data
    const found = AppState.keeps.findIndex(k => k.id == keep.id)
    AppState.keeps.splice(found, 1, res.data)
  }
  async edit(keep) {
    const res = await api.put('api/keeps/' + keep.id, keep)
    AppState.activeKeep = res.data
  }

}
export const keepsService = new KeepsService()
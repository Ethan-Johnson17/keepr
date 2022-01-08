import { api } from "./AxiosService"
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"


class KeepsService {

  async getAll(query = '') {
    const res = await api.get(query)
    logger.log('Keeps', res.data)
    AppState.keeps = res.data
  }

}
export const keepsService = new KeepsService()
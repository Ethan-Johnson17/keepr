<template>
  <div class="container-fluid">
    <div class="row mb-3">
      <div class="col-md-2 text-center mt-5">
        <img class="img-fluid imgH" :src="account.picture" alt="" />
      </div>
      <div class="col-md-6 mt-5">
        <h1 class="f-42">{{ account.name }}</h1>
        <h4>Vaults: {{ vaults.length }}</h4>
        <h4>Keeps: {{ keeps.length }}</h4>
      </div>
    </div>
    <div class="row align-items-center justify-content-center">
      <div class="py-1 me-2 d-flex">
        <h1 class="me-3 text-danger">Vaults</h1>
      </div>
    </div>
    <div class="row">
      <div class="col m-3 bg-img" v-for="v in vaults" :key="v.id">
        <Vault :vault="v" />
      </div>
    </div>
    <div class="row align-items-center justify-content-center">
      <div class="py-1 me-2 d-flex">
        <h1 class="me-3 text-danger">Keeps</h1>
      </div>
      <div class="row">
        <div class="col m-3 bg-img" v-for="k in keeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
    <KeepModal />
    <NewVaultModal />
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await keepsService.getAllByProfile(route.params.id)
        await vaultsService.getAllByProfile(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),

    }
  }
}
</script>

<style scoped>
img {
  width: 200px;
}
</style>

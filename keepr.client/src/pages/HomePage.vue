<template>
  <div class="align-items-center justify-content-center container-fluid">
    <div class="text-end py-3 me-2">
      <i
        data-bs-toggle="modal"
        data-bs-target="#newKeepModal"
        title="Add Keep"
        class="
          mdi mdi-plus
          f-24
          text-light
          bg-dark
          rounded-circle
          px-1
          selectable
        "
      ></i>
    </div>
    <div class="row">
      <div class="col m-3 bg-img" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
  <KeepModal :account="account" />
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll('api/keeps')
        await vaultsService.getByAccount('api/vaults')
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
    }
  }
}

</script>

<style scoped lang="scss">
.bg-img {
  background-size: cover;
}
</style>

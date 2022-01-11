<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12 mt-5 ms-5">
        <h1 class="f-42">{{ vault.name }}</h1>
        <h4 class="ms-1">Keeps:</h4>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getById(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
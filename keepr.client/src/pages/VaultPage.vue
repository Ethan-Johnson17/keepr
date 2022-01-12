<template>
  <div class="container-fluid">
    <div class="row bg-img">
      <div class="col-md-9 mt-5 ms-5">
        <h1 class="f-42">{{ vault.name }}</h1>
        <h4 class="ms-1">Keeps: {{ vk.length }}</h4>
      </div>
      <div class="col-md-2 mt-5">
        <button
          v-if="vault.creatorId == account.id"
          type="button"
          class="btn btn-outline-danger"
          @click="remove(vault.id)"
        >
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col m-3 bg-img" v-for="vk in vk" :key="vk.id">
        <Keep :keep="vk" />
      </div>
    </div>
    <VkModal :keep="vk" />
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        await vaultsService.getById(route.params.id, AppState.account.id)
        await vaultsService.getKeepsByVault(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      vk: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),

      async remove(id) {
        const yes = await Pop.confirm('Delete Vault?')
        if (!yes) { return }
        await vaultsService.remove(id)
        router.push({
          name: "Account",
        })
        Pop.toast('Successfully deleted vault', 'success')
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  background-size: cover;
}
</style>
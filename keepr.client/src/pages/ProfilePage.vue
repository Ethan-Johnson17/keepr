<template>
  <div class="container-fluid">
    <div class="row mb-3">
      <div class="col-md-2 text-center mt-5">
        <img class="img-fluid imgH rounded" :src="account.picture" alt="" />
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
      <div
        class="col m-3 bg-img"
        v-for="v in vaults"
        :key="v.id"
        @click="routeTo(v.id)"
      >
        <Vault :vault="v" />
      </div>
    </div>
    <div class="row align-items-center justify-content-center">
      <div class="py-1 me-2 d-flex">
        <h1 class="me-3 text-danger">Keeps</h1>
      </div>
      <div class="masonry-with-columns">
        <div class="m-3" v-for="k in keeps" :key="k.id">
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
import { useRoute, useRouter } from 'vue-router'
import { accountService } from '../services/AccountService'
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        await keepsService.getAllByProfile(route.params.id)
        await vaultsService.getAllByProfile(route.params.id)
        await accountService.getProfile(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),

      routeTo(id) {
        router.push({
          name: "Vaults",
          params: { id: id }
        })
      }

    }
  }
}
</script>

<style scoped lang="scss">
img {
  width: 200px;
}

body {
  margin: 0.5;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 180px;
  column-gap: 1rem;
  div {
    margin: 0 1rem 0 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }
}
</style>

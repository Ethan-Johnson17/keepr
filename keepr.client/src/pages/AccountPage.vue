<template>
  <div class="container-fluid">
    <div class="row mb-3">
      <div class="col-md-2 text-center mt-5">
        <img
          class="img-fluid imgH"
          :src="account.picture"
          :alt="account.name"
        />
      </div>
      <div class="col-md-4 mt-5">
        <h1 class="f-42">
          {{ account.name }}
          <i
            class="col-md-2 mdi mdi-pencil-outline f-24"
            title="Edit account"
          ></i>
        </h1>
        <h4>Vaults: {{ myVaults.length }}</h4>
        <h4>Keeps: {{ keeps.length }}</h4>
      </div>
    </div>
    <div class="row align-items-center justify-content-center">
      <div class="py-1 me-2 d-flex">
        <h1 class="me-3 text-danger">Vaults</h1>
        <i
          data-bs-toggle="modal"
          data-bs-target="#newVaultModal"
          class="mdi mdi-plus f-24 text-danger px-1 mt-1 selectable"
          title="New Vault"
        ></i>
      </div>
    </div>
    <div class="row">
      <div
        class="col m-3 bg-img"
        v-for="v in myVaults"
        :key="v.id"
        @click="routeTo(v.id)"
      >
        <!-- <router-link :to="{ path: '/vaults/' + v.id }"> -->
        <Vault :vault="v" />
        <!-- </router-link> -->
      </div>
    </div>
    <div class="row align-items-center justify-content-center">
      <div class="py-1 me-2 d-flex">
        <h1 class="me-3 text-danger">Keeps</h1>
        <i
          data-bs-toggle="modal"
          data-bs-target="#newKeepModal"
          class="mdi mdi-plus f-24 text-danger px-1 mt-1 selectable"
          title="New Keep"
        ></i>
      </div>
      <div class="row">
        <div class="col-md-2 m-3" v-for="k in keeps" :key="k.id">
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
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    const router = useRouter()
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
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps.filter(k => k.creatorId == AppState.account.id)),
      myVaults: computed(() => AppState.myVaults),

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

<style scoped>
img {
  width: 200px;
}
</style>

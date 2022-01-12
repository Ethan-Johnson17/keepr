<template>
  <div class="">
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
    <div class="masonry-with-columns">
      <div class="m-3" v-for="k in keeps" :key="k.id">
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

body {
  margin: 0.5;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 180px;
  column-gap: 1rem;
  div {
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }
}

// @media screen and (max-width: 600px) {
//   .masonry-with-columns {
//     columns: 1 175px;
//   }
// }
</style>

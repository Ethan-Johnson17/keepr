<template>
  <div
    class="
      Keep
      row
      bg-img
      justify-content-between
      elevation-3
      rounded
      onHover
      selectable
    "
    @click="setActive(keep)"
    :style="{
      'background-image': 'url(' + keep.img + ')',
      height: '300px',
    }"
  >
    <div class="col-md-12 mt-auto bg-opac">
      <div class="row">
        <div class="col-md-9 mt-auto text-light">
          <h4 class="stroke-text">{{ keep.name }}</h4>
        </div>
        <div class="col-md-3 mt-auto">
          <img
            class="rounded-circle selectable mb-1 ms-3"
            height="30"
            :title="keep.creator.name"
            :src="keep.creator.picture"
            @click.stop="profilePage(keep.creatorId)"
          />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
  props: { keep: { type: Object } },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    // onMounted(async () => {
    //   try {
    //     await keepsService.getAll('api/keeps')
    //   } catch (error) {
    //     logger.error(error)
    //     Pop.toast(error.message, 'error')
    //   }
    // })
    return {
      profile: computed(() => AppState.profiles),
      account: computed(() => AppState.account),

      profilePage(id) {
        router.push({ path: '/profile/' + id })
      },

      async setActive(keep) {
        try {
          AppState.activeKeep = keep
          keep.views++
          let stats = { views: keep.views, keeps: keep.keeps, shares: keep.shares, id: keep.id, creatorId: keep.creatorId, }
          Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle()
          await keepsService.editStats(stats)
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("keepModal")).hide()
          Pop.toast(error, 'error', 'center')
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  min-width: 300px;
}

.onHover {
  transition: 500ms;
  background-size: cover;
}
.onHover:hover {
  transform: scale(1.02);
}

// .stroke-text {
//   -webkit-text-stroke: 1.5px rgb(20, 20, 20, 0.3);
// opacity: 1;
// }

.bg-opac {
  background-color: rgb(121, 121, 121, 0.5);
  // box-shadow: 2px black;
  // filter: blur(2px);
  // -webkit-filter: blur(2px);
}
</style>
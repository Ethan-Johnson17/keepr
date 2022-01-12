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
    :style="{
      'background-image': 'url(' + vault.img + ')',
      height: '300px',
    }"
  >
    <div class="col-md-12 mt-auto bg-opac">
      <div class="row">
        <div class="col-md-9 mt-auto text-light">
          <h4 class="stroke-text">{{ vault.name }}</h4>
        </div>
        <!-- NOTE show how many keeps -->
        <!-- <div class="col-md-3">
          <p>{{ vault.keeps.length }}</p>
        </div> -->
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
export default {
  props: { vault: { type: Object } },
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

      profilePage(id) {
        router.push({ path: '/profile/' + id })
      },

      async setActive(keep) {
        try {
          AppState.activeKeep = keep
          // await keepsService.getByRestaurantId(props.restaurant.id)
          Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle()
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("keepModal")).hide()
          Pop.toast(error, 'error', 'center')
        }
      }
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
}
.onHover:hover {
  transform: scale(1.02);
}

.stroke-text {
  -webkit-text-stroke: 1px rgb(20, 20, 20);
  opacity: 1;
}

.bg-opac {
  background-color: rgb(121, 121, 121, 0.5);
  // box-shadow: 2px black;
  // filter: blur(2px);
  // -webkit-filter: blur(2px);
}
</style>
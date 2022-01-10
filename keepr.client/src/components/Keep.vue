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
    <div class="col-md-9 mt-auto text-light elevation-3 blur">
      <h4 class="stroke-text">{{ keep.name }}</h4>
    </div>
    <div class="col-md-3 mt-auto">
      <img
        class="rounded-circle selectable"
        height="30"
        :title="keep.creator.name"
        :src="keep.creator.picture"
        @click.stop="profilePage(keep.creatorId)"
      />
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

.blur {
  opacity: 0.2;
  background-color: grey;
  // filter: blur(2px);
  // -webkit-filter: blur(2px);
}
</style>
<template>
  <div
    class="Keep row bg-img"
    :style="{
      'background-image': 'url(' + keep.img + ')',
      height: '300px',
    }"
  >
    <div class="col-md-6">
      <h4>{{ keep.name }}</h4>
    </div>
    <div class="col-md-6">
      <img class="rounded-circle" height="30" :src="keep.creator.picture" />
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  props: { keep: { type: Object } },
  setup(props) {
    // onMounted(async () => {
    //   try {
    //     await keepsService.getAll('api/keeps')
    //   } catch (error) {
    //     logger.error(error)
    //     Pop.toast(error.message, 'error')
    //   }
    // })
    return {
      profile: computed(() => AppState.profiles)
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-img {
  min-width: 300px;
}
</style>
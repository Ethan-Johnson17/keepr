<template>
  <Modal id="keepModal" size="modal-xl">
    <template #modal-body>
      <div class="container-fluid">
        <div class="text-end">
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="row">
          <div class="col-md-6">
            <img
              height="450"
              class="w-100 object-fit-cover img-fluid"
              :src="keep.img"
              :alt="keep.name"
            />
          </div>
          <div class="col-md-6">
            <div class="row justify-content-center">
              <div class="col-md-2 mdi mdi-eye">{{ keep.views }}</div>
              <div class="col-md-2">Icons</div>
              <div class="col-md-2">Icons</div>
            </div>
            <div class="row">
              <div class="col-md-12 text-center mt-5 mb-4">
                <h3>{{ keep.name }}</h3>
              </div>
              <div class="col-md-12 border-bottom border-dark">
                <p>
                  {{ keep.description }} - Lorem ipsum dolor sit amet
                  consectetur, adipisicing elit. Dignissimos placeat sequi
                  maiores repellat voluptatibus dolor cum quis saepe, nemo
                  consectetur, doloribus illum sed labore minus nesciunt
                  corporis, natus laudantium odit! Dignissimos rem vero fugiat
                  saepe nostrum, voluptatum necessitatibus corrupti sunt sed,
                  placeat labore adipisci a, vel ad veritatis ex. Doloribus
                  totam odit autem! Laborum consequatur quidem saepe quas
                  aperiam. Nihil.
                </p>
              </div>
            </div>
            <div class="row">
              <div class="col-md-5"></div>
              <div
                v-if="account.id == keep.creatorId"
                class="col-md-2 mdi mdi-trash-can-outline f-16 selectable"
                @click="remove(keep.id)"
              ></div>
              <div class="col-md-5"></div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),

      async remove(id) {
        const yes = await Pop.confirm('Delete Keep?')
        if (!yes) { return }
        Modal.getOrCreateInstance('#keepModal').toggle()
        await keepsService.remove(id)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
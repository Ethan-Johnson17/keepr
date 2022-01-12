<template>
  <Modal id="vkModal" size="modal-xl">
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
          <div
            class="col-md-5 mb-3 size rounded me-2"
            :style="{
              'background-image': 'url(' + keep.img + ')',
            }"
          ></div>
          <div class="col-md-6">
            <div class="row justify-content-center">
              <div class="col-md-2 mdi mdi-eye">{{ keep.views }}</div>
              <div class="col-md-2 text-center d-flex">
                <h6 class="border border-2 border-danger me-2 px-1 rounded">
                  K
                </h6>
                {{ keep.keeps }}
              </div>
              <div class="col-md-2 mdi mdi-share-variant">
                {{ keep.shares }}
              </div>
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
            <div class="row justify-content-evenly mt-2">
              <div class="col-md-6 mt-auto ms-0 ps-0">
                <button
                  @click="remove(keep.vaultKeepId)"
                  class="btn btn-outline-danger"
                >
                  Remove from vault
                </button>
              </div>
              <div class="col-md-6 text-center me-0 pe-0">
                <img
                  :src="keep.creator?.picture"
                  class="profile-img rounded"
                  :alt="keep.creator?.name"
                />
                {{ keep.creator?.name }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
export default {
  props: { account: { type: Object }, vk: { type: Object } },
  setup(props) {
    return {
      keep: computed(() => AppState.activeVaultKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),

      async remove(id) {
        try {
          // logger.log(keep)
          const yes = await Pop.confirm('Remove Keep from vault?')
          if (!yes) { return }
          Modal.getOrCreateInstance('#vkModal').toggle()
          await vaultKeepsService.remove(id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }

      },


      // NOTE Come back to this, create vault and add keep to it
      // addToNewVault(vault, keep) {
      //   const vK = { vaultId: vault.id, keepId: keep.id }
      //   vaultKeepsService.newVK(vK)
      // }
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img {
  height: 35px;
}

.size {
  // height: 300px;
  background-size: cover;
}
</style>
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
          <div
            class="col-md-6 mb-3 size rounded"
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
            <div class="row mt-auto justify-content-center">
              <div class="col-md-5 mt-auto">
                <div class="dropdown mx-4 my-2">
                  <button
                    class="btn btn-secondary dropdown-toggle"
                    type="button"
                    id="dropdownMenuButton1"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                    required
                  >
                    {{ vaultsList }}
                  </button>
                  <ul
                    class="dropdown-menu"
                    aria-labelledby="dropdownMenuButton1"
                  >
                    <li v-for="v in myVaults" :key="v.id">
                      <div
                        class="dropdown-item selectable"
                        @click="(vaultsList = v.name), addToVault(v, keep)"
                      >
                        {{ v.name }}
                      </div>
                    </li>
                    <!-- <li>
                      <div
                        class="dropdown-item selectable"
                        @click="(vaultsList = v.name), addToNewVault(v, keep)"
                      >
                        Add new vault...
                      </div>
                    </li> -->
                  </ul>
                </div>
              </div>
              <div
                v-if="account.id == keep.creatorId"
                class="col-md-1 mdi mdi-trash-can-outline f-16 selectable"
                @click="remove(keep.id)"
              ></div>
              <div class="col-md-5 text-center">
                <img
                  :src="keep.creator?.picture"
                  class="profile-img rounded-circle"
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
  props: { account: { type: Object } },
  setup(props) {
    const vaultsList = ref('Choose a vault')
    return {
      vaultsList,
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),

      async remove(id) {
        const yes = await Pop.confirm('Delete Keep?')
        if (!yes) { return }
        Modal.getOrCreateInstance('#keepModal').toggle()
        await keepsService.remove(id)
      },

      async addToVault(vault, keep) {
        try {
          const vK = { vaultId: vault.id, keepId: keep.id }
          await vaultKeepsService.newVK(vK)
          keep.keeps++
          let stats = { keeps: keep.keeps, views: keep.views, id: keep.id, creatorId: keep.creatorId, }
          await keepsService.editStats(stats)
          Pop.toast('Keep added to ' + vault.name, 'success')
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
  height: 40px;
}

.size {
  // height: 300px;
  background-size: cover;
}
</style>
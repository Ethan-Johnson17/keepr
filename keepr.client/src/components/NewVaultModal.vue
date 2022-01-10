<template>
  <Modal id="newVaultModal" size="modal-s">
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
        <form @submit.prevent="newVault">
          <div class="row">
            <div class="col-md-12">
              <h3>New Vault</h3>
            </div>
            <div class="col-md-12">
              <label>Name</label>
              <input
                type="text"
                aria-label="Name"
                placeholder="Name"
                class="form-control"
                v-model="editable.name"
                required
              />
            </div>
            <!-- <div class="col-md-12 my-3">
              <label>Image</label>
              <input
                type="url"
                aria-label="img"
                placeholder="imgUrl"
                class="form-control"
                v-model="editable.img"
                required
              />
            </div> -->
            <div class="col-md-12">
              <label>Description</label>
              <textarea
                type="text"
                aria-label="description"
                placeholder="description"
                class="form-control"
                v-model="editable.description"
                required
              ></textarea>
            </div>
            <div class="col-md-2 my-3 d-flex">
              <input
                type="checkbox"
                aria-label="isPrivate"
                v-model="editable.isPrivate"
              />
              <label>Private?</label>
            </div>
          </div>
          <div class="text-end mt-3">
            <button
              class="btn btn-warning"
              type="submit"
              data-bs-toggle="modal"
              data-bs-target="#newVaultModal"
            >
              Submit
            </button>
          </div>
        </form>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
import { api } from '../services/AxiosService'
export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async newVault() {
        logger.log(editable.value)
        const vault = editable.value
        await vaultsService.newVault(vault)
        Modal.getOrCreateInstance(document.getElementById("newVaultModal")).toggle()
        editable.value = {}
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
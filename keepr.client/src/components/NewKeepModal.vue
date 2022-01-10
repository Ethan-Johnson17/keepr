<template>
  <Modal id="newKeepModal" size="modal-s">
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
        <form @submit.prevent="newKeep">
          <div class="row">
            <div class="col-md-12">
              <h3>New Keep</h3>
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
            <div class="col-md-12 my-3">
              <label>Image</label>
              <input
                type="url"
                aria-label="img"
                placeholder="imgUrl"
                class="form-control"
                v-model="editable.img"
                required
              />
            </div>
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
          </div>
          <div class="text-end mt-3">
            <button
              class="btn btn-warning"
              type="submit"
              data-bs-toggle="modal"
              data-bs-target="#newKeepModal"
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
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      keep: computed(() => AppState.activeKeep),

      async newKeep() {
        logger.log(editable.value)
        const keep = editable.value
        await keepsService.newKeep(keep)
        Modal.getOrCreateInstance(document.getElementById("newKeepModal")).toggle()
        editable.value = {}
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
<template>
  <div class="elevation-3 rounded onHover selectable" @click="setActive(keep)">
    <img class="px-0 rounded-3 img-fluid" :src="keep.img" :alt="keep.name" />
    <span class="d-flex justify-content-between position-absolute move">
      <h4 class="shadow text-light me-5 ms-2">{{ keep.name }}</h4>
      <img
        class="rounded selectable mb-1 ms-5"
        height="30"
        :title="keep.creator.name"
        :src="keep.creator.picture"
        @click.stop="profilePage(keep.creatorId)"
      />
    </span>
  </div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core";
import { AppState } from "../AppState";
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger";
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
export default {
  props: { keep: { type: Object }, vk: { type: Object } },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
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
        router.push({ path: "/profile/" + id });
      },

      async setActive(keep) {
        try {
          keep.views++;
          let stats = {
            views: keep.views,
            id: keep.id,
            creatorId: keep.creatorId,
            keeps: keep.keeps,
          };
          if (
            route.name == "Home" ||
            route.name == "Profile" ||
            route.name == "Account"
          ) {
            AppState.activeKeep = keep;
            Modal.getOrCreateInstance(
              document.getElementById("keepModal")
            ).toggle();
          } else if (route.name == "Vaults") {
            AppState.activeVaultKeep = keep;
            Modal.getOrCreateInstance(
              document.getElementById("vkModal")
            ).toggle();
          }
          await keepsService.editStats(stats);
        } catch (error) {
          logger.error(error);
          Modal.getOrCreateInstance(
            document.getElementById("keepModal")
          ).hide();
          Pop.toast(error, "error", "center");
        }
      },
    };
  },
};
</script>


<style lang="scss" scoped>
.onHover {
  transition: 500ms;
  background-size: cover;
}
.onHover:hover {
  transform: scale(1.02);
  box-shadow: 0 2px 5px -1px rgba(0, 0, 0, 0.2), 0 5px 6px 0 rgba(0, 0, 0, 0.14),
    0 1px 12px 0 rgba(0, 0, 0, 0.12);
}

.shadow {
  text-shadow: 2px 2px 2px rgb(35, 35, 35);
}

.move {
  bottom: -3px;
}

.bgColor {
  background-color: rgb(155, 155, 163, 0);
  padding-bottom: -5rem;
}
</style>
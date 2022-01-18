<template>
  <header>
    <Navbar />
  </header>
  <main class="pe-4 scrollbar">
    <router-view />
  </main>
  <!-- <footer>
    <div class="bg-secondary darken-20 text-light text-center p-4"></div>
  </footer> -->
  <newKeepModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.scrollbar {
  overflow-y: scroll;
  height: 74vh;
}
.scrollbar::-webkit-scrollbar {
  width: 7px;
}
.scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.scrollbar::-webkit-scrollbar-thumb {
  background-color: $primary;
  border-radius: 10px;
}

// body {
//   margin: 0;
//   padding: 1rem;
// }

// .masonry-with-columns {
//   columns: 6 200px;
//   column-gap: 1rem;
//   div {
//     width: 150px;
//     background: #ec985a;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   }
//   @for $i from 1 through 36 {
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }
</style>

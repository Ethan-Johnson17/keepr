import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profiles: [],
  keeps: [],
  vaults: [],
  activeKeep: {},
  activeVault: {},
  myVaults: [],
  vaultKeeps: []
})

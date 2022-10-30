import 'vuetify/styles';

import { createApp } from 'vue';
import { createStore } from 'vuex';
import { createVuetify } from 'vuetify';
import { loadFonts } from './plugins/webfontloader';

import App from './App.vue';

// Create a new store instance.
const store = createStore({
  state: {
    listeStation: {},
    provinces: [
      'BRITISH COLUMBIA',
      'ALBERTA',
      'SASKATCHEWAN',
      'MANITOBA',
      'ONTARIO',
      'QUEBEC',
      'NEW BRUNSWICK',
      'NOVA SCOTIA',
      'NEWFOUNDLAND',
    ],
  },
  mutations: {
    setListeStations(state, stations) {
      state.listeStation = { ...stations };
    },
  },
  actions: {
    setListeStations({ commit }, stations) {
      commit('setListeStations', stations);
    },
  },
  getters: {
    stations(state) {
      return state.listeStation;
    },
    provinces(state) {
      return state.provinces.sort();
    },
  },
});

loadFonts();

const app = createApp(App);
const vuetify = createVuetify(); // Replaces new Vuetify()

app.use(vuetify);
app.use(store);

app.mount('#app');

<template>
  <v-container>
    <v-expansion-panels :v-model="panel" :v-if="stations">
      <v-expansion-panel>
        <v-expansion-panel-title> ALL </v-expansion-panel-title>
        <v-expansion-panel-text>
          <ul style="list-style: none">
            <li
              v-for="(station, index) in getAllStations()"
              :key="index"
              @click="onStationSelected(station)"
              style="cursor: pointer"
              :class="selectedStationId == station.id && 'selected'"
            >
              {{ station.name }} ({{ station.airportCode }})
            </li>
          </ul>
        </v-expansion-panel-text>
      </v-expansion-panel>
      <v-expansion-panel
        v-for="(province, index) in provinces"
        :key="index"
        :value="province"
      >
        <v-expansion-panel-title>
          {{ province }}
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <ul style="list-style: none">
            <li
              v-for="(station, index) in getListeStationByProvince(province)"
              :key="index"
              @click="onStationSelected(station)"
              style="cursor: pointer"
              :class="selectedStationId == station.id && 'selected'"
            >
              {{ station.name }} ({{ station.airportCode }})
            </li>
          </ul>
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>
  </v-container>
</template>

<script>
import { reactive } from "@vue/reactivity";
import { mapState, mapGetters } from "vuex";

export default {
  name: "ListeStation",
  data() {
    return {
      selectedStationId: "",
    };
  },
  setup() {
    return {
      listeProvince: reactive([]),
      panel: [],
    };
  },
  props: {
    stations: {
      type: Object,
    },
  },
  methods: {
    getAllStations() {
      let stationArray = [];
      for (const [key, station] of Object.entries(this.stations)) {
        stationArray.push(station);
      }
      return stationArray.sort((a, b) => a.name.localeCompare(b.name));
    },
    getListeStationByProvince(province) {
      // TODO: could be done when all data is loaded
      let stationsProvince = [];
      for (const [key, station] of Object.entries(this.stations)) {
        if (station.province == province) {
          stationsProvince.push(station);
        }
      }
      return stationsProvince.sort((a, b) => a.name.localeCompare(b.name));
    },
    onStationSelected(station) {
      if (this.selectedStationId == station.id) {
        return;
      }

      this.$emit("onStationSelected", station);
      this.selectedStationId = station.id;
    },
  },

  computed: {
    ...mapState({
      stations: "listeStation",
      provinces: "provinces",
    }),
    ...mapGetters({
      listeStations: "stations",
      provinces: "provinces",
    }),
  },
};
</script>

<style lang="scss">
.v-expansion-panel-title {
  background: #adc5e7;

  &--active {
    background: #55ffb4;
  }
}

.selected {
  color: red;
}
</style>

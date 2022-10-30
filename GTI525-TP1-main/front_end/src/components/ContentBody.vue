<template>
  <v-container fluid>
    <v-row>
      <v-col cols="4">
        <ListeStation
          :stations="stations"
          @onStationSelected="handleStationSelected"
        />
      </v-col>
      <v-col>
        <InfoStation :station="selectedStation" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import InfoStation from "./InfoStation.vue";
import ListeStation from "./ListeStation.vue";

export default {
  name: "ContentBody",
  components: {
    InfoStation,
    ListeStation,
  },

  props: {
    stations: {
      type: Object,
    },
  },

  methods: {
    handleStationSelected(station) {
      this.selectedStation = station;
    },
  },

  data() {
    return {
      selectedStation: {},
    };
  },
};
</script>

<style></style>

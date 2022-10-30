<template>
  <v-app>
    <ContentHeader />
    <v-main>
      <ContentBody :stations="stations" />
    </v-main>
    <v-footer>
      <ContentFooter />
    </v-footer>
  </v-app>
</template>

<script>
import ContentBody from './components/ContentBody.vue';
import ContentHeader from './components/ContentHeader.vue';
import ContentFooter from './components/ContentFooter.vue';

export default {
  name: 'App',

  components: {
    ContentHeader,
    ContentBody,
    ContentFooter,
  },

  mounted() {
    const stationIds = Object.keys(window.stations);
    const rawStationInventory = window.stationInventory;

    // split data by line
    let stationData = rawStationInventory.split('\n').slice(5);
    let parsedStations = [];

    /**
     * Parse station data.
     * Recover needed fields: name, province and id
     */
    parsedStations = stationData.map((station) => {
      station = station.split(',');
      /*.join("")
        .split('"')
        .filter((x) => x != "");*/

      let newStation = {
        name: (station[0] ?? '').replaceAll('"', ''),
        province: (station[1] ?? '').replaceAll('"', ''),
        id: (station[3] ?? '').replaceAll('"', ''),
        airportCode: (station[5] ?? '').replaceAll('"', ''),
      };
      return newStation;
    });

    // filter stations with meteo data only
    parsedStations = parsedStations.filter((station) =>
      stationIds.includes(station.id)
    );

    let stationsDict = Object.assign(
      {},
      ...parsedStations.map((x, index) => ({
        [index]: {
          id: x.id,
          name: x.name,
          province: x.province,
          airportCode: x.airportCode,
        },
      }))
    );
    this.$store.dispatch('setListeStations', stationsDict);
    this.stations = stationsDict;
  },
};
</script>

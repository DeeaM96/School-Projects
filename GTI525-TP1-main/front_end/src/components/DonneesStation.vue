<template>
  <v-container>
    <v-row class="ma-auto">
      <v-col style="display: block">
        <table id="donneesTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Année</th>
              <th>Mois</th>
              <th>Temp. max moyenne (°C)</th>
              <th>Temp. min moyenne (°C)</th>
              <th>Temp. moyenne (°C)</th>
              <th>Temp. max enregistrée (°C)</th>
              <th>Temp. min enregistrée (°C)</th>
              <th>Pluie totale (mm)</th>
              <th>Neige totale (cm)</th>
              <th>Vitesse du vent max (km/h)</th>
            </tr>
          </thead>
        </table>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  name: "DonneesStation",
  props: {
    donnees: {
      type: Array,
    },
  },
  watch: {
    donnees: function () {
      $("#donneesTable").DataTable().destroy();
      $("#donneesTable").DataTable({
        data: this.donnees,
        columns: this.columns,
      });
    },
  },
  data() {
    return {
      columns: [
        { data: "year" },
        { data: "month" },
        { data: "meanMaxTempCelsius" },
        { data: "meanMinTempCelsius" },
        { data: "meanTempCelsius" },
        { data: "extrMaxTempCelsius" },
        { data: "extrMinTempCelsius" },
        { data: "totalPrecipMillimeter" },
        { data: "totalSnowCentimeter" },
        { data: "speedMaxGustKM_H" },
      ],
    };
  },
  mounted() {
    $("#donneesTable").DataTable({
      data: this.donnees,
      columns: this.columns,
    });
  },
};
</script>

<template>
  <v-container style="max-width: 1100px" v-if="station.id">
    <span>{{ station.name }} ({{ station.airportCode }})</span>
    <h3>Plage de dates</h3>

    <v-row class="ma-auto">
      <v-col style="display: block">
        <v-row class="ma-auto">
          <v-col class="pa-0 pb-1 align-self-end" cols="1"
            ><span> De: </span></v-col
          >
          <v-col class="pa-1" cols="4"
            ><v-select
              :return-object="false"
              hide-details
              variant="underlined"
              density="compact"
              label="Année"
              v-model="dates.deAnnee"
              :items="annees"
            ></v-select
          ></v-col>
          <v-col class="pa-1" cols="4"
            ><v-select
              :return-object="false"
              hide-details
              variant="underlined"
              density="compact"
              label="Mois"
              v-model="dates.deMois"
              :items="mois"
              item-value="value"
              item-title="title"
            ></v-select
          ></v-col>
        </v-row>
        <v-row class="ma-auto"
          ><v-col class="pa-0 pb-1 align-self-end" cols="1"
            ><span> À: </span></v-col
          >
          <v-col class="pa-1" cols="4"
            ><v-select
              :return-object="false"
              hide-details
              variant="underlined"
              density="compact"
              label="Année"
              v-model="dates.aAnnee"
              :items="annees"
            ></v-select
          ></v-col>
          <v-col class="pa-1" cols="4"
            ><v-select
              :return-object="false"
              hide-details
              variant="underlined"
              density="compact"
              label="Mois"
              v-model="dates.aMois"
              :items="mois"
              item-value="value"
              item-title="title"
            ></v-select></v-col
        ></v-row>
      </v-col>
      <v-col class="align-self-center" style="display: block">
        <v-btn @click="defaultDates()">Toutes les données</v-btn></v-col
      >
    </v-row>

    <v-divider></v-divider>

    <v-row class="ma-auto">
      <v-col style="display: block">
        <v-tabs v-model="tab">
          <v-tab value="donnees">Données Météo</v-tab>
          <v-tab value="statistiques">Statistiques Globales</v-tab>
        </v-tabs>
        <v-window v-model="tab">
          <v-window-item value="donnees">
            <DonneesStation :donnees="donneesMeteo" />
          </v-window-item>

          <v-window-item value="statistiques">
            <StatistiquesStation :donnees="statistiquesGlobales" />
          </v-window-item> </v-window></v-col
    ></v-row>
  </v-container>
</template>
<script>
import _ from "lodash";

import DonneesStation from "./DonneesStation.vue";
import StatistiquesStation from "./StatistiquesStation.vue";

export default {
  name: "InfoStation",
  components: {
    DonneesStation,
    StatistiquesStation,
  },
  props: {
    station: {
      type: Object,
    },
  },
  watch: {
    station: function () {
      console.log(this.station.id);
      const donneesStation = window.stations[this.station.id];
      this.donnees = this.parseDonnees(donneesStation);
      this.populateDates();
    },
    dates: {
      handler: function () {
        this.refreshDonnees();
      },
      deep: true,
    },
  },
  data() {
    return {
      tab: null,
      donnees: [],
      dates: {
        deAnnee: null,
        deMois: null,
        aAnnee: null,
        aMois: null,
      },
      annees: [],
      mois: [],
      maxAnnee: null,
      minAnnee: null,
      maxMois: null,
      minMois: null,
      donneesMeteo: null,
      statistiquesGlobales: {
        globale: [],
        janvier: [],
        fevrier: [],
        mars: [],
        avril: [],
        mai: [],
        juin: [],
        juillet: [],
        aout: [],
        septembre: [],
        octobre: [],
        novembre: [],
        decembre: [],
      },
    };
  },
  methods: {
    parseDonnees(donneesStation) {
      let donnees = donneesStation.split("\n").slice(2);
      let parsedDonnees = [];

      parsedDonnees = donnees.map((donnee) => {
        donnee = donnee.split(",");
        /*.join("")
          .split('"')
          .filter((x) => x != "");*/

        return {
          longX: (donnee[0] ?? "").replaceAll('"', ""),
          longY: (donnee[1] ?? "").replaceAll('"', ""),
          name: (donnee[2] ?? "").replaceAll('"', ""),
          climateId: (donnee[3] ?? "").replaceAll('"', ""),
          dateTime: (donnee[4] ?? "").replaceAll('"', ""),
          year: (donnee[5] ?? "").replaceAll('"', ""),
          month: (donnee[6] ?? "").replaceAll('"', ""),
          meanMaxTempCelsius: (donnee[7] ?? "").replaceAll('"', ""),
          meanMaxTempFlag: (donnee[8] ?? "").replaceAll('"', ""),
          meanMinTempCelsius: (donnee[9] ?? "").replaceAll('"', ""),
          meanMinTempFlag: (donnee[10] ?? "").replaceAll('"', ""),
          meanTempCelsius: (donnee[11] ?? "").replaceAll('"', ""),
          meanTempFlag: (donnee[12] ?? "").replaceAll('"', ""),
          extrMaxTempCelsius: (donnee[13] ?? "").replaceAll('"', ""),
          extrMaxTempFlag: (donnee[14] ?? "").replaceAll('"', ""),
          extrMinTempCelsius: (donnee[15] ?? "").replaceAll('"', ""),
          extrMinTempFlag: (donnee[16] ?? "").replaceAll('"', ""),
          totalRainMillimeter: (donnee[17] ?? "").replaceAll('"', ""),
          totalRainFlag: (donnee[18] ?? "").replaceAll('"', ""),
          totalSnowCentimeter: (donnee[19] ?? "").replaceAll('"', ""),
          totalSnowFlag: (donnee[20] ?? "").replaceAll('"', ""),
          totalPrecipMillimeter: (donnee[21] ?? "").replaceAll('"', ""),
          totalPrecipFlag: (donnee[22] ?? "").replaceAll('"', ""),
          snowGrndLastDayCentimeter: (donnee[23] ?? "").replaceAll('"', ""),
          snowGrndLastDayFlag: (donnee[24] ?? "").replaceAll('"', ""),
          directionMaxGust10deg: (donnee[25] ?? "").replaceAll('"', ""),
          directionMaxGustFlag: (donnee[26] ?? "").replaceAll('"', ""),
          speedMaxGustKM_H: (donnee[27] ?? "").replaceAll('"', ""),
          speedMaxGustFlag: (donnee[28] ?? "").replaceAll('"', ""),
        };
      });

      return parsedDonnees;
    },
    refreshDonnees() {
      // Données Météo
      this.donneesMeteo = this.donnees.filter((d) => {
        if (!isNaN(d.year)) {
          let year = parseInt(d.year);

          if (!isNaN(d.month)) {
            let month = parseInt(d.month);

            return (
              year >= this.dates.deAnnee &&
              year <= this.dates.aAnnee &&
              month >= this.dates.deMois &&
              month <= this.dates.aMois
            );
          }
        }
      });

      // Statistiques Globales
      this.statistiquesGlobales.janvier = this.getStatsByMonth("01");
      this.statistiquesGlobales.fevrier = this.getStatsByMonth("02");
      this.statistiquesGlobales.mars = this.getStatsByMonth("03");
      this.statistiquesGlobales.avril = this.getStatsByMonth("04");
      this.statistiquesGlobales.mai = this.getStatsByMonth("05");
      this.statistiquesGlobales.juin = this.getStatsByMonth("06");
      this.statistiquesGlobales.juillet = this.getStatsByMonth("07");
      this.statistiquesGlobales.aout = this.getStatsByMonth("08");
      this.statistiquesGlobales.septembre = this.getStatsByMonth("09");
      this.statistiquesGlobales.octobre = this.getStatsByMonth("10");
      this.statistiquesGlobales.novembre = this.getStatsByMonth("11");
      this.statistiquesGlobales.decembre = this.getStatsByMonth("12");
      this.statistiquesGlobales.globale = this.getGlobalStats();
    },
    getGlobalStats() {
      let stats = [
        ...this.statistiquesGlobales.janvier,
        ...this.statistiquesGlobales.fevrier,
        ...this.statistiquesGlobales.mars,
        ...this.statistiquesGlobales.avril,
        ...this.statistiquesGlobales.mai,
        ...this.statistiquesGlobales.juin,
        ...this.statistiquesGlobales.juillet,
        ...this.statistiquesGlobales.aout,
        ...this.statistiquesGlobales.septembre,
        ...this.statistiquesGlobales.octobre,
        ...this.statistiquesGlobales.novembre,
        ...this.statistiquesGlobales.decembre,
      ];

      // Température moyenne mensuelle
      let tempMoyMax = stats.find(
        (s) =>
          s.maxValue ==
          Math.max(...stats.filter((s) => s.id == 1).map((s) => s.maxValue))
      );
      let tempMoyMin = stats.find(
        (s) =>
          s.minValue ==
          Math.min(...stats.filter((s) => s.id == 1).map((s) => s.minValue))
      );

      // Température extrême
      let tempExtrMax = stats.find(
        (s) =>
          s.maxValue ==
          Math.max(...stats.filter((s) => s.id == 2).map((s) => s.maxValue))
      );
      let tempExtrMin = stats.find(
        (s) =>
          s.minValue ==
          Math.min(...stats.filter((s) => s.id == 2).map((s) => s.minValue))
      );

      // Quantité de pluie
      let totalRainMax = stats.find(
        (s) =>
          s.maxValue ==
          Math.max(...stats.filter((s) => s.id == 3).map((s) => s.maxValue))
      );
      let totalRainMin = stats.find(
        (s) =>
          s.minValue ==
          Math.min(...stats.filter((s) => s.id == 3).map((s) => s.minValue))
      );

      // Quantité de neige
      let totalSnowMax = stats.find(
        (s) =>
          s.maxValue ==
          Math.max(...stats.filter((s) => s.id == 4).map((s) => s.maxValue))
      );
      let totalSnowMin = stats.find(
        (s) =>
          s.minValue ==
          Math.min(...stats.filter((s) => s.id == 4).map((s) => s.minValue))
      );

      // Vitesse du vent
      let speedGustMax = stats.find(
        (s) =>
          s.maxValue ==
          Math.max(...stats.filter((s) => s.id == 5).map((s) => s.maxValue))
      );
      let speedGustMin = stats.find(
        (s) =>
          s.minValue ==
          Math.min(...stats.filter((s) => s.id == 5).map((s) => s.minValue))
      );

      return [
        {
          id: tempMoyMax.id,
          title: tempMoyMax.title,
          unit: tempMoyMax.unit,
          maxValue: tempMoyMax.maxValue,
          maxValueWUnit: tempMoyMax.maxValueWUnit,
          maxYear: tempMoyMax.maxYear,
          maxMonth: tempMoyMax.month,
          minValue: tempMoyMin.minValue,
          minValueWUnit: tempMoyMin.minValueWUnit,
          minYear: tempMoyMin.minYear,
          minMonth: tempMoyMin.month,
        },
        {
          id: tempExtrMax.id,
          title: tempExtrMax.title,
          unit: tempExtrMax.unit,
          maxValue: tempExtrMax.maxValue,
          maxValueWUnit: tempExtrMax.maxValueWUnit,
          maxYear: tempExtrMax.maxYear,
          maxMonth: tempExtrMax.month,
          minValue: tempExtrMin.minValue,
          minValueWUnit: tempExtrMin.minValueWUnit,
          minYear: tempExtrMin.minYear,
          minMonth: tempExtrMin.month,
        },
        {
          id: totalRainMax.id,
          title: totalRainMax.title,
          unit: totalRainMax.unit,
          maxValue: totalRainMax.maxValue,
          maxValueWUnit: totalRainMax.maxValueWUnit,
          maxYear: totalRainMax.maxYear,
          maxMonth: totalRainMax.month,
          minValue: totalRainMin.minValue,
          minValueWUnit: totalRainMin.minValueWUnit,
          minYear: totalRainMin.minYear,
          minMonth: totalRainMin.month,
        },
        {
          id: totalSnowMax.id,
          title: totalSnowMax.title,
          unit: totalSnowMax.unit,
          maxValue: totalSnowMax.maxValue,
          maxValueWUnit: totalSnowMax.maxValueWUnit,
          maxYear: totalSnowMax.maxYear,
          maxMonth: totalSnowMax.month,
          minValue: totalSnowMin.minValue,
          minValueWUnit: totalSnowMin.minValueWUnit,
          minYear: totalSnowMin.minYear,
          minMonth: totalSnowMin.month,
        },
        {
          id: speedGustMax.id,
          title: speedGustMax.title,
          unit: speedGustMax.unit,
          maxValue: speedGustMax.maxValue,
          maxValueWUnit: speedGustMax.maxValueWUnit,
          maxYear: speedGustMax.maxYear,
          maxMonth: speedGustMax.month,
          minValue: speedGustMin.minValue,
          minValueWUnit: speedGustMin.minValueWUnit,
          minYear: speedGustMin.minYear,
          minMonth: speedGustMin.month,
        },
      ];
    },
    getStatsByMonth(monthNumber) {
      // Could this had been done better? Yes. It's definitely too clustered and repetitive, but does it work nonetheless? Also yes.
      let month = _.cloneDeep(
        this.donneesMeteo.filter((d) => {
          return d.month == monthNumber || d.month == parseInt(monthNumber);
        })
      );

      if (month.length > 0) {
        month = month.map((d) => {
          d.meanMaxTempCelsius = parseFloat(d.meanMaxTempCelsius);
          d.meanMinTempCelsius = parseFloat(d.meanMinTempCelsius);
          d.extrMaxTempCelsius = parseFloat(d.extrMaxTempCelsius);
          d.extrMinTempCelsius = parseFloat(d.extrMinTempCelsius);
          d.totalRainMillimeter = parseFloat(d.totalRainMillimeter);
          d.totalSnowCentimeter = parseFloat(d.totalSnowCentimeter);
          d.speedMaxGustKM_H = parseFloat(d.speedMaxGustKM_H);
          return d;
        });

        // Température moyenne mensuelle
        var tempMoyMax = month.find(
          (d) =>
            d.meanMaxTempCelsius ==
            Math.max(
              ...month
                .filter((d) => !isNaN(d.meanMaxTempCelsius))
                .map((d) => d.meanMaxTempCelsius)
            )
        );
        var tempMoyMin = month.find(
          (d) =>
            d.meanMinTempCelsius ==
            Math.min(
              ...month
                .filter((d) => !isNaN(d.meanMinTempCelsius))
                .map((d) => d.meanMinTempCelsius)
            )
        );

        // Température extrême
        var tempExtrMax = month.find(
          (d) =>
            d.extrMaxTempCelsius ==
            Math.max(
              ...month
                .filter((d) => !isNaN(d.extrMaxTempCelsius))
                .map((d) => d.extrMaxTempCelsius)
            )
        );
        var tempExtrMin = month.find(
          (d) =>
            d.extrMinTempCelsius ==
            Math.min(
              ...month
                .filter((d) => !isNaN(d.extrMinTempCelsius))
                .map((d) => d.extrMinTempCelsius)
            )
        );

        // Quantité de pluie
        var totalRainMax = month.find(
          (d) =>
            d.totalRainMillimeter ==
            Math.max(
              ...month
                .filter((d) => !isNaN(d.totalRainMillimeter))
                .map((d) => d.totalRainMillimeter)
            )
        );

        var totalRainMin = month.find(
          (d) =>
            d.totalRainMillimeter ==
            Math.min(
              ...month
                .filter((d) => !isNaN(d.totalRainMillimeter))
                .map((d) => d.totalRainMillimeter)
            )
        );

        // Quantité de neige
        var totalSnowMax = month.find(
          (d) =>
            d.totalSnowCentimeter ==
            Math.max(
              ...month
                .filter((d) => !isNaN(d.totalSnowCentimeter))
                .map((d) => d.totalSnowCentimeter)
            )
        );

        var totalSnowMin = month.find(
          (d) =>
            d.totalSnowCentimeter ==
            Math.min(
              ...month
                .filter((d) => !isNaN(d.totalSnowCentimeter))
                .map((d) => d.totalSnowCentimeter)
            )
        );

        // Vitesse du vent
        var speedGustMax = month.find(
          (d) =>
            d.speedMaxGustKM_H ==
            Math.max(
              ...month
                .filter((d) => !isNaN(d.speedMaxGustKM_H))
                .map((d) => d.speedMaxGustKM_H)
            )
        );

        var speedGustMin = month.find(
          (d) =>
            d.speedMaxGustKM_H ==
            Math.min(
              ...month
                .filter((d) => !isNaN(d.speedMaxGustKM_H))
                .map((d) => d.speedMaxGustKM_H)
            )
        );
      }

      return [
        {
          id: 1,
          month: monthNumber,
          title: "Température moyenne mensuelle",
          unit: "°C",
          maxValue: tempMoyMax?.meanMaxTempCelsius ?? "",
          maxValueWUnit: (tempMoyMax?.meanMaxTempCelsius ?? "-") + " °C",
          maxYear: tempMoyMax?.year ?? "-",
          minValue: tempMoyMin?.meanMinTempCelsius ?? "",
          minValueWUnit: (tempMoyMin?.meanMinTempCelsius ?? "-") + " °C",
          minYear: tempMoyMin?.year ?? "-",
        },
        {
          id: 2,
          month: monthNumber,
          title: "Température extrême",
          unit: "°C",
          maxValue: tempExtrMax?.extrMaxTempCelsius ?? "",
          maxValueWUnit: (tempExtrMax?.extrMaxTempCelsius ?? "-") + " °C",
          maxYear: tempExtrMax?.year ?? "-",
          minValue: tempExtrMin?.extrMinTempCelsius ?? "",
          minValueWUnit: (tempExtrMin?.extrMinTempCelsius ?? "-") + " °C",
          minYear: tempExtrMin?.year ?? "-",
        },
        {
          id: 3,
          month: monthNumber,
          title: "Quantité de pluie",
          unit: "mm",
          maxValue: totalRainMax?.totalRainMillimeter ?? "",
          maxValueWUnit: (totalRainMax?.totalRainMillimeter ?? "-") + " mm",
          maxYear: totalRainMax?.year ?? "-",
          minValue: totalRainMin?.totalRainMillimeter ?? "",
          minValueWUnit: (totalRainMin?.totalRainMillimeter ?? "-") + " mm",
          minYear: totalRainMin?.year ?? "-",
        },
        {
          id: 4,
          month: monthNumber,
          title: "Quantité de neige",
          unit: "cm",
          maxValue: totalSnowMax?.totalSnowCentimeter ?? "",
          maxValueWUnit: (totalSnowMax?.totalSnowCentimeter ?? "-") + " cm",
          maxYear: totalSnowMax?.year ?? "-",
          minValue: totalSnowMin?.totalSnowCentimeter ?? "",
          minValueWUnit: (totalSnowMin?.totalSnowCentimeter ?? "-") + " cm",
          minYear: totalSnowMin?.year ?? "-",
        },
        {
          id: 5,
          month: monthNumber,
          title: "Vitesse du vent",
          unit: "km/h",
          maxValue: speedGustMax?.speedMaxGustKM_H ?? "",
          maxValueWUnit: (speedGustMax?.speedMaxGustKM_H ?? "-") + " km/h",
          maxYear: speedGustMax?.year ?? "-",
          minValue: speedGustMin?.speedMaxGustKM_H ?? "",
          minValueWUnit: (speedGustMin?.speedMaxGustKM_H ?? "-") + " km/h",
          minYear: speedGustMin?.year ?? "-",
        },
      ];
    },
    populateDates() {
      // Années
      let anneesArr = Array.from(
        new Set(
          this.donnees.map((d) => {
            if (!isNaN(d.year)) return parseInt(d.year);
          })
        )
      )
        .filter((d) => d)
        .sort((a, b) => a - b);

      this.dates.aAnnee = this.maxAnnee = anneesArr[anneesArr.length - 1];
      this.dates.deAnnee = this.minAnnee = anneesArr[0];

      let amountOfYears = this.maxAnnee - this.minAnnee + 1;
      this.annees = Array(amountOfYears)
        .fill()
        .map((_, i) => this.minAnnee + i);

      // Mois
      let moisArr = Array.from(
        new Set(
          this.donnees.map((d) => {
            if (!isNaN(d.month)) return parseInt(d.month);
          })
        )
      )
        .filter((d) => d)
        .sort((a, b) => a - b);

      this.dates.aMois = this.maxMois = moisArr[moisArr.length - 1];
      this.dates.deMois = this.minMois = moisArr[0];

      let amountOfMonths = this.maxMois - this.minMois + 1;
      let tmpMois = Array(amountOfMonths)
        .fill()
        .map((_, i) => this.minMois + i);

      this.mois = tmpMois.map((m) => {
        return { value: m, title: this.toMonthName(m) };
      });
    },
    defaultDates() {
      if (
        this.maxAnnee != null &&
        this.minAnnee != null &&
        this.maxMois != null &&
        this.minMois != null
      ) {
        this.dates.aAnnee = this.maxAnnee;
        this.dates.deAnnee = this.minAnnee;
        this.dates.aMois = this.maxMois;
        this.dates.deMois = this.minMois;
      }
    },
    toMonthName(monthNumber) {
      const date = new Date();
      date.setMonth(monthNumber - 1);

      return date.toLocaleString("fr-CA", {
        month: "long",
      });
    },
  },
};
</script>

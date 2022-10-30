<template>
  <v-container>
    <v-row class="ma-auto">
      <v-col style="display: block">
        <table
          id="statistiquesTable"
          class="display compact"
          style="width: 100%"
        >
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Mois</th>
              <th>Valeur min</th>
              <th>Année</th>
              <th>Mois</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Janvier</h3>
        <table id="janvierTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Février</h3>
        <table id="fevrierTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Mars</h3>
        <table id="marsTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Avril</h3>
        <table id="avrilTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Mai</h3>
        <table id="maiTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Juin</h3>
        <table id="juinTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Juillet</h3>
        <table id="juilletTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Août</h3>
        <table id="aoutTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Septembre</h3>
        <table id="septembreTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Octobre</h3>
        <table id="octobreTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Novembre</h3>
        <table id="novembreTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>

        <h3 class="pb-2 pt-6">Décembre</h3>
        <table id="decembreTable" class="display compact" style="width: 100%">
          <thead>
            <tr>
              <th>Donnée</th>
              <th>Valeur max</th>
              <th>Année</th>
              <th>Valeur min</th>
              <th>Année</th>
            </tr>
          </thead>
        </table>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  name: "StatistiquesStation",
  props: {
    donnees: {
      type: Object,
    },
  },
  watch: {
    donnees: {
      handler: function () {
        $("#statistiquesTable").DataTable().destroy();
        $("#statistiquesTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.globale,
          columns: this.globalColumns,
        });
        $("#janvierTable").DataTable().destroy();
        $("#janvierTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.janvier,
          columns: this.monthColumns,
        });
        $("#fevrierTable").DataTable().destroy();
        $("#fevrierTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.fevrier,
          columns: this.monthColumns,
        });
        $("#marsTable").DataTable().destroy();
        $("#marsTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.mars,
          columns: this.monthColumns,
        });
        $("#avrilTable").DataTable().destroy();
        $("#avrilTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.avril,
          columns: this.monthColumns,
        });
        $("#maiTable").DataTable().destroy();
        $("#maiTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.mai,
          columns: this.monthColumns,
        });
        $("#juinTable").DataTable().destroy();
        $("#juinTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.juin,
          columns: this.monthColumns,
        });
        $("#juilletTable").DataTable().destroy();
        $("#juilletTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.juillet,
          columns: this.monthColumns,
        });
        $("#aoutTable").DataTable().destroy();
        $("#aoutTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.aout,
          columns: this.monthColumns,
        });
        $("#septembreTable").DataTable().destroy();
        $("#septembreTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.septembre,
          columns: this.monthColumns,
        });
        $("#octobreTable").DataTable().destroy();
        $("#octobreTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.octobre,
          columns: this.monthColumns,
        });
        $("#novembreTable").DataTable().destroy();
        $("#novembreTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.novembre,
          columns: this.monthColumns,
        });
        $("#decembreTable").DataTable().destroy();
        $("#decembreTable").DataTable({
          bInfo: false,
          searching: false,
          paging: false,
          ordering: false,
          data: this.donnees.decembre,
          columns: this.monthColumns,
        });
      },
      deep: true,
    },
  },
  data() {
    return {
      globalColumns: [
        { data: "title" },
        { data: "maxValueWUnit" },
        { data: "maxYear" },
        { data: "maxMonth" },
        { data: "minValueWUnit" },
        { data: "minYear" },
        { data: "minMonth" },
      ],
      monthColumns: [
        { data: "title" },
        { data: "maxValueWUnit" },
        { data: "maxYear" },
        { data: "minValueWUnit" },
        { data: "minYear" },
      ],
    };
  },
  mounted() {
    $("#statistiquesTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.globale,
      columns: this.globalColumns,
    });
    $("#janvierTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.janvier,
      columns: this.monthColumns,
    });
    $("#fevrierTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.fevrier,
      columns: this.monthColumns,
    });
    $("#marsTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.mars,
      columns: this.monthColumns,
    });
    $("#avrilTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.avril,
      columns: this.monthColumns,
    });
    $("#maiTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.mai,
      columns: this.monthColumns,
    });
    $("#juinTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.juin,
      columns: this.monthColumns,
    });
    $("#juilletTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.juillet,
      columns: this.monthColumns,
    });
    $("#aoutTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.aout,
      columns: this.monthColumns,
    });
    $("#septembreTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.septembre,
      columns: this.monthColumns,
    });
    $("#octobreTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.octobre,
      columns: this.monthColumns,
    });
    $("#novembreTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.novembre,
      columns: this.monthColumns,
    });
    $("#decembreTable").DataTable({
      bInfo: false,
      searching: false,
      paging: false,
      ordering: false,
      data: this.donnees.decembre,
      columns: this.monthColumns,
    });
  },
};
</script>

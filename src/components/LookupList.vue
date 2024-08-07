<template>
    <div>
      <h1>Lookup List</h1>
      <table>
        <thead>
          <tr>
            <th>Lookup ID</th>
            <th>Lookup Description</th>
            <th>Lookup Type</th>
            <th>Is Active</th>
            <th>Created By</th>
            <th>Created Date</th>
            <th>Updated By</th>
            <th>Updated Date</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="lookup in lookups" :key="lookup.Lookup_Id">
            <td>{{ lookup.Lookup_Id }}</td>
            <td>{{ lookup.Lookup_Desc }}</td>
            <td>{{ lookup.Lookup_Type }}</td>
            <td>{{ lookup.Is_Active }}</td>
            <td>{{ lookup.CreatedBy }}</td>
            <td>{{ new Date(lookup.CreatedDate).toLocaleString() }}</td>
            <td>{{ lookup.UpdatedBy }}</td>
            <td>{{ lookup.UpdatedDate ? new Date(lookup.UpdatedDate).toLocaleString() : 'N/A' }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>

<script>
 
import api from '../APIs/Lookupapi';

export default {
  data() {
    return {
      lookups: []
    };
  },
  async created() {
    try {
    //   const response = await axios.get('http://localhost:8080/LookupService/GetLookups');
      
    //this.lookups = response.data;
    this.lookups = await api.getLookup('http://localhost:5097/LookupService/GetLookups');
    } catch (error) {
      console.error('Error fetching lookups:', error);
    }
  }
};
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

th {
  background-color: #f2f2f2;
}

tr:nth-child(even) {
  background-color: #f9f9f9;
}

tr:hover {
  background-color: #f1f1f1;
}
</style>

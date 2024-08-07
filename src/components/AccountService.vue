<template>
  <div>
    <h1>Account List</h1>
    <table>
      <thead>
        <tr>
          <th>Acc ID</th>
          <th>Account Number</th>
          <th>Account Status ID</th>
          <th>Created Date</th>
          <th>Created By</th>
          <th>Updated Date</th>
          <th>Updated By</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="account in accounts" :key="account.AccId">
          <td>{{ account.AccId }}</td>
          <td>{{ account.AccountNumber }}</td>
          <td>{{ account.AccountStatus_id }}</td>
          <td>{{ account.CreatedDate }}</td>
          <td>{{ account.CreatedBy }}</td>
          <td>{{ account.UpdatedDate }}</td>
          <td>{{ account.UpdatedBy }}</td>
        </tr>
      </tbody>
    </table>
    <!-- Add forms or buttons for adding/updating/deleting accounts as needed -->
  </div>
</template>

<script>
import accountApi from '../APIs/accountApi';

export default {
  data() {
    return {
      accounts: [],
    };
  },
  async created() {
    try {
      this.accounts = await accountApi.getAccounts();
    } catch (error) {
      console.error('Error loading accounts:', error);
    }
  },
};
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  padding: 8px 12px;
  border: 1px solid #ddd;
}

th {
  background-color: #f4f4f4;
  text-align: left;
}

tr:nth-child(even) {
  background-color: #f9f9f9;
}

h1 {
  margin-bottom: 20px;
}
</style>

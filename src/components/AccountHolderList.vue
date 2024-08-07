<template>
  <div class="account-holders">
    <h1>Account Holders</h1>
    
    <!-- Account Holders List -->
    <div class="account-list">
      <ul>
        <li v-for="accountHolder in accountHolders" :key="accountHolder.AccHID" class="account-item">
          <div class="account-details">
            {{ accountHolder.Acc_holders_N }} ({{ accountHolder.AccNUM }})
          </div>
          <button class="delete-button" @click="deleteAccountHolder(accountHolder.AccHID)">Delete</button>
        </li>
      </ul>
    </div>
    
    <!-- Add Account Holder Form -->
    <div class="add-account-form">
      <h2>Add Account Holder</h2>
      <form @submit.prevent="addAccountHolder">
        <div class="form-group">
          <label for="AccHID">AccHID:</label>
          <input v-model="newAccountHolder.AccHID" id="AccHID" placeholder="AccHID" />
        </div>
        <div class="form-group">
          <label for="AccNUM">AccNUM:</label>
          <input v-model="newAccountHolder.AccNUM" id="AccNUM" placeholder="AccNUM" />
        </div>
        <div class="form-group">
          <label for="AccTypeID">AccTypeID:</label>
          <input v-model="newAccountHolder.AccTypeID" id="AccTypeID" placeholder="AccTypeID" />
        </div>
        <div class="form-group">
          <label for="Acc_holders_N">Account Holder Name:</label>
          <input v-model="newAccountHolder.Acc_holders_N" id="Acc_holders_N" placeholder="Acc_holders_N" />
        </div>
        <div class="form-group">
          <label for="AC_Balance">Account Balance:</label>
          <input v-model="newAccountHolder.AC_Balance" id="AC_Balance" placeholder="AC_Balance" />
        </div>
        <div class="form-group">
          <label for="CD">CD:</label>
          <input v-model="newAccountHolder.CD" id="CD" placeholder="CD" />
        </div>
        <button type="submit" class="submit-button">Add</button>
      </form>
    </div>
  </div>
</template>

<script>
import AccountHolderService from '../APIs/AccountHolderServiceApi';

export default {
  data() {
    return {
      accountHolders: [],
      newAccountHolder: {
        AccHID: '',
        AccNUM: '',
        AccTypeID: '',
        Acc_holders_N: '',
        AC_Balance: '',
        CD: ''
      }
    };
  },
  async created() {
    this.accountHolders = await AccountHolderService.getAccountHolders();
  },
  methods: {
    async addAccountHolder() {
      await AccountHolderService.addAccountHolder(this.newAccountHolder);
      this.accountHolders = await AccountHolderService.getAccountHolders();
      this.resetForm();
    },
    async deleteAccountHolder(id) {
      await AccountHolderService.deleteAccountHolder(id);
      this.accountHolders = await AccountHolderService.getAccountHolders();
    },
    resetForm() {
      this.newAccountHolder = {
        AccHID: '',
        AccNUM: '',
        AccTypeID: '',
        Acc_holders_N: '',
        AC_Balance: '',
        CD: ''
      };
    }
  }
};
</script>

<style scoped>
.account-holders {
  padding: 20px;
}

.account-list {
  margin-bottom: 20px;
}

.account-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.account-details {
  font-size: 16px;
}

.delete-button {
  background-color: red;
  color: white;
  border: none;
  padding: 5px 10px;
  border-radius: 3px;
  cursor: pointer;
}

.add-account-form {
  background-color: #f9f9f9;
  padding: 20px;
  border-radius: 5px;
}

.add-account-form h2 {
  margin-top: 0;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
}

.form-group input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 3px;
}

.submit-button {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 10px 15px;
  border-radius: 3px;
  cursor: pointer;
}
</style>

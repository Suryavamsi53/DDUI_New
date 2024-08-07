<template>
  <div class="transactions">
    <h1>Transactions</h1>
    
    <!-- Transaction List -->
    <div class="transaction-list">
      <ul>
        <li v-for="transaction in transactions" :key="transaction.TransID" class="transaction-item">
          <div class="transaction-details">
            <span class="transaction-type">{{ transaction.Transaction_type }}</span>: 
            <span class="transaction-amount">{{ transaction.Amount }}</span> 
            on <span class="transaction-date">{{ transaction.Date }}</span>
          </div>
          <button class="delete-button" @click="deleteTransaction(transaction.TransID)">Delete</button>
        </li>
      </ul>
    </div>
    
    <!-- Add Transaction Form -->
    <div class="add-transaction-form">
      <h2>Add Transaction</h2>
      <form @submit.prevent="addTransaction">
        <div class="form-group">
          <label for="TransID">TransID:</label>
          <input v-model="newTransaction.TransID" id="TransID" placeholder="TransID" />
        </div>
        <div class="form-group">
          <label for="AccountID">AccountID:</label>
          <input v-model="newTransaction.AccountID" id="AccountID" placeholder="AccountID" />
        </div>
        <div class="form-group">
          <label for="TransTypeID">TransTypeID:</label>
          <input v-model="newTransaction.TransTypeID" id="TransTypeID" placeholder="TransTypeID" />
        </div>
        <div class="form-group">
          <label for="Amount">Amount:</label>
          <input v-model="newTransaction.Amount" id="Amount" placeholder="Amount" />
        </div>
        <div class="form-group">
          <label for="Date">Date:</label>
          <input v-model="newTransaction.Date" id="Date" placeholder="Date" />
        </div>
        <div class="form-group">
          <label for="Transaction_type">Transaction Type:</label>
          <input v-model="newTransaction.Transaction_type" id="Transaction_type" placeholder="Transaction_type" />
        </div>
        <button type="submit" class="submit-button">Add</button>
      </form>
    </div>
  </div>
</template>

<script>
import TransactionService from '../APIs/TransactionServiceapi'

export default {
  data() {
    return {
      transactions: [],
      newTransaction: {
        TransID: '',
        AccountID: '',
        TransTypeID: '',
        Amount: '',
        Date: '',
        Transaction_type: ''
      }
    };
  },
  async created() {
    this.transactions = await TransactionService.getTransactions();
  },
  methods: {
    async addTransaction() {
      await TransactionService.addTransaction(this.newTransaction);
      this.transactions = await TransactionService.getTransactions();
      this.resetForm();
    },
    async deleteTransaction(id) {
      await TransactionService.deleteTransaction(id);
      this.transactions = await TransactionService.getTransactions();
    },
    resetForm() {
      this.newTransaction = {
        TransID: '',
        AccountID: '',
        TransTypeID: '',
        Amount: '',
        Date: '',
        Transaction_type: ''
      };
    }
  }
};
</script>

<style scoped>
.transactions {
  padding: 20px;
}

.transaction-list {
  margin-bottom: 20px;
}

.transaction-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.transaction-details {
  font-size: 16px;
}

.transaction-type {
  font-weight: bold;
}

.transaction-amount {
  color: green;
}

.transaction-date {
  color: gray;
}

.delete-button {
  background-color: red;
  color: white;
  border: none;
  padding: 5px 10px;
  border-radius: 3px;
  cursor: pointer;
}

.add-transaction-form {
  background-color: #f9f9f9;
  padding: 20px;
  border-radius: 5px;
}

.add-transaction-form h2 {
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

import axios from 'axios';

const API_URL = 'http://localhost:5097/TransactionService';

export default {
  async getTransactions() {
    const response = await axios.get(`${API_URL}/GetTransactionsAsync`);
    return response.data;
  },

  async getTransaction(id) {
    const response = await axios.get(`${API_URL}/GetTransactionAsync`, { params: { id } });
    return response.data;
  },

  async addTransaction(transaction) {
    await axios.post(`${API_URL}/AddTransactionAsync`, transaction);
  },

  async updateTransaction(transaction) {
    await axios.put(`${API_URL}/UpdateTransactionAsync`, transaction);
  },

  async deleteTransaction(id) {
    await axios.delete(`${API_URL}/DeleteTransactionAsync`, { params: { id } });
  }
};

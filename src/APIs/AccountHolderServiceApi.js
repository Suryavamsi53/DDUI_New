import axios from 'axios';

const API_URL = 'http://localhost:5097/AccountHolderService';

export default {
  async getAccountHolders() {
    const response = await axios.get(`${API_URL}/GetAccountHoldersAsync`);
    return response.data;
  },

  async getAccountHolder(id) {
    const response = await axios.get(`${API_URL}/GetAccountHolderAsync`, { params: { id } });
    return response.data;
  },

  async addAccountHolder(accountHolder) {
    await axios.post(`${API_URL}/AddAccountHolderAsync`, accountHolder);
  },

  async updateAccountHolder(accountHolder) {
    await axios.put(`${API_URL}/UpdateAccountHolderAsync`, accountHolder);
  },

  async deleteAccountHolder(id) {
    await axios.delete(`${API_URL}/DeleteAccountHolderAsync`, { params: { id } });
  }
};

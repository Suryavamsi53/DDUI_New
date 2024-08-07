import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5097/AccountService.svc',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getAccounts() {
    try {
      const response = await apiClient.get('/GetAccounts');
      return response.data;
    } catch (error) {
      console.error('Error fetching accounts:', error);
      throw error;
    }
  },
  async getAccount(id) {
    try {
      const response = await apiClient.get(`/GetAccount/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching account with ID ${id}:`, error);
      throw error;
    }
  },
  async addAccount(account) {
    try {
      const response = await apiClient.post('/AddAccount', account);
      return response.data;
    } catch (error) {
      console.error('Error adding account:', error);
      throw error;
    }
  },
  async updateAccount(account) {
    try {
      const response = await apiClient.put('/UpdateAccount', account);
      return response.data;
    } catch (error) {
      console.error('Error updating account:', error);
      throw error;
    }
  },
  async deleteAccount(id) {
    try {
      const response = await apiClient.delete(`/DeleteAccount/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error deleting account with ID ${id}:`, error);
      throw error;
    }
  },
};




// import axios from 'axios';

// const API_URL = 'http://localhost:5000/AccountService';

// export default {
//   async getAccounts() {
//     const response = await axios.get(`${API_URL}/GetAccountsAsync`);
//     return response.data;
//   },

//   async getAccount(id) {
//     const response = await axios.get(`${API_URL}/GetAccountAsync`, { params: { id } });
//     return response.data;
//   },

//   async addAccount(account) {
//     await axios.post(`${API_URL}/AddAccountAsync`, account);
//   },

//   async updateAccount(account) {
//     await axios.put(`${API_URL}/UpdateAccountAsync`, account);
//   },

//   async deleteAccount(id) {
//     await axios.delete(`${API_URL}/DeleteAccountAsync`, { params: { id } });
//   }
// };

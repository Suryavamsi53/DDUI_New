import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5097/AddressService.svc',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getAddresses() {
    try {
      const response = await apiClient.get('/GetAddresses');
      return response.data;
    } catch (error) {
      console.error('Error fetching addresses:', error);
      throw error;
    }
  },
  async getAddress(id) {
    try {
      const response = await apiClient.get(`/GetAddress/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching address with ID ${id}:`, error);
      throw error;
    }
  },
  async addAddress(address) {
    try {
      const response = await apiClient.post('/AddAddress', address);
      return response.data;
    } catch (error) {
      console.error('Error adding address:', error);
      throw error;
    }
  },
  async updateAddress(address) {
    try {
      const response = await apiClient.put('/UpdateAddress', address);
      return response.data;
    } catch (error) {
      console.error('Error updating address:', error);
      throw error;
    }
  },
  async deleteAddress(id) {
    try {
      const response = await apiClient.delete(`/DeleteAddress/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error deleting address with ID ${id}:`, error);
      throw error;
    }
  },
};

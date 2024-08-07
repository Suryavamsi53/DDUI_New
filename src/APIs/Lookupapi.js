import axios from 'axios';

// Create an axios instance with default configuration
const apiClient = axios.create({
  baseURL: 'http://localhost:5097/LookupService.svc', // Ensure this matches your WCF service base URL
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  // Function to get all lookups
  async getLookups() {
    try {
      const response = await apiClient.get('/GetLookups');
      return response.data;
    } catch (error) {
      console.error('Error fetching lookups:', error.response ? error.response.data : error.message);
      throw error;
    }
  },

  // Function to get a specific lookup by ID
  async getLookup(id) {
    try {
      const response = await apiClient.get(`/GetLookup/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching lookup with ID ${id}:`, error.response ? error.response.data : error.message);
      throw error;
    }
  },

  // Function to add a new lookup
  async addLookup(lookup) {
    try {
      const response = await apiClient.post('/AddLookup', lookup);
      return response.data;
    } catch (error) {
      console.error('Error adding lookup:', error.response ? error.response.data : error.message);
      throw error;
    }
  },

  // Function to update an existing lookup
  async updateLookup(lookup) {
    try {
      const response = await apiClient.put('/UpdateLookup', lookup);
      return response.data;
    } catch (error) {
      console.error('Error updating lookup:', error.response ? error.response.data : error.message);
      throw error;
    }
  },

  // Function to delete a lookup
  async deleteLookup(id) {
    try {
      const response = await apiClient.delete(`/DeleteLookup/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error deleting lookup with ID ${id}:`, error.response ? error.response.data : error.message);
      throw error;
    }
  },
};

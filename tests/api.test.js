import axios from 'axios';
import { expect } from 'chai';

const apiClient = axios.create({
  baseURL: 'http://localhost:5097/Lookupapi', // Adjust this to your actual API base URL
  headers: {
    'Content-Type': 'application/json',
  },
});

describe('LookupService API', function() {
  // Test for getting all lookups
  it('should get all lookups', async function() {
    try {
      const response = await apiClient.get('/GetLookups');
      console.log('Data received:', response.data);
      expect(response.status).to.equal(200);
      expect(response.data).to.be.an('array'); // Adjust according to your expected response
    } catch (error) {
      console.error('Error:', error.response ? error.response.data : error.message);
      throw new Error(`Failed to fetch lookups: ${error.message}`);
    }
  });

  // Test for getting a specific lookup by ID
  it('should get a specific lookup by ID', async function() {
    const id = 1; // Adjust this as needed
    try {
      const response = await apiClient.get(`/GetLookup/${id}`);
      console.log('Data received:', response.data);
      expect(response.status).to.equal(200);
      expect(response.data).to.be.an('object'); // Adjust according to your expected response
    } catch (error) {
      console.error('Error:', error.response ? error.response.data : error.message);
      throw new Error(`Failed to fetch lookup with ID ${id}: ${error.message}`);
    }
  });

  // Test for adding a new lookup
  it('should add a new lookup', async function() {
    const lookup = {
      Lookup_id: 123,
      Lookup_type: 'Type1',
      Lookup_desc: 'Test Lookup',
      Is_active: 'Y',
      Createdby: 'Admin',
      CreatedDATE: new Date().toISOString()
    };
    try {
      const response = await apiClient.post('/AddLookup', lookup);
      console.log('Data received:', response.data);
      expect(response.status).to.equal(200);
    } catch (error) {
      console.error('Error:', error.response ? error.response.data : error.message);
      throw new Error(`Failed to add lookup: ${error.message}`);
    }
  });

  // Test for updating an existing lookup
  it('should update an existing lookup', async function() {
    const lookup = {
      Lookup_id: 1,
      Lookup_type: 'Type2',
      Lookup_desc: 'Updated Lookup',
      Is_active: 'N',
      Updatedby: 'Admin',
      UpdatedDATE: new Date().toISOString()
    };
    try {
      const response = await apiClient.put('/UpdateLookup', lookup);
      console.log('Data received:', response.data);
      expect(response.status).to.equal(200);
    } catch (error) {
      console.error('Error:', error.response ? error.response.data : error.message);
      throw new Error(`Failed to update lookup: ${error.message}`);
    }
  });

  // Test for deleting a lookup
  it('should delete a lookup', async function() {
    const id = 1; // Adjust this as needed
    try {
      const response = await apiClient.delete(`/DeleteLookup/${id}`);
      expect(response.status).to.equal(200);
    } catch (error) {
      console.error('Error:', error.response ? error.response.data : error.message);
      throw new Error(`Failed to delete lookup with ID ${id}: ${error.message}`);
    }
  });
});

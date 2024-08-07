<template>
  <div>
    <h1>Address List</h1>
    <button @click="showAddForm = !showAddForm">{{ showAddForm ? 'Cancel' : 'Add Address' }}</button>
    <div v-if="showAddForm">
      <h2>Add Address</h2>
      <form @submit.prevent="addAddress">
        <label for="address">Address:</label>
        <input v-model="newAddress.address" id="address" required />

        <label for="accountId">Account ID:</label>
        <input v-model="newAddress.accountID" id="accountId" type="number" required />

        <label for="addressTypeId">Address Type ID:</label>
        <input v-model="newAddress.addressTypeID" id="addressTypeId" type="number" required />

        <button type="submit">Add</button>
      </form>
    </div>

    <ul>
      <li v-for="address in addresses" :key="address.addressID">
        {{ address.address }} - {{ address.accountID }} - {{ address.addressTypeID }}
        <button @click="editAddress(address)">Edit</button>
        <button @click="deleteAddress(address.addressID)">Delete</button>
      </li>
    </ul>
  </div>
</template>

<script>
import addressApi from '../APIs/addressApi';

export default {
  data() {
    return {
      addresses: [],
      newAddress: {
        addressID: null,
        accountID: null,
        addressTypeID: null,
        address: '',
      },
      showAddForm: false,
    };
  },
  async created() {
    this.loadAddresses();
  },
  methods: {
    async loadAddresses() {
      try {
        this.addresses = await addressApi.getAddresses();
      } catch (error) {
        console.error('Error loading addresses:', error);
      }
    },
    async addAddress() {
      try {
        await addressApi.addAddress(this.newAddress);
        this.loadAddresses();
        this.resetForm();
      } catch (error) {
        console.error('Error adding address:', error);
      }
    },
    async editAddress() {
      //use address 
      // Implement edit functionality
    },
    async deleteAddress(id) {
      try {
        await addressApi.deleteAddress(id);
        this.loadAddresses();
      } catch (error) {
        console.error('Error deleting address:', error);
      }
    },
    resetForm() {
      this.newAddress = {
        addressID: null,
        accountID: null,
        addressTypeID: null,
        address: '',
      };
      this.showAddForm = false;
    }
  },
};
</script>

<style scoped>
h1 {
  margin-bottom: 20px;
}

button {
  margin-bottom: 20px;
}

form {
  margin-bottom: 20px;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  margin-bottom: 10px;
}

label {
  display: block;
  margin: 10px 0 5px;
}

input {
  margin-bottom: 10px;
  padding: 5px;
  width: 100%;
  box-sizing: border-box;
}
</style>

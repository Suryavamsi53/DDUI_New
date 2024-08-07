<template>
  <div id="app">
    <nav class="sidebar">
      <ul>
        <li @click="currentComponent = 'LookupList'">Lookup List</li>
        <li @click="currentComponent = 'AccountService'">Account Service</li>
        <li @click="currentComponent = 'AddressList'">Address List</li>
        <li @click="currentComponent = 'AccountHolderList'">Account Holder List</li>
        <li @click="currentComponent = 'TransactionList'">Transaction List</li>
      </ul>
    </nav>
    <div class="main-content">
      <component :is="currentComponent"></component>
    </div>
  </div>
</template>

<script>
import LookupList from './components/LookupList.vue';
import AccountService from './components/AccountService.vue';
import AddressList from './components/AddressList.vue';
import AccountHolderList from './components/AccountHolderList.vue';
import TransactionList from './components/TransactionList.vue';

export default {
  name: 'App',
  components: {
    LookupList,
    AccountService,
    AddressList,
    AccountHolderList,
    TransactionList,
  },
  data() {
    return {
      currentComponent: 'LookupList'
    };
  }
}
</script>

<style>
#app {
  display: flex;
}

.sidebar {
  width: 200px;
  height: 100vh;
  background-color: #333;
  color: white;
  display: flex;
  flex-direction: column;
  padding-top: 20px;
  position: fixed;
  transition: width 0.3s ease;
}

.sidebar:hover {
  width: 250px;
}

.sidebar ul {
  list-style-type: none;
  padding: 0;
  margin: 0;
}

.sidebar ul li {
  padding: 15px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.sidebar ul li:hover {
  background-color: #575757;
}

.sidebar ul li:hover {
  background-color: #575757;
}

.main-content {
  margin-left: 200px; /* Match sidebar width */
  padding: 20px;
  width: calc(100% - 200px); /* Adjust width based on sidebar */
  transition: width 0.3s ease;
}

.sidebar:hover ~ .main-content {
  margin-left: 250px; /* Adjust based on expanded sidebar */
  width: calc(100% - 250px);
}
</style>

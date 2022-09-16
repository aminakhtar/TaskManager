<template>
    <div class="card text-center m-3">
      <div class="card-body">Total vue packages: {{totalVuePackages}}</div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: "get-request",
    data() {
      return {
        totalVuePackages: null
      };
    },
    created() {
      // Simple GET request using axios
      let result = axios.get('https://api.coindesk.com/v1/bpi/currentprice.json')
      .then(resp => {
        this.totalVuePackages = resp;
        });
    },
  };
  </script>
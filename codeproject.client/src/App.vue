<template>
  <div id="app">
    <!-- Provider Selection Dropdown -->
    <div v-if="!selectedProvider">
      <h2>Select a Provider</h2>
      <Providers @provider-selected="setProvider" />
    </div>

    <!-- ToDos Component with Search Box, displayed only after provider selection -->
    <div v-else>
      <ToDos :provider="selectedProvider" />
    </div>
  </div>
</template>

<script>
  import Providers from './components/Providers.vue';
  import ToDos from './components/ToDos.vue';

  export default {
    components: {
      Providers,
      ToDos
    },
    data() {
      return {
        selectedProvider: ''
      };
    },
    methods: {
      setProvider(provider) {
        this.selectedProvider = provider;
      }
    }
  };
</script>

<style>
  /* Add your styling here if needed */
</style>

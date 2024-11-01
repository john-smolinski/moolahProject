<template>
  <div>
    <label for="provider-select">Select a Provider:</label>
    <select id="provider-select" v-model="selectedProvider" @change="selectProvider">
      <option value="">Select a provider</option>
      <option v-for="provider in providers" :key="provider.id" :value="provider.name">
        {{ provider.name }}
      </option>
    </select>
  </div>
</template>

<script>
  import providersService from '../services/providersService';

  export default {
    data() {
      return {
        providers: [],
        selectedProvider: ''
      };
    },
    async created() {
      try {
        const response = await providersService.getAllProviders();
        this.providers = response.data;
      } catch (error) {
        console.error('Error fetching providers:', error);
      }
    },
    methods: {
      selectProvider() {
        this.$emit('provider-selected', this.selectedProvider); // Emit the selected provider to parent
      }
    }
  };
</script>

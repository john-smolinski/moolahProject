<template>
  <div>
    <h2>To-Do List for {{ provider }}</h2>

    <label for="search">Search ToDos:</label>
    <input type="text"
           v-model="searchTerm"
           @input="debouncedSearch"
           placeholder="Enter search term" />

    <div v-if="toDos.length">
      <div v-for="toDo in toDos" :key="toDo.id" class="to-do-item">
        <h3>{{ toDo.name }}</h3>
        <p>{{ toDo.description }}</p>
        <button @click="editToDo(toDo)">Edit</button>
        <button @click="deleteToDo(toDo.id)">Delete</button>
      </div>
    </div>
    <p v-else>No to-dos found for the search criteria.</p>
  </div>
</template>

<script>
  import toDoService from '../services/toDoService';

  export default {
    props: {
      provider: {
        type: String,
        required: true
      }
    },
    data() {
      return {
        toDos: [],
        searchTerm: '',
        debounceTimer: null
      };
    },
    created() {
      this.searchToDos();
    },
    methods: {
      debouncedSearch() {
        clearTimeout(this.debounceTimer);
        this.debounceTimer = setTimeout(() => {
          this.searchToDos();
        }, 300); // 300ms delay
      },
      async searchToDos() {
        const searchParams = {
          provider: this.provider,
          search: this.searchTerm
        };

        try {
          const response = await toDoService.searchToDos(searchParams);
          this.toDos = response.data;
        } catch (error) {
          console.error('Error searching to-dos:', error);
          this.toDos = [];
        }
      },
      editToDo(toDo) {
        // Logic to open an edit form or navigate to an edit page
        alert(`Edit ToDo: ${toDo.name}`);
      },
      async deleteToDo(id) {
        if (confirm('Are you sure you want to delete this to-do item?')) {
          try {
            await toDoService.deleteToDo(id, this.provider); // Pass provider as a parameter
            this.toDos = this.toDos.filter(toDo => toDo.id !== id); // Update list after deletion
          } catch (error) {
            console.error('Error deleting to-do:', error);
          }
        }
      }
    }
  };
</script>

<style>
  .to-do-item {
    border: 1px solid #ddd;
    padding: 10px;
    margin: 10px 0;
  }

    .to-do-item h3 {
      margin: 0;
      font-size: 1.2em;
    }

    .to-do-item p {
      margin: 5px 0;
    }

    .to-do-item button {
      margin-right: 5px;
    }
</style>

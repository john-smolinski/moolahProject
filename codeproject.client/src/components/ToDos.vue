<template>
  <div>
    <h2>To-Do List for {{ provider }}</h2>

    <label for="search">Search ToDos:</label>
    <input type="text"
           v-model="searchTerm"
           @input="debouncedSearch"
           placeholder="Enter search term" />

    <!-- Form to Add New To-Do -->
    <div class="add-todo-form">
      <h3>Add New To-Do</h3>
      <input v-model="newToDo.name" placeholder="Enter title" />
      <textarea v-model="newToDo.description" placeholder="Enter description"></textarea>
      <button @click="postToDo">Add To-Do</button>
    </div>

    <div v-if="toDos.length">
      <div v-for="toDo in toDos" :key="toDo.id" class="to-do-item">
        <div v-if="toDo.isEditing">
          <input v-model="toDo.name" placeholder="Edit title" />
          <textarea v-model="toDo.description" placeholder="Edit description"></textarea>
          <button @click="saveEdit(toDo)">Save</button>
          <button @click="cancelEdit(toDo)">Cancel</button>
        </div>
        <div v-else>
          <h3>{{ toDo.name }}</h3>
          <p>{{ toDo.description }}</p>
          <button @click="startEdit(toDo)">Edit</button>
          <button @click="deleteToDo(toDo.id)">Delete</button>
        </div>
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
        debounceTimer: null,
        newToDo: { name: '', description: '' } // Holds data for the new to-do
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
          this.toDos = response.data.map(toDo => ({
            ...toDo,
            isEditing: false // Add isEditing property to each to-do item
          }));
        } catch (error) {
          console.error('Error searching to-dos:', error);
          this.toDos = [];
        }
      },
      async postToDo() {
        const toDoData = {
          name: this.newToDo.name,
          description: this.newToDo.description,
          ProviderName: this.provider // Set ProviderName to the selected provider
        };

        try {
          const response = await toDoService.createToDo(toDoData);
          this.toDos.push(response.data); // Add the new to-do to the list
          this.newToDo.name = ''; // Clear form fields
          this.newToDo.description = '';
        } catch (error) {
          console.error('Error adding to-do:', error);
        }
      },
      startEdit(toDo) {
        toDo.isEditing = true;
      },
      cancelEdit(toDo) {
        toDo.isEditing = false;
        this.searchToDos(); // Reload the original data to discard edits
      },
      async saveEdit(toDo) {
        try {
          const updatePayload = {
            id: toDo.id,
            name: toDo.name,
            description: toDo.description,
            ProviderName: this.provider // Ensure this matches what the backend expects
          };

          await toDoService.updateToDo(toDo.id, updatePayload);
          toDo.isEditing = false;
        } catch (error) {
          console.error('Error saving to-do:', error);
        }
      },
      async deleteToDo(id) {
        if (confirm('Are you sure you want to delete this to-do item?')) {
          try {
            await toDoService.deleteToDo(id, this.provider);
            this.toDos = this.toDos.filter(toDo => toDo.id !== id);
          } catch (error) {
            console.error('Error deleting to-do:', error);
          }
        }
      }
    }
  };
</script>

<style>
  /* Additional styling for the form and todo items */
  .add-todo-form {
    margin: 20px 0;
    padding: 10px;
    border: 1px solid #ddd;
  }

    .add-todo-form h3 {
      margin-top: 0;
    }

    .add-todo-form input,
    .add-todo-form textarea {
      display: block;
      width: 100%;
      margin: 5px 0;
    }

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

# Hogwarts Potions and Houses

## Examples in this project

- Web API in ASP.NET, CRUD operations
- C# and Entity Framework Core
- Code-first approach
- MSSQL Server database connection
- Asynchronous programming

## About the project

### Summary

This project is about managing virtual potions and student rooms on a school server backend, in a very famous fictional universe.  
The project is based on the following rules:

- The basics of the `Potions` class are recipes. By recipes, we can identify potions. A `Recipe` has an `id`, a `name`, a `Student` who brew it and a list of ingredients.
- An `Ingredient` contains an `id` and a `name`.
- A `Potion` has an `id`, `name`, a `Student` who brews it, a list of ingredients, a `BrewingStatus`, and a `Recipe`.
- Until a potion does not contain five ingredients, its `BrewingStatus` is _brew_. After that, if there is already a `Recipe` with the same ingredients (in any order), the status is _replica_. Otherwise, the status is _discovery_.
- If the brew is a _discovery_, the recipe is persisted with the list of ingredients, the student, and with a name generated from the Student's name (e.g. "John Doe's discovery #2").

### Potion Endpoints

- GET `/potions`  
  Lists all potions.
- POST `/potions`  
  The request consists of a potion just brewed, containing the student ID, and the list of ingredients.
  The list of ingredients is checked if it matches any potion. The response contains the persisted potion.
- GET `/potions/{student-id}`  
  Lists all known potions of a student, by the given ID.

### Potion Brewing Endpoints

- POST `/potions/brew`  
  A new potion is generated containing the student and the status of brewing. The response contains the potion, including the ingredients added so far.
- PUT `/potions/{potion-id}/add`
  The potion gets updated with one new ingredient. The response contains the updated potion.
- GET `/potions/{potion-id}/help`  
  Returns the recipes that contain the same ingredients as the potion brewing.

### Room Endpoints

- GET `/rooms/`  
  Lists all rooms.
- POST `/rooms/`  
  Adds a new room.
- GET `/rooms/{id}`  
  Returns the room by the given ID.
- PUT `/rooms/{id}`  
  Modifies the room by the given ID.
- DELETE `/rooms/{id}`  
  Removes the room by the given ID.
- GET `/room/available`  
  Returns a list of all available (empty) rooms.
- GET `/room/rat-owners`  
  Returns a list of rooms, residents of which have no cats or owls.

## Running the project

- Clone the repository
- Build the solution
- Run the solution in Visual Studio with IIS Express

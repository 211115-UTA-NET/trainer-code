# Write the SQL commands to create a set of tables that fulfill the structure outlined below. 
## Your finished work should be turned in following these steps:
- Pull the CodeChallenge from the trainer-code repo.
- Create a new repo called (FirstNameLastInitial)-SQLChallenge as a public repo on the batch organization.
- Clone the new repo to your computer, and copy the CodeChallenge folder from trainer-code to it.
- Using the outline below and the image as an example, write the SQL to meet the objectives listed.
- Save your code as a .sql file, and push it to your new repo.
 
### *Use the PokemonTypes.jpeg image for a graphical example of what your tables should contain.*

#### 1 - Create a table called `Pokemon` that contains...
- an `Id` field that can contain a whole number  
- a `Name` field that can contain a string  
- a `Height` field that can contain a string  
- a `Weight` field that can contain a string  

The `Id` field should increment by one for every new entry, and should be the Primary Key of the table.  
  
#### 2 - Create a table called `Type` that contains...
- an `Id` field that can contain a whole number  
- a `Name` field that can contain a string  

The `Id` field should increment by one for every new entry, and should be the Primary Key of the table.  
The `Name` field should only hold unique values.  
  
#### 3 - Create a table called `PokemonType` that contains...
- an `Id` field that can contain a whole number  
- a `PokemonId` field that can contain a whole number  
- a `TypeId` field that can contain a whole number  

The `Id` field should increment by one for every new entry, and should be the Primary Key of the table.  
The `PokemonId` field should reference the `Pokemon` Table.  
The `TypeId` field should reference the `Type` Table.  
  
#### 4 - Enter the following records in the `Pokemon` table...
- a pokemon with a weight of 70, height of 7, and named Bulbasaur.  
- a pokemon with a height of 3, a weight of 40, and the name Ditto.  
  
#### 5 - Enter the following records in the `Type` table...
- the type Normal.  
- the type Grass.  
  
#### 6 - In the `PokemonType` table...
- create a record to link Bulbasaur to the Grass type.  
- create a record to link Ditto to the Normal type.  
  
#### 7 - Write a query that will retrieve all columns from each record in the `Pokemon` table, and include the Type.Id, Type.Name, and PokemonType.Id fields for each record.  

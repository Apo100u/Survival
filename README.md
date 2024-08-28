# About project
A 3d top-down game with basic survival features: movement, collecting items, inventory, crafting.

Controls:  
W, S, A, D - Movement  
E - Interact  
LMB in inventory panel - Drop item  
LMB in crafting panel - Add / remove from crafting

# Key features
### OutputTree data structure
* Custom data structure [OutputTree](Assets/Scripts/CustomDataStructures/OutputTree) was created to represent recipe tree. It's similiar to n-ary tree but allows nodes to have output.
* Main goal of this class was to avoid iterating through all recipes in the game each time when trying to get recipe from ingredients.
* OutputTree relies on sorted input to make sure recipe will be returned regardless of order of ingredients.
* The class is generic so potentially can be used for other purposes.

### Entity components
* Entities in the game can use [Entities Components](Assets/Scripts/Gameplay/Entities/Components).
* Those components don't know about each other and were made in such a way that they can work independently and can be mixed in any way on an entity.
* The logic of how the components work is managed by entity, example: [ExploringState](Assets/Scripts/Gameplay/Entities/Player/ExploringState.cs).

### State machine
* Generic [State Machine](Assets/Scripts/Gameplay/Helpers/StateMachine/StateMachine.cs) class was created that can be used for any functionality that needs to be a State Machine.
* This State Machine supports conditional transitions between states.
* Example setup of the state machine is in [Player](Assets/Scripts/Gameplay/Entities/Player/Player.cs) class.

# Advent of Code 2023
## Day 16 - The Floor Will Be Lava

### Part 1
#### Initial thoughts
First, I'll create a two dimensional map of the contraption. This structure should have a "energized" property, and a set of energized directions(up, down, left and right).
Then traverse the contraption, starting in the top left, going right.
Mark each tile crossed as energized.
The sample shows that the light can get caught in a perpetual loop. Stop search for a path once a border is crossed, or a tile is reached that is already energized, and is going in the same direction.
Looks like we'll need some kinf of recursive path search function.

### Implementation
Pretty much exactly as I thought.

#### Solution : 6514
---
### Part 2
#### Initial thoughts
Now that the beam can start from any edge position, instead of just the top left corner, I think I just have to add some extra logic to the code that starts the traveral.

### Implementation
My initial thought was right. The way I built part 1 made adapting to part 2 almost trivial.


#### Solution : 8089

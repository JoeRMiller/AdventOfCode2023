# Advent of Code 2023
## Day 10 - Pipe Maze

### Part 1
#### Initial thoughts
The input is a closed loop, with lots of decoy characters.
The plan is to:

	Read the loop map, locating the start position
	Identify the connected points
	Traverse the loop, counting the steps
	Return the maximum distance that can be travelled from the start

### Implementation
Implementation went pretty much as the plan stated.


#### Solution : 6815
---
### Part 2
#### Initial thoughts
This one is diabolical, and I have no idea what algorithm to use.
First thing I'll do is create a map that contains only the traversed pipe loop.
Then maybe check each unmapped part to see if it can reach the outer map wall somehow?
First I'll start with an unmapped point, and see how many pipes it crosses in all 4 cardinal directions. If it is an even number, its outside the loop.


### Implementation
I created a map of the pipe chain, and added each link to a List.
I then iterated though each unmapped tile (excluding the first and last row/column) and searched to the write to count crossed walls.
The search worked by identifying a wall part.
If the part was a | it counted as a crossing.
If it was another pipe piece, if both ends entered/exited in the same direction, it didn't count as a wall crossing.
If each end went in opposite directions, it was a wall crossing.


#### Solution : 269

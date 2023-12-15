# Advent of Code 2023
## Day 15 - Lens Library

### Part 1
#### Initial thoughts
Unless I'm reading it wrong, this seems very straightforward.

	Split the input line by ,
	for each item, calculate the hash value
		get ascii code
		multiply by 17
		mod that by 256,
		add result to previous total
	add all the hashes up

### Implementation
Exactly as planned

#### Solution : 506869
---
### Part 2
#### Initial thoughts

	Alter the hash class to parse the command.
	Calculate the box hash, and set the command type
	Create a dictionary<int,Hash>
	Process the hash list, and modify the boxes as commanded
	Calculate the lens power of each box
	Add up the powers.

### Implementation
Exactly as planned


#### Solution : 271384

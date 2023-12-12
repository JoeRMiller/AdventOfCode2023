# Advent of Code 2023
## Day 11 - Cosmic Expansion

### Part 1
#### Initial thoughts
Seems simple at first glance plan is to:
	
	Read input into list of strings
	Find for rows with no #
	Insert new empty row for each empty row

	Find all columns with no #
	Insert new empty column for empty one.

	Create List of all #, storing coordinates
	Iterate through each #, calculating distance to all subsequent #'s
	Sum all the distances

### Implementation
Implementation went accoring to plan.

#### Solution : 9965032
---
### Part 2
#### Initial thoughts
Seems deceptively similar to part one. Will expand arrays 1000000 times instead of 1, and see what goes wrong.

### Implementation
As per usual, the naive approach was *way* too computationally intensive. I redesigned the data structures so that space didnt actually expand, but instead the tile held the expansion value. When walkign the tiles to calculate a distance, I added the tile values, which would be large if they were part of an expansion. The overall map stated the same size as the original.


#### Solution : 550358864332

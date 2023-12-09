# Advent of Code 2023
## Day 9 - Mirage Maintenance

### Part 1
#### Initial thoughts
Looks like we are building an upside down fibonacci? tree. Create a list of list from the input, work backward from each to calculate the extrapolated number.
Sum the extrapolated numbers

### Implementation
Read input into List of List of ints
For each list, calculate the extrapolated value =>

	First, expand the list into a tree iteravely.
	Then go backwards through the list appending the extrapolated value to each list.
	Finally, return the last item in the first list.

#### Solution : 1939607039
---
### Part 2
#### Initial thoughts
Seems very similar to part 1, but instead of appending an extrapolation, we are pushing one on to the front of the list instead.

### Implementation
Suprisingly simple for a part 2. Instead of appending the extrapolation to list, the extrapolation is inserted at 0. The extrapolation calculation is slightly different, but straightforward.

#### Solution : 1041

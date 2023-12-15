# Advent of Code 2023
## Day 14 - Parabolic Reflector Dish

### Part 1
#### Initial thoughts
Plan is to build a 2 dimensional array of the dish, starting at the top (north), move all rocks (0) up a row if they can.
Iterate through the list of rows, and then add up the weight according to their distance from the bottom.

### Implementation
Turns out I had to keep scanning rows until no more rocks moved. This is a bad algorithm. But the brute force methos worked.
I have a bad feeling that part 2 will expose this weakness horribly.

#### Solution : 108792
---
### Part 2
#### Initial thoughts
Well, rotating the dish a billion times might expose this terrible algorithm. Estimated time is:24 hours. Thats not gonna work. Need a new approach.
This time I'll implement the rock sort differently. I'll look at a row, and starting at the top, move each rock as far as possible.

So far this implementation is somehow *WORSE*, so now I'm hoping the dish settles on a repeating pattern.

This is true for the sample. Now I have to figure out how to find the repeating pattern, figure out how long it is, how long the initial not repeating run is, then calculate what the billionth iteration would be.

### Implementation



#### Solution :

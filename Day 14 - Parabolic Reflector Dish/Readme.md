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
Turns out the dish does settle into a repeating pattern.
Finding that pattern was the hardest part.
First, ran the tilting long enough to start the repeating pattern, and gather ehough weights so they could be separated from weights prior to the pattern starting.
I used a standard deviation below the mean (for the times a specific weight appeared) as the cutoff for weights in the pattern.
I found the last weight in the sequence that wasn't in the group, and discarded them.
Starting with a search window the size of the count of all the weights above the mean count

	Slide the window along the remaining sequence
	Ff there is a match without subsequent mismatches, save the location.
	Subtract the match index plus the leading number of weights from the tilt iterations (1,000,000,000)
	Get the mod of that remainder and matching window length, which is how far into the repeating sequence the correct weight is.


#### Solution : 99118

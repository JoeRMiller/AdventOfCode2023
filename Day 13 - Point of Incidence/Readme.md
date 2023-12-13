# Advent of Code 2023
## Day 13 - Point of Incidence

### Part 1
#### Initial thoughts
After reading all the patterns into a list of strings, I'll iterate through the rows/cols to see of the row/col equals the next.
Then iterate back and forward 1 row/col, and see if they are the same. Continue until I reach a pattern boundary, or there is a mismatch.
If I reach a boundary, I found a mirror, so calculate the result


### Implementation
Went as planned

#### Solution : 31265
---
### Part 2
#### Initial thoughts
Now when comparing, on mismatches see if they are off by one. Fix one side and check for the mirror, then try fixing the other side.

### Implementation
The instructions on this one were kind of unclear. The implementation was to search for identical lines, and if they werent, then check for off by one character lines.
I just replaced the first instance of the mismatched character. I think the key was once a mismatch was found, thats the line. Score it, and dont bother with the other direction if it hasnt been checked.
This code is a mess. I'll need to come back and clean it up, and see how much unecessary code there is.

38642
#### Solution : 39359

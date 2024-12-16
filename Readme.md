# Circular Domino Chain

This C# project solves the problem of determining whether a set of dominoes can form a circular chain. A circular domino chain is a sequence of dominoes where the number on one side of each domino matches the number on the neighboring side of the next domino, and the first and last dominoes match as well, forming a circle.

## Problem Description

Given a set of dominoes, each represented by two integers (a|b), the task is to check whether it's possible to arrange them in a circular chain where:
- The second value of one domino matches the first value of the next domino.
- The first value of the first domino matches the second value of the last domino, forming a closed loop.

### Example:
For a set of dominoes like `2|3, 3|4, 1|2, 4|1`, one possible valid circular chain would be `Circular domino chain: [1|2] [2|3] [3|4] [4|1]`. The first and last dominoes match, forming a valid circular chain.

For a set like `1|2, 4|1, 2|3`, this is **not** a valid chain, because the first and last dominoes don't match. The output would be: `It is impossible to form a circular domino chain.`

## Features

- **Input**: A text file containing a set of dominoes, each represented by two integers in the format `a|b`.
- **Output**: A valid circular domino chain, or a message saying "It is impossible to form a circular domino chain" if no valid chain can be formed.

## How to Run the Program

1. Clone this repository.
2. Create an input text file or use example (e.g., `input_test1.txt`) with the dominoes in the format `a|b`, one per line.
3. Run the C# program.
4. The program will attempt to form a circular chain from the input dominoes and print the result.

### Sample Output:
`Circular domino chain: [1|2] [2|3] [3|4] [4|5] ...`

If it's impossible to form a circular chain:
`It is impossible to form a circular domino chain.`

## Explanation

### Key Concepts:

1. **Domino Matching**: The program attempts to arrange the dominoes such that the numbers on adjacent halves match. The first and last dominoes must also match to form a circular chain.
2. **Backtracking Algorithm**: The solution uses a backtracking approach to explore all possible arrangements of the dominoes, and if a valid chain is found, it returns the chain. If no valid arrangement is found, it returns a failure message.

### Optimizations:
- The program checks whether all numbers in the domino set appear an even number of times before attempting to form a chain. This is a necessary condition for the chain to be circular.

## Usage

To run the program, you need to have:
- .NET SDK installed
- A compatible C# environment (JetBrains Rider, Visual Studio, Visual Studio Code, or any IDE that supports C#)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
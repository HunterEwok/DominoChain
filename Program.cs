namespace DominoChain;

static class CircularDominoChain
{
    // The file name containing the input data for the app.
    private const string InputFileName = "input_test2.txt";
    
    // The constant with the text indicating that the dominoes cannot form a circular chain.
    private const string ErrorMessage = "It is impossible to form a circular domino chain.";
        
    public static void Main()
    {
        // Read dominoes from input file.
        var dominos = ReadDominoesFromFile(InputFileName);

        if (dominos.Count == 0)
        {
            Console.WriteLine($"Error: No valid dominoes found in {InputFileName}");
            return;
        }
        
        // Preliminary check: Are all numbers appearing an even number of times?
        if (CanFormCircularChain(dominos))
        {
            // Attempt to build the chain.
            var result = FindCircularChain(dominos);
            
            if (result.Any())
            {
                Console.WriteLine("Circular domino chain: " + 
                                  string.Join(" ", result.Select(d => $"[{d.Item1}|{d.Item2}]")));
            }
            else
                Console.WriteLine(ErrorMessage);
        }
        else
            Console.WriteLine(ErrorMessage);
    }

    // Read and parse the input data file.
    static List<(int, int)> ReadDominoesFromFile(string filePath)
    {
        var dominos = new List<(int, int)>();

        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int a) &&
                    int.TryParse(parts[1], out int b))
                {
                    dominos.Add((a, b));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return dominos;
    }
    
    // Checks if it's possible to form a circular domino chain by counting dot frequencies.
    static bool CanFormCircularChain(List<(int, int)> dominos)
    {
        var dotCounts = new Dictionary<int, int>();

        foreach (var domino in dominos)
        {
            dotCounts.TryAdd(domino.Item1, 0);
            dotCounts.TryAdd(domino.Item2, 0);

            dotCounts[domino.Item1]++;
            dotCounts[domino.Item2]++;
        }

        // All dots must have even counts for a circular chain to be possible.
        return dotCounts.Values.All(count => count % 2 == 0);
    }

    // This procedure attempts to find a circular domino chain from a given list of dominoes.
    // It uses a backtracking approach to explore all possible arrangements of the dominoes.
    // If a valid circular chain is found, it returns the chain as a list of dominoes. 
    // If no valid circular chain can be formed, it returns an empty list.
    static List<(int, int)> FindCircularChain(List<(int, int)> dominos)
    {
        var used = new bool[dominos.Count];
        var chain = new List<(int, int)>();

        // Since it's about a ring, it doesn't matter which domino we start with.
        // Therefore, we will start with index 0.
        chain.Clear();
        chain.Add(dominos[0]);

        Array.Fill(used, false);
        used[0] = true;

        if (Backtrack(dominos, chain, used))
        {
            // Ensure the chain forms a valid circle.
            if (chain[0].Item1 == chain[^1].Item2)
                return chain;
        }

        return new List<(int, int)>();
    }

    // This procedure recursively tries to add unused dominoes to the current chain
    // by checking if the last domino in the chain can be connected to the next unused domino.
    // If a valid connection is found, the domino is added and the process continues.
    // If the chain is completed successfully, it returns true.
    // If no valid arrangement can be made, it backtracks and tries other possibilities.
    static bool Backtrack(List<(int, int)> dominos, List<(int, int)> chain, bool[] used)
    {
        // Base case: All dominoes are used.
        if (chain.Count == dominos.Count)
            return true;

        for (int i = 0; i < dominos.Count; i++)
        {
            if (!used[i])
            {
                var domino = dominos[i];

                // Try both orientations.
                if (TryAddDomino(dominos, chain, domino, used, i) || 
                    TryAddDomino(dominos, chain, (domino.Item2, domino.Item1), used, i))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Attempts to add a domino to the current chain if it fits, and backtracks if necessary.
    /// </summary>
    /// <param name="dominos">The list of all available dominoes.</param>
    /// <param name="chain">The current chain being built.</param>
    /// <param name="domino">The domino to attempt to add.</param>
    /// <param name="used">A boolean array indicating which dominoes have been used.</param>
    /// <param name="index">The index of the domino being considered for addition.</param>
    /// <returns>True if the domino was successfully added and the chain can continue; false otherwise.</returns>
    static bool TryAddDomino(
        List<(int, int)> dominos, 
        List<(int, int)> chain, 
        (int, int) domino, 
        bool[] used, 
        int index)
    {
        // Check if the current domino can be added to the chain by matching the last value of the chain
        if (chain[^1].Item2 == domino.Item1)
        {
            chain.Add(domino);
            used[index] = true;

            // Recursively attempt to build the chain further with the next domino.
            if (Backtrack(dominos, chain, used))
                return true;

            // If backtracking failed, remove the domino and mark it as unused.
            chain.RemoveAt(chain.Count - 1);
            used[index] = false;
        }

        // Return false if the domino could not be added.
        return false;
    }
}
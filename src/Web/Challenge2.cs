namespace Web
{
    public class Challenge2
    {
        ///  O(n-1) == :)
        /// <summary>
        ///  Find largest difference between any of the string characters
	    ///  where i < j and i comes before j in the alphabet
        /// </summary>
        /// <param name="input"></param>
        /// <returns>int</returns>
        public int MaxDistance(string input)
        {
            // uppercase and lowercase don't really impact distance between letters in the alphabet so we'll work with lowercase. 
            input = input.ToLower();

            // handle null and short strings
            if (string.IsNullOrEmpty(input) || input.Length == 1)
            {
                return 0;
            }

            var maxcount = 0;
            int lowestPosition = 0;
            int highestPosition = 1;
            
            // LowestPosition and highestPosition are updated only if the new lowest or highest character
            // comes before them in the alphabet and occurs before/after the previous one.
            for (int i = 0, j = 1; j < input.Length; i++, j++)
            {                
                lowestPosition = input[i] < input[lowestPosition] 
                    ? i 
                    : lowestPosition;
                
                highestPosition = input[j] > input[highestPosition]
                    // Only update new highest position if it comes after the lowest position
                    && j > lowestPosition 
                        ? j 
                        : highestPosition;
            }
            // If the lowest position comes before the highest position then calculate the difference, otherwise 0
            if (lowestPosition < highestPosition)
            {
                maxcount = GetDifference(input[lowestPosition], input[highestPosition]);
            }
            
            return maxcount;
        }

        //O(n^2) == :(
        // First pass
        public int MaxDistanceSlow(string input)
        {
            var maxcount = 0;
            

            for (int i = 0; i < input.Length; i++)
            {
            
                for (var j = input.Length - 1; j > i; j--)
                {                    
                    // Loops within loops. 
                    if (input[i] < input[j])
                    {
                        var diff = GetDifference(input[i], input[j]);
                        maxcount = diff > maxcount ? diff : maxcount;
                    }
                }
            }            
            return maxcount;
        }

        private int GetDifference(char a, char b)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var diff = alphabet.IndexOf(b) - alphabet.IndexOf(a) - 1;            
            return diff;
        }
    }
}

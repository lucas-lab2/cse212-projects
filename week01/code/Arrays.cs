public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Plan:
        // 1. Create a new array of doubles with the given 'length'.
        // 2. Loop from index 0 to length - 1.
        // 3. At each index i, store (i + 1) * number. This gives us:
        //    index 0 -> 1 * number (first multiple)
        //    index 1 -> 2 * number (second multiple)
        //    ...and so on.
        // 4. Return the completed array.

        var result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (i + 1) * number;
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Plan:
        // Rotating right by 'amount' means the last 'amount' elements move to the front.
        // Example: [1,2,3,4,5,6,7,8,9] rotated right by 3 -> [7,8,9,1,2,3,4,5,6]
        //
        // Steps:
        // 1. Calculate the split index: splitIndex = data.Count - amount
        //    This is where the "tail" (elements that will move to the front) begins.
        // 2. Extract the tail: the last 'amount' elements starting at splitIndex.
        // 3. Remove those tail elements from the end of the list.
        // 4. Insert the tail elements at the beginning (index 0) of the list.

        int splitIndex = data.Count - amount;

        // Extract the last 'amount' elements
        List<int> tail = data.GetRange(splitIndex, amount);

        // Remove the tail from the original list
        data.RemoveRange(splitIndex, amount);

        // Insert the tail at the beginning of the list
        data.InsertRange(0, tail);
    }
}

public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: Skip duplicates — only insert unique values
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Problem 2: BST search — O(log n) on balanced tree
        if (value == Data)
            return true;
        if (value < Data)
            return Left != null && Left.Contains(value);
        return Right != null && Right.Contains(value);
    }

    public int GetHeight()
    {
        // Problem 4: height = 1 + max height of either subtree
        int leftHeight  = Left  != null ? Left.GetHeight()  : 0;
        int rightHeight = Right != null ? Right.GetHeight() : 0;
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}
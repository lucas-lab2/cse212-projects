using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them one by one.
    // Expected Result: Items are dequeued in highest-priority-first order:
    //   "High" (pri 10), then "Medium" (pri 5), then "Low" (pri 1).
    // Defect(s) Found: Bug 1 - loop used `_queue.Count - 1`, skipping the last element.
    //                  Bug 2 - dequeue never called _queue.RemoveAt, so the queue never shrank.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with the same highest priority.
    // Expected Result: The item enqueued LAST ("Beta") is dequeued first because the loop
    //   uses >= when comparing priorities, so each later equal-priority item replaces the
    //   current high-priority index. "Beta" (index 1) beats "Alpha" (index 0).
    // Defect(s) Found: No additional defect; this documents the tie-breaking behavior
    //                  of the fixed implementation.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Alpha", 7);
        priorityQueue.Enqueue("Beta", 7);
        priorityQueue.Enqueue("Gamma", 3);

        // Beta was enqueued after Alpha; with >= comparison, Beta's index replaces Alpha's,
        // so Beta comes out first among the tied priority-7 items.
        Assert.AreEqual("Beta", priorityQueue.Dequeue());
        Assert.AreEqual("Alpha", priorityQueue.Dequeue());
        Assert.AreEqual("Gamma", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue a single item and then dequeue it.
    // Expected Result: The single item is returned.
    // Defect(s) Found: Bug 2 (missing RemoveAt) was caught by this test - the item was
    //                  returned but never removed, causing the queue to appear non-empty.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("OnlyOne", 5);

        Assert.AreEqual("OnlyOne", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException is thrown with message "The queue is empty."
    // Defect(s) Found: None - the empty check was already implemented correctly.
    public void TestPriorityQueue_EmptyThrows()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()} caught: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: The last item added has the highest priority (catches the off-by-one loop bug).
    // Expected Result: "LastButBest" (priority 99, added last) is dequeued first.
    // Defect(s) Found: Bug 1 - the original loop `index < _queue.Count - 1` skipped the last
    //                  element, so a highest-priority item at the end was never considered.
    public void TestPriorityQueue_LastItemHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 2);
        priorityQueue.Enqueue("Second", 3);
        priorityQueue.Enqueue("LastButBest", 99);

        Assert.AreEqual("LastButBest", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue 4 items with varying priorities and drain the entire queue.
    // Expected Result: Items come out in priority order - "D"(10), "B"(8), "C"(5), "A"(1).
    // Defect(s) Found: Both bugs (loop boundary + missing RemoveAt) are exercised here;
    //                  without both fixes the queue would not drain correctly.
    public void TestPriorityQueue_DrainInOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 8);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 10);

        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }
}
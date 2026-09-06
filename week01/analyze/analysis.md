# W01 Analyze: Performance

## Part 1: Analyze Code

### 1. Big-O of `SortArray` in `Sorting.cs`

```csharp
private static void SortArray(int[] data) {
    for (var sortPos = data.Length - 1; sortPos >= 0; sortPos--) {  // outer loop: O(n)
        for (var swapPos = 0; swapPos < sortPos; ++swapPos) {       // inner loop: O(n)
            if (data[swapPos] > data[swapPos + 1]) {
                (data[swapPos + 1], data[swapPos]) = (data[swapPos], data[swapPos + 1]);
            }
        }
    }
}
```

**Answer: O(n²)**

The outer loop iterates over every index of the array (n iterations).
The inner loop iterates from 0 up to the current `sortPos` (also scales with n).
Because the two loops are **nested** and both depend on the size of the data,
the total work is O(n) × O(n) = **O(n²)**. This is a classic Bubble Sort implementation.

---

### 2. Big-O of the three `StandardDeviation` implementations in `StandardDeviation.cs`

| Function              | Big-O     | Reasoning |
|-----------------------|-----------|-----------|
| `StandardDeviation1`  | **O(n)**  | Two separate (non-nested) loops over the array. O(n) + O(n) = O(2n) → **O(n)**. |
| `StandardDeviation2`  | **O(n²)** | A loop over each number that contains **another full loop** to recompute the average each time. Two nested loops both scaling with n → **O(n²)**. |
| `StandardDeviation3`  | **O(n)**  | Uses `numbers.Sum()` (O(n)) followed by one loop (O(n)), both sequential → **O(n)**. |

---

### 3. Other Common Big-O Notations

| Algorithm                    | Big-O Notation | Name                    |
|------------------------------|----------------|-------------------------|
| Merge Sort                   | O(n log n)     | Logarithmic Linear Time |
| Traveling Salesman Algorithm | O(2^n)         | Exponential Time        |

---

### 4. Big-O Performance Order (Best → Worst for large n)

**From best to worst performance:**

1. **O(1)** — Constant time. Work does not grow as n increases.
2. **O(log n)** — Logarithmic. Grows very slowly (e.g., binary search).
3. **O(n)** — Linear. Work grows proportionally with n.
4. **O(n log n)** — Logarithmic linear. Slightly worse than linear (e.g., Merge Sort).
5. **O(n²)** — Polynomial. Work grows as the square of n (e.g., Bubble Sort).
6. **O(2^n)** — Exponential. Extremely fast growth; impractical for large n.

---

## Part 2: Predicting, Measuring, and Comparing — Two Search Algorithms

### Functions analyzed: `SearchSorted1` and `SearchSorted2` in `Search.cs`

### Predictions (before running)

- **`SearchSorted1`**: Uses a `foreach` loop that iterates through every element of the
  array until it finds the target. In the worst case (target not found), it visits all
  n elements → **Predicted: O(n)**.
- **`SearchSorted2`**: Uses a recursive binary search — each call halves the search space.
  This is the same "divide and conquer" pattern described in the lesson →
  **Predicted: O(log n)**.

---

### Actual Results (from running `Search.cs`)

```
              n    sort1-count    sort2-count     sort1-time     sort2-time
     ----------     ----------     ----------     ----------     ----------
              0              0              1        0.00038        0.00037
           1000           1000             11        0.00255        0.00011
           2000           2000             12        0.00469        0.00013
           3000           3000             13        0.00705        0.00014
           4000           4000             13        0.01033        0.00027
           5000           5000             14        0.01257        0.00016
           6000           6000             14        0.01404        0.00015
           7000           7000             14        0.01764        0.00016
           8000           8000             14        0.02056        0.00016
           9000           9000             15        0.02165        0.00015
          10000          10000             15        0.02485        0.00015
          15000          15000             15        0.03611        0.00015
          20000          20000             16        0.04735        0.00018
          25000          25000             16        0.05953        0.00018
```

### Analysis of Results

**`SearchSorted1` (Linear Search):**
- The `sort1-count` grows exactly proportionally with n (1000→1000, 5000→5000, 25000→25000).
- The `sort1-time` also grows linearly as n increases.
- **Confirmed: O(n)**

**`SearchSorted2` (Binary Search):**
- The `sort2-count` barely increases: at n=1000 it is 11 checks; at n=25000 only 16 checks.
- This matches log₂(25000) ≈ 14.6 — exactly logarithmic growth.
- The `sort2-time` stays nearly constant regardless of how large n gets (~0.00011–0.00018 ms).
- **Confirmed: O(log n)**

---

### Answers to Assignment Questions

**Q: What is the performance using Big-O notation for each function?**
- `SearchSorted1`: **O(n)** — Linear Search
- `SearchSorted2`: **O(log n)** — Binary Search

**Q: Which function has the better performance in the worst case?**
- **`SearchSorted2` (Binary Search)** has far better worst-case performance.
  At n=25,000, SearchSorted1 performs 25,000 checks while SearchSorted2 performs only 16.
  Binary Search is dramatically more efficient for large sorted datasets.

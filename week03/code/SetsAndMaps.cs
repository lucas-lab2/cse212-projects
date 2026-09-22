using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am &amp; ma", "if &amp; fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Single-pass O(n): track seen words, emit a pair when we encounter a word
        // whose reverse was already seen.
        var seen = new HashSet<string>(words.Length);
        var results = new List<string>();

        foreach (var word in words)
        {
            // Skip same-letter words like "aa" — they can never form a symmetric pair
            if (word[0] == word[1])
            {
                seen.Add(word);
                continue;
            }

            // Build reverse using char array to avoid string interpolation overhead
            var reversed = new string(new[] { word[1], word[0] });

            // If we've already seen the reverse, this completes a pair — emit it
            if (seen.Contains(reversed))
            {
                results.Add($"{word} & {reversed}");
            }
            else
            {
                seen.Add(word);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            // The degree is in column 4 (0-indexed: index 3)
            if (fields.Length > 3)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                    degrees[degree] += 1;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize: strip spaces and convert to lowercase
        var normalized1 = word1.Replace(" ", "").ToLower();
        var normalized2 = word2.Replace(" ", "").ToLower();

        // Quick length check — if lengths differ, they can't be anagrams
        if (normalized1.Length != normalized2.Length)
            return false;

        // Build a character frequency map from word1
        var charFrequency = new Dictionary<char, int>();
        foreach (var c in normalized1)
        {
            if (charFrequency.ContainsKey(c))
                charFrequency[c] += 1;
            else
                charFrequency[c] = 1;
        }

        // Decrement frequencies using word2's characters
        foreach (var c in normalized2)
        {
            if (!charFrequency.ContainsKey(c))
                return false; // character not in word1 at all

            charFrequency[c] -= 1;

            if (charFrequency[c] < 0)
                return false; // word2 has more of this character than word1
        }

        // All frequencies should be exactly 0 (guaranteed by equal lengths + no negatives above)
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Build formatted strings: "{place} - Mag {mag}"
        var results = new List<string>();
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                var place = feature.Properties?.Place ?? "Unknown";
                var mag = feature.Properties?.Mag;
                results.Add($"{place} - Mag {mag}");
            }
        }

        return results.ToArray();
    }
}
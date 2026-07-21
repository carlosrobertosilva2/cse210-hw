using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        
        // Split text into words and create Word objects
        string[] wordArray = text.Split(' ');
        foreach (string word in wordArray)
        {
            // Remove punctuation for cleaner display but keep it for hiding logic
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int count = 3)
    {
        // Get all visible words
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();
        
        // If no visible words, return
        if (visibleWords.Count == 0)
            return;

        // If there are fewer visible words than count, hide all remaining
        int wordsToHide = Math.Min(count, visibleWords.Count);
        
        // Randomly select words to hide
        Random random = new Random();
        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index); // Remove to avoid selecting again
        }
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText() + " - ";
        
        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }
        
        return displayText.Trim();
    }

    // Method to get count of visible words (useful for progress tracking)
    public int GetVisibleWordCount()
    {
        return _words.Count(w => !w.IsHidden());
    }

    // Method to get total word count
    public int GetTotalWordCount()
    {
        return _words.Count;
    }
}

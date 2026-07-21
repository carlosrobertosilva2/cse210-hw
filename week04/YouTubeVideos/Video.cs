using System;
using System.Collections.Generic;

public class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public int GetLengthInSeconds()
    {
        return _lengthInSeconds;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }

    public string GetFormattedLength()
    {
        int minutes = _lengthInSeconds / 60;
        int seconds = _lengthInSeconds % 60;
        return $"{minutes}:{seconds:D2}";
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {GetFormattedLength()} ({_lengthInSeconds} seconds)");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        
        if (_comments.Count == 0)
        {
            Console.WriteLine("  No comments yet.");
        }
        else
        {
            foreach (Comment comment in _comments)
            {
                Console.WriteLine($"  - {comment.GetDisplayText()}");
            }
        }
        Console.WriteLine();
    }
}

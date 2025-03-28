using System;
using System.Collections.Generic;

// Comment Class: Represents an individual comment
public class Comment
{
    public string Commenter { get; set; }
    public string Text { get; set; }

    // Constructor to initialize a comment
    public Comment(string commenter, string text)
    {
        Commenter = commenter;
        Text = text;
    }
}

// Video Class: Represents a YouTube video
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }  // Private list of comments

    // Constructor to initialize a video
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();  // Initialize the list of comments
    }

    // Method to add a comment to the video
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Method to return the number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Method to display all comments
    public void DisplayComments()
    {
        foreach (var comment in Comments)
        {
            Console.WriteLine($"Comment by {comment.Commenter}: {comment.Text}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Create 3-4 videos with comments
        Video video1 = new Video("How to Make Pizza", "Chef John", 300);
        video1.AddComment(new Comment("Alice", "This looks delicious!"));
        video1.AddComment(new Comment("Bob", "I tried this recipe and it was amazing."));
        video1.AddComment(new Comment("Charlie", "I need to try this, thanks for sharing."));

        Video video2 = new Video("Space Exploration 101", "SpaceX", 1200);
        video2.AddComment(new Comment("Dave", "Great video!"));
        video2.AddComment(new Comment("Eve", "Really informative, learned a lot."));
        video2.AddComment(new Comment("Frank", "Space exploration is the future!"));

        Video video3 = new Video("Learning Python", "Tech Guru", 1500);
        video3.AddComment(new Comment("Grace", "Python is awesome, this tutorial is very clear."));
        video3.AddComment(new Comment("Hank", "Thanks for the tips, very useful."));
        
        // Put videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Iterate through each video and display its information
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            video.DisplayComments();
            Console.WriteLine(); // For spacing between videos
        }
    }
}



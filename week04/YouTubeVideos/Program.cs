using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== YouTube Video Program ===\n");

        // Create videos
        Video video1 = new Video("Learn C# in 10 Minutes", "CodeMaster", 600);
        Video video2 = new Video("Understanding OOP Principles", "TechTeacher", 900);
        Video video3 = new Video("Building Your First App", "DevGuru", 1200);
        Video video4 = new Video("Advanced Debugging Techniques", "BugHunter", 750);

        // Add comments to video 1
        video1.AddComment(new Comment("SarahJ", "This was so helpful!"));
        video1.AddComment(new Comment("MikeR", "Great explanation, thanks!"));
        video1.AddComment(new Comment("EmilyW", "I finally understand C#!"));
        video1.AddComment(new Comment("DavidK", "Subscribed for more content!"));

        // Add comments to video 2
        video2.AddComment(new Comment("AlexP", "OOP finally clicked for me!"));
        video2.AddComment(new Comment("LisaT", "The abstraction example was perfect."));
        video2.AddComment(new Comment("JohnD", "Can you make a video on polymorphism?"));
        video2.AddComment(new Comment("AnnaS", "This is gold, thank you!"));

        // Add comments to video 3
        video3.AddComment(new Comment("ChrisM", "Built my first app today!"));
        video3.AddComment(new Comment("PatriciaR", "Step by step made it easy."));
        video3.AddComment(new Comment("TomH", "Great pacing, not too fast."));
        video3.AddComment(new Comment("JessicaL", "You're a natural teacher."));

        // Add comments to video 4
        video4.AddComment(new Comment("RobertF", "Debugging is my nightmare, this helped."));
        video4.AddComment(new Comment("NancyC", "The breakpoint tips were awesome!"));
        video4.AddComment(new Comment("KevinW", "Finally I can debug without crying."));
        video4.AddComment(new Comment("LauraP", "Very practical advice!"));

        // Add videos to list
        List<Video> videos = new List<Video> { video1, video2, video3, video4 };

        // Display all videos and their comments
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

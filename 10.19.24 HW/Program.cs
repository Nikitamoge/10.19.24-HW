using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.Json;
using System.IO;

//Task 1
public class Album
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Year { get; set; }
    public int Duration { get; set; }
    public string RecordingStudio { get; set; }
    public List<Song> Songs { get; set; }

    public Album()
    {
        Songs = new List<Song>();
    }
}

public class Song
{
    public string Title { get; set; }
    public string Style { get; set; }
    public int Duration { get; set; }
}

public class MusicProgram
{
    public static void Main(string[] args)
    {
        //Task 1
        var album = new Album
        {
            Title = "John Rahn",
            Artist = "The Branches",
            Year = 2005,
            Duration = 35,
            RecordingStudio = "Productin",
            Songs = new List<Song>
            {
                new Song { Title = "Go away", Style = "Rock", Duration = 259 },
                new Song { Title = "Something inside", Style = "Rap", Duration = 183 }
            }
        };

        XmlSerializer serializer = new XmlSerializer(typeof(Album));
        using (FileStream fs = new FileStream("album.xml", FileMode.Create))
        {
            serializer.Serialize(fs, album);
        }

        Album deserializedAlbum;
        using (FileStream fs = new FileStream("album.xml", FileMode.Open))
        {
            deserializedAlbum = (Album)serializer.Deserialize(fs);
        }

        //Task 1
        Console.WriteLine($"Album: {deserializedAlbum.Title}");
        Console.WriteLine($"Artist: {deserializedAlbum.Artist}");
        Console.WriteLine($"Year: {deserializedAlbum.Year}");
        Console.WriteLine($"Duration: {deserializedAlbum.Duration} minutes");
        Console.WriteLine($"Recording Studio: {deserializedAlbum.RecordingStudio}");
        Console.WriteLine("Songs:");
        foreach (var song in deserializedAlbum.Songs)
        {
            Console.WriteLine($" - {song.Title} ({song.Style}), {song.Duration} seconds");
        }


        //Task 2
        var magazine = new Magazine
        {
            Title = "Tech",
            Publisher = "Media fire",
            PublicationDate = new DateTime(2024, 10, 19),
            PageCount = 120,
            Articles = new List<Article>
            {
                new Article { Title = "AI", CharacterCount = 7000, Summary = "Ai future." },
                new Article { Title = "Robots", CharacterCount = 2000, Summary = "Robot's life." }
            }
        };

        string jsonString = JsonSerializer.Serialize(magazine);
        File.WriteAllText("magazine.json", jsonString);

        jsonString = File.ReadAllText("magazine.json");
        var deserializedMagazine = JsonSerializer.Deserialize<Magazine>(jsonString);

        Console.WriteLine($"Magazine: {deserializedMagazine.Title}");
        Console.WriteLine($"Publisher: {deserializedMagazine.Publisher}");
        Console.WriteLine($"Publication Date: {deserializedMagazine.PublicationDate.ToShortDateString()}");
        Console.WriteLine($"Page Count: {deserializedMagazine.PageCount}");
        Console.WriteLine("Articles:");
        foreach (var article in deserializedMagazine.Articles)
        {
            Console.WriteLine($" - {article.Title}, {article.CharacterCount} characters");
            Console.WriteLine($"   Summary: {article.Summary}");
        }
    }
}

public class Magazine
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime PublicationDate { get; set; }
    public int PageCount { get; set; }
    public List<Article> Articles { get; set; }

    public Magazine()
    {
        Articles = new List<Article>();
    }
}

public class Article
{
    public string Title { get; set; }
    public int CharacterCount { get; set; }
    public string Summary { get; set; }
}

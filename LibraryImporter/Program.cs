using System;
using System.IO;
using PlaylistManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LibraryImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];
            Console.WriteLine("Importing records from file: " + file + " ....");
            var csv = new CsvHelper.CsvReader(File.OpenText(file));
            csv.Configuration.HasHeaderRecord = true;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            csv.Read();
            csv.ReadHeader();
            var album = String.Empty;
            var artists = new Dictionary<string, Artist>();
            var ctx = new DbContextOptionsBuilder<MusicLibraryContext>();
            ctx.UseSqlServer<MusicLibraryContext>(configuration.GetConnectionString("MusicLibraryDB"));
            using (var db = new MusicLibraryContext(ctx.Options))
            {
                var alb = new Album();
                var art = new Artist();
                while (csv.Read())
                {
                    var r = csv.GetRecord<TrackRecord>();
                    if (r.Album != album)
                    {
                        album = r.Album;
                        int y;
                        alb = new Album
                        {
                            Title = r.Album,
                            AlbumArtist = r.AlbumArtist,
                            Year = (Int32.TryParse(r.Year, out y) ? (int?)y : null)
                        };
                        db.Albums.Add(alb);
                    }
                    if (!artists.ContainsKey(r.Artist))
                    {
                        art = new Artist
                        {
                            Title = r.Artist,
                        };
                        db.Artists.Add(art);
                        artists.Add(r.Artist, art);
                    }
                    else
                        art = artists[r.Artist];

                    int dn;
                    var t = new Track
                    {
                        Album = alb,
                        Artist = art,
                        Title = r.Title,
                        TrackNum = r.TrackNum,
                        DiscNum = (Int32.TryParse(r.DiscNum, out dn) ? (int?)dn : null)
                    };
                    db.Tracks.Add(t);
                }
                db.SaveChanges();
            }

            Console.WriteLine("Import complete - press any key to exit...");
            Console.ReadLine();
        }
    }
    public class TrackRecord
    {
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Artist { get; set; }
        public string DiscNum { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }
        public int? TrackNum { get; set; }
    }
}

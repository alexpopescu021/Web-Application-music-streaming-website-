﻿using PAW.Model;
using PAWDataAcess.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppLogic
{
    public class SongService
    {
        private readonly IPersistanceContext persistanceContext;
        private readonly ISongRepository songRepository;

        public SongService(IPersistanceContext persistanceContext)
        {
            songRepository = persistanceContext.SongRepository;
            this.persistanceContext = persistanceContext;
        }

        public IEnumerable<Song> GetSongsByGenre(string genre)
        {

            return songRepository.GetSongsByGenre(genre);
        }

        public Song GetSongById(string songId)
        {
            Guid.TryParse(songId, out Guid guid);
            var song = songRepository?.GetById(guid);

            if (song == null)
            {
                throw new Exception();
            }

            return song;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return songRepository.GetAll();
        }

        public Song CreateNewSong(string artist, string genre, string title, decimal price, string path)
        {
            var song = Song.Create(artist,  genre,  title, price, path);
            song = songRepository.Add(song);
            persistanceContext.SaveChanges();
            
            return song;
        }

        public void RemoveSong(string id)
        {
            Guid idToSearch = Guid.Parse(id);
            songRepository.Remove(idToSearch);
            persistanceContext.SaveChanges();
        }

        public void Update(Guid id, string artist, string genre, string title, decimal price, string path)
        {
            songRepository.UpdateSong(id,title,genre,artist, price, path);
        }

        public void Update(Guid id, string artist, string genre, string title, decimal price)
        {
            songRepository.UpdateSong(id, title, genre, artist, price);
        }
        public Song CreateNewSong(string title, string artist, string genre, decimal price)
        {
            var song = Song.Create(artist, genre, title, price);
            song = songRepository.Add(song);
            persistanceContext.SaveChanges();
            return song;
        }

        public void AddToCart(Song song)
        {
            
        }
    }
}

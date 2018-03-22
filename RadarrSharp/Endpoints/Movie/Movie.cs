﻿using Newtonsoft.Json;
using RadarrSharp.Helpers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RadarrSharp.Endpoints.Movie
{
    /// <summary>
    /// Movie endpoint client
    /// </summary>
    public class Movie : IMovie
    {
        private RadarrClient _radarrClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        /// <param name="radarrClient">The radarr client.</param>
        public Movie(RadarrClient radarrClient)
        {
            _radarrClient = radarrClient;
        }

        /// <summary>
        /// Returns all Movies in your collection
        /// </summary>
        /// <returns>Data.Movie[]</returns>
        public async Task<Data.Movie[]> GetMovies()
        {
            var json = await _radarrClient.GetJson("/movie");

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<Data.Movie[]>(json, JsonHelpers.SerializerSettings);

            return null;
        }

        /// <summary>
        /// Returns the movie with the matching ID
        /// </summary>
        /// <param name="id">Movie ID</param>
        /// <returns>Data.Movie</returns>
        public async Task<Data.Movie> GetMovie(int id)
        {
            var json = await _radarrClient.GetJson($"/movie/id={id}");

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<Data.Movie>(json, JsonHelpers.SerializerSettings);

            return null;
        }

        /// <summary>
        /// Adds a new movie to your collection
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="qualityProfileId">Quality profile ID</param>
        /// <param name="titleSlug">Title slug</param>
        /// <param name="images">Images array</param>
        /// <param name="tmdbId">TMDb ID</param>
        /// <param name="rootFolderPath">Full path will be created by combining the rootFolderPath with the movie title</param>
        /// <param name="monitored">Is monitored</param>
        /// <param name="addOptions">Usage unknown</param>
        /// <returns>Data.Movie</returns>
        public async Task<Data.Movie> AddMovie(string title, int qualityProfileId, string titleSlug, Data.Image[] images, int tmdbId, string rootFolderPath, [Optional] bool monitored, [Optional] Dictionary<string, bool> addOptions)
        {
            var dictionary = new Dictionary<string, object>
            {
                ["title"] = title,
                ["qualityProfileId"] = qualityProfileId,
                ["titleSlug"] = titleSlug,
                ["images"] = images,
                ["tmdbId"] = tmdbId,
                ["rootFolderPath"] = rootFolderPath,
                ["monitored"] = monitored
            };

            if (addOptions != null)
                dictionary.Add("addOptions", addOptions);

            string parameter = JsonConvert.SerializeObject(new Dictionary<string, object>(dictionary));

            var json = await _radarrClient.PostJson("/movie", parameter, "POST");

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<Data.Movie>(json, JsonHelpers.SerializerSettings);

            return null;
        }

        /// <summary>
        /// Update an existing movie
        /// </summary>
        /// <param name="movie">Movie to update - Requires all properties of Data.Movie object</param>
        /// <returns>Data.Movie</returns>
        public async Task<Data.Movie> UpdateMovie(Data.Movie movie)
        {
            var json = await _radarrClient.PostJson("/movie", JsonConvert.SerializeObject(movie), "PUT");

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<Data.Movie>(json, JsonHelpers.SerializerSettings);

            return null;
        }

        /// <summary>
        /// Delete the movie with the given ID
        /// </summary>
        /// <param name="id">Movie ID</param>
        /// <param name="deleteFiles">If true the movie folder and all files will be deleted when the movie is deleted</param>
        /// <returns>Nothing</returns>
        public async Task DeleteMovie(int id, [Optional] bool deleteFiles)
        {
            await _radarrClient.Delete($"/movie/id={id}{(deleteFiles ? $"?deleteFiles={deleteFiles}" : "")}");
        }
    }
}
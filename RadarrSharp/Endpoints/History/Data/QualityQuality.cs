﻿using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace RadarrSharp.Endpoints.History.Data
{
    /// <summary>
    /// 
    /// </summary>
    public partial class QualityQuality
    {
        /// <summary>
        /// 
        /// </summary>
        [J("id")] public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [J("name")] public string Name { get; set; }
    }
}
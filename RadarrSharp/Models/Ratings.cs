﻿using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace RadarrSharp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Ratings
    {
        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        /// <value>
        /// The votes.
        /// </value>
        [J("votes")] public long Votes { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [J("value")] public double Value { get; set; }
    }
}

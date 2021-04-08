﻿/*
Copyright 2013 Google Inc

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Google.Apis.Auth
{
    /// <summary>
    /// JSON Web Token (JWT) implementation as specified in 
    /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-08.
    /// </summary>
    public class JsonWebToken
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// JWT Header as specified in http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-08#section-5.
        /// </summary>
        public class Header
        {
            /// <summary>
            /// Gets or sets type header parameter used to declare the type of this object or <c>null</c>.
            /// </summary>
            [JsonProperty("typ")]
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets content type header parameter used to declare structural information about the JWT or 
            /// <c>null</c>.
            /// </summary>
            [JsonProperty("cty")]
            public string ContentType { get; set; }
        }

        /// <summary>
        /// JWT Payload as specified in http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-08#section-4.1.
        /// </summary>
        public class Payload
        {
            /// <summary>
            /// Gets or sets issuer claim that identifies the principal that issued the JWT or <c>null</c>.
            /// </summary>
            [JsonProperty("iss")]
            public string Issuer { get; set; }

            /// <summary>
            /// Gets or sets subject claim identifying the principal that is the subject of the JWT or <c>null</c>.
            /// </summary>
            [JsonProperty("sub")]
            public string Subject { get; set; }

            /// <summary>
            /// Gets or sets audience claim that identifies the audience that the JWT is intended for (should either be
            /// a string or list) or <c>null</c>.
            /// </summary>
            [JsonProperty("aud")]
            public object Audience { get; set; }

            /// <summary>
            /// Gets or sets the target audience claim that identifies the audience that an OIDC token generated from
            /// this JWT is intended for. Maybe be null. Multiple target audiences are not supported.
            /// <c>null</c>.
            /// </summary>
            [JsonProperty("target_audience")]
            public string TargetAudience { get; set; }

            /// <summary>
            /// Gets or sets expiration time claim that identifies the expiration time (in seconds) on or after which 
            /// the token MUST NOT be accepted for processing or <c>null</c>.
            /// </summary>
            [JsonProperty("exp")]
            public long? ExpirationTimeSeconds { get; set; }

            /// <summary>
            /// Gets or sets not before claim that identifies the time (in seconds) before which the token MUST NOT be
            /// accepted for processing or <c>null</c>.
            /// </summary>
            [JsonProperty("nbf")]
            public long? NotBeforeTimeSeconds { get; set; }

            /// <summary>
            /// Gets or sets issued at claim that identifies the time (in seconds) at which the JWT was issued or 
            /// <c>null</c>.
            /// </summary>
            [JsonProperty("iat")]
            public long? IssuedAtTimeSeconds { get; set; }

            /// <summary>
            /// Gets or sets JWT ID claim that provides a unique identifier for the JWT or <c>null</c>.
            /// </summary>
            [JsonProperty("jti")]
            public string JwtId { get; set; }

            /// <summary>
            /// The nonce value specified by the client during the authorization request.
            /// Must be present if a nonce was specified in the authorization request, otherwise this will not be present.
            /// </summary>
            [JsonProperty("nonce")]
            public string Nonce { get; set; }

            /// <summary>
            /// Gets or sets type claim that is used to declare a type for the contents of this JWT Claims Set or 
            /// <c>null</c>.
            /// </summary>
            [JsonProperty("typ")]
            public string Type { get; set; }

            /// <summary>Gets the audience property as a list.</summary>
            [JsonIgnore]
            public IEnumerable<string> AudienceAsList
            {
                get
                {
                    var asList = Audience as List<string>;
                    if (asList != null)
                    {
                        return asList;
                    }
                    var list = new List<string>();
                    var asString = Audience as string;
                    if (asString != null)
                    {
                        list.Add(asString);
                    }

                    return list;
                }
            }

            [JsonIgnore]
            internal DateTimeOffset? IssuedAt =>
                IssuedAtTimeSeconds is null ? (DateTimeOffset?)null : UnixEpoch.AddSeconds(IssuedAtTimeSeconds.Value);

            [JsonIgnore]
            internal DateTimeOffset? ExpiresAt =>
                ExpirationTimeSeconds is null ? (DateTimeOffset?)null : UnixEpoch.AddSeconds(ExpirationTimeSeconds.Value);
        }
    }
}
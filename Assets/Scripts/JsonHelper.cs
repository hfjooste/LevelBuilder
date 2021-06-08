/*
 2021 © Third Pixel Games. All Rights Reserved

 All information contained herein is and remains the property of Third Pixel Games. The intellectual 
 and technical concepts contained herein are proprietary to Third Pixel Games and may be covered by 
 patents and patents in process and are protected by trade secret and copyright laws. Dissemination 
 of this information or reproduction of this material (including source code) is strictly forbidden 
 unless prior written consent is obtained from Third Pixel Games.
*/

namespace LevelBuilder
{
    using System;

    using UnityEngine;

    /// <summary>
    /// A helper class to assist with serializing arrays
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Deserialize the object using the provided json
        /// </summary>
        /// <typeparam name="T">The type of object currently serialized</typeparam>
        /// <param name="json">The json string to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static T[] FromJson<T>(string json)
        {
            return JsonUtility.FromJson<Wrapper<T>>(json).Items;
        }

        /// <summary>
        /// Serialize the object to a json string
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="array">The object to serialize</param>
        /// <returns>The serialized json string</returns>
        public static string ToJson<T>(T[] array)
        {
            return JsonUtility.ToJson(new Wrapper<T> { Items = array });
        }

        /// <summary>
        /// A helper class used to serialize arrays
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        [Serializable]
        private class Wrapper<T>
        {
            /// <summary>
            /// The array of items that will be serialized
            /// </summary>
            public T[] Items;
        }
    }
}
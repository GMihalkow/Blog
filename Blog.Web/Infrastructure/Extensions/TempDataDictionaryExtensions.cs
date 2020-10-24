using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Blog.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        // TODO [GM]: Maybe support collections?
        public static void AddSerialized<T>(this ITempDataDictionary dictionary, string key, T item)
        {
            var serializedObject = JObject.FromObject(item);

            serializedObject["Type"] = item.GetType().FullName;

            if (dictionary.ContainsKey(key))
                dictionary.Remove(key);

            dictionary.Add(key, serializedObject.ToString());
        }

        public static T GetValue<T>(this ITempDataDictionary dictionary, string key)
        {
            dictionary.TryGetValue(key, out object seralizedObj);

            if (seralizedObj != null)
            {
                var jsonStringRepresentationOfObject = seralizedObj.ToString();
                var jsonParsedObj = JObject.Parse(jsonStringRepresentationOfObject);

                if (jsonParsedObj.Value<string>("Type") != typeof(T).FullName)
                    throw new InvalidOperationException("Invalid type specified.");

                jsonParsedObj.Remove("Type");

                return JsonConvert.DeserializeObject<T>(jsonParsedObj.ToString());
            }

            return default;
        }
    }
}
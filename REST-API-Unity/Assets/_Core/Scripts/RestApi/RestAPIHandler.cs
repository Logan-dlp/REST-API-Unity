using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace logandlp.restapi
{
    public static class RestAPIHandler<T> where T : class
    {
        private const string API_URL = "http://localhost";
        private const string API_PORT = "8080";
        private const string API_KEY = "CjQBR9mMxnrrfiGj";
        
        private static string API_LINK = $"{API_URL}:{API_PORT}/{API_KEY}";
        
        public static IEnumerator Post(T obj)
        {
            string json = JsonConvert.SerializeObject(obj);

            UnityWebRequest request = new UnityWebRequest(API_LINK, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
        }

        public static IEnumerator Get(Action<T> onResult)
        {
            UnityWebRequest request = UnityWebRequest.Get(API_LINK);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                List<T> result = JsonConvert.DeserializeObject<List<T>>(request.downloadHandler.text);
                if (result != null && result.Count > 0)
                {
                    onResult?.Invoke(result[0]);
                }
            }
        }
    }
}
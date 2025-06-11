using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace logandlp.restapi
{
    public static class RestAPIHandler<T> where T : struct
    {
        private const string API_URL = "http://localhost";
        private const string API_PORT = "8080";
        private const string API_KEY = "f8fd4fca4f48ee7dd58beaaf85edd8167196dc3ac0d1c590c4a5e529016f8e8d";
        
        private static string API_LINK = $"{API_URL}:{API_PORT}/{API_KEY}";
        
        public static IEnumerator Post(T obj)
        {
            string json = JsonUtility.ToJson(obj);

            UnityWebRequest request = new(API_LINK, "POST");
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"POST success : {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"POST failed : {request.error}");
            }
        }

        public static IEnumerator Get(T obj)
        {
            UnityWebRequest request = UnityWebRequest.Get(API_LINK);
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"GET success : {request.downloadHandler.text}");
                obj = JsonUtility.FromJson<T>(request.downloadHandler.text);
            }
            else
            {
                obj = default(T);
            }
        }
    }
}
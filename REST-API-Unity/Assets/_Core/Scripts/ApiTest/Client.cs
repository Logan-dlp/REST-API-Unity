using UnityEngine;

namespace logandlp.apitest
{
    using restapi;
    
    public class Client : MonoBehaviour
    {
        [SerializeField] private string _message;

        [ContextMenu("Send Message")]
        public void Send()
        {
            Data data = new()
            {
                Message = _message
            };

            StartCoroutine(RestAPIHandler<Data>.Post(data));
        }
    }
}
using TMPro;
using UnityEngine;

namespace logandlp.apitest
{
    using restapi;
    
    public class Client : MonoBehaviour
    {
        public void SendMessages(TextMeshProUGUI message)
        {
            Data data = new()
            {
                Message = message.text
            };

            StartCoroutine(RestAPIHandler<Data>.Post(data));
        }
    }
}
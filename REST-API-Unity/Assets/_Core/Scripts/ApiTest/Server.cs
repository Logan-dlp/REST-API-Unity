using UnityEngine;
using UnityEngine.Events;

namespace logandlp.apitest
{
    using restapi;

    public class Server : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Data> _onDataReceived;
        
        private void Update()
        {
            StartCoroutine(RestAPIHandler<Data>.Get((data) => {
                if (data != null)
                {
                    _onDataReceived?.Invoke(data);
                }
            }));
        }
    }
}
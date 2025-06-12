using UnityEngine;

namespace logandlp.apitest
{
    using restapi;

    public class Server : MonoBehaviour
    {
        private void Update()
        {
            StartCoroutine(RestAPIHandler<Data>.Get((data) => {
                if (data != null)
                {
                    Debug.Log(data.Message);
                }
            }));
        }
    }
}
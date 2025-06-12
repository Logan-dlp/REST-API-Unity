using TMPro;
using UnityEngine;

namespace logandlp.apitest
{
    public class DisplayMessage : MonoBehaviour
    {
        [SerializeField] private GameObject _template;
        [SerializeField] private GameObject _parent;
        
        public void CreateInstance(Data data)
        {
            GameObject instance = Instantiate(_template, _parent.transform);
            if (instance.TryGetComponent(out TextMeshProUGUI text))
            {
                text.text = data.Message;
            }
        }
    }
}
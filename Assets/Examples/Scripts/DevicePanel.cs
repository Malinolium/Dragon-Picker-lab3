using InstantGamesBridge;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class DevicePanel : MonoBehaviour
    {
        [SerializeField] private Text _type;

        private void OnEnable()
        {
            _type.text = $"Type: { Bridge.device.type }";
        }
    }
}
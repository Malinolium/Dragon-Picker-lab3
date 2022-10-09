using InstantGamesBridge;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class PlatformPanel : MonoBehaviour
    {
        [SerializeField] private Text _id;

        [SerializeField] private Text _language;

        [SerializeField] private Text _payload;

        private void OnEnable()
        {
            _id.text = $"ID: { Bridge.platform.id }";
            _language.text = $"Language: { Bridge.platform.language }";
            _payload.text = $"Payload: { Bridge.platform.payload }";
        }
    }
}
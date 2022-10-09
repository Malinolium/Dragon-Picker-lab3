using System.Collections.Generic;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Game;
using UnityEngine;

namespace Examples
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private GameObject _initializationPanel;

        [SerializeField] private GameObject _initializationErrorPanel;

        [SerializeField] private List<GameObject> _otherPanels;

        [SerializeField] private AudioSource _musicAudioSource;

        private void Start()
        {
            _initializationPanel.SetActive(true);
            _initializationErrorPanel.SetActive(false);

            foreach (var panel in _otherPanels)
                panel.SetActive(false);

            Bridge.Initialize(OnInitializationCompleted);
        }

        private void OnInitializationCompleted(bool isInitialized)
        {
            if (isInitialized)
            {
                _initializationPanel.SetActive(false);

                foreach (var panel in _otherPanels)
                    panel.SetActive(true);

                _musicAudioSource.Play();
                Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;
            }
            else
                _initializationErrorPanel.SetActive(true);
        }

        private void OnGameVisibilityStateChanged(VisibilityState visibilityState)
        {
            switch (visibilityState)
            {
                case VisibilityState.Visible:
                    _musicAudioSource.Play();
                    break;

                case VisibilityState.Hidden:
                    _musicAudioSource.Pause();
                    break;
            }
        }
    }
}
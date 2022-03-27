using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CryptoMandala
{
    public sealed class ReloadUI : MonoBehaviour
    {
        [SerializeField] Button _reloadButton;

        void Start()
        {
            Observable.EveryUpdate()
                .Where(_=>Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ =>
                {
                    _reloadButton.gameObject.SetActive(_reloadButton.gameObject.activeSelf);
                }).AddTo(this);
            
            
            _reloadButton.onClick.AddListener(() =>
            {
                LensProtocolSingleton.Instance.UpdateData();
            });
        }
    }
}
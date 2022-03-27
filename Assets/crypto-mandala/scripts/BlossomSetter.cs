using System;
using UniRx;
using UnityEngine;

namespace CryptoMandala
{
    public sealed class BlossomSetter : MonoBehaviour
    {
        [SerializeField] bool _forceLevel = false;
        [SerializeField] int _level = 0;
        [SerializeField] GameObject[] _blossomPrefabs;

        [SerializeField] Transform _blossomParent;

        LensProtocol _data => LensProtocolSingleton.Instance.LensProtocolData;
        
        GameObject _blossom;

        void Start()
        {
            LensProtocolSingleton.Instance.Loaded
                .ObserveEveryValueChanged(v=>v.Value)
                .Where(x => x)
                .SubscribeOnMainThread()
                .Subscribe(_ =>
                {
                    Destroy(_blossom);
                    SetUp();
                }).AddTo(this);
        }

        void SetUp()
        {
            var spawnNum = _data.SeedLevel() > 7
                ? 7
                : _data.SeedLevel() - 1;
            
            _blossom = Instantiate(_blossomPrefabs[_forceLevel ? _level : spawnNum], _blossomParent);
            _blossom.transform.localPosition = Vector3.zero;
            _blossom.transform.localScale = Vector3.one;
            
            Debug.Log($"spawned num: {spawnNum} = {_blossom.name}");

            // tree.gameObject.transform.localScale = Vector3.one * 0.1f * _data.SocialLevel();
        }
    }
}
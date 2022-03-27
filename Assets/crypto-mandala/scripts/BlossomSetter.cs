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

        void Start()
        {
            LensProtocolSingleton.Instance.Loaded
                .ObserveEveryValueChanged(v=>v.Value)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    SetUp();
                }).AddTo(this);
        }

        void SetUp()
        {
            var spawnNum = _data.SeedLevel() > 7
                ? 7
                : _data.SeedLevel() - 1;
            
            var tree = Instantiate(_blossomPrefabs[_forceLevel ? _level : spawnNum], _blossomParent);
            
            Debug.Log($"spawned num: {spawnNum} = {tree.name}");

            // tree.gameObject.transform.localScale = Vector3.one * 0.1f * _data.SocialLevel();
        }
    }
}
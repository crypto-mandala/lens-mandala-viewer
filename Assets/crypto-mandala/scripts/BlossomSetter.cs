using UnityEngine;

namespace CryptoMandala
{
    public sealed class BlossomSetter : MonoBehaviour
    {
        [SerializeField] GameObject[] _blossomPrefabs;

        [SerializeField] Transform _blossomParent;

        LensProtocol _data => LensProtocolSingleton.Instance.LensProtocolData;

        void Start()
        {
            var spawnNum = _data.SeedLevel() > 7
                ? 7
                : _data.SeedLevel() - 1;
            
            var tree = Instantiate(_blossomPrefabs[spawnNum], _blossomParent);
            
            Debug.Log($"spawned num: {spawnNum} = {tree.name}");

            // tree.gameObject.transform.localScale = Vector3.one * 0.1f * _data.SocialLevel();
        }
    }
}
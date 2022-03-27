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
            var tree = Instantiate(
                _blossomPrefabs[_data.SeedLevel() > 7
                    ? 7
                    : _data.SeedLevel()],
                _blossomParent);

            tree.gameObject.transform.localScale = Vector3.one * 0.1f * _data.SocialLevel();
        }
    }
}
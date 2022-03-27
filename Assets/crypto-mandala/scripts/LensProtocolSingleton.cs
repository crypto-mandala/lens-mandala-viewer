using System.IO;
using System.Net;
using System.Text;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;


namespace CryptoMandala
{
    public class LensProtocolSingleton
    {
        // example https://lens-mandala.vercel.app/api/neo4j/seed/0x41db8e68b817abe104cced933c9d8c5030ba1879/1
        public string Address { get; private set; } = "0x41db8e68b817abe104cced933c9d8c5030ba1879";
        
        string _hostUri = "https://lens-mandala.vercel.app";
        string PostUri() => $"{_hostUri}/api/neo4j/seed/{Address}/1";
        
        public LensProtocol LensProtocolData { get; private set; }= new LensProtocol();
        
        public static LensProtocolSingleton Instance = new LensProtocolSingleton();
        
        BoolReactiveProperty _loaded = new BoolReactiveProperty(false);
        public IReadOnlyReactiveProperty<bool> Loaded => _loaded;

        LensProtocolSingleton()
        {
            UpdateData();
        }
        
        public void SetAddress(string address)
        {
            Address = address;
            Debug.Log($"Set NFT address: {Address}");
        }
        
        
        public void UpdateData()
        {
            UniTask.Run(async () =>
            {
                _loaded.Value = false;
                return _loaded.Value = await GetHttpAsync();
            });
        }

        async UniTask<bool> GetHttpAsync()
        {
            var request = WebRequest.CreateHttp(PostUri());
            request.Method = "GET";
            using var res = await request.GetResponseAsync();
            
            if(res == null)
            {
                Debug.LogError($"Get Http Error with {PostUri()}");
                return false;
            }
            
            await using var stream = res.GetResponseStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var json = await reader.ReadToEndAsync();
            LensProtocolData = JsonUtility.FromJson<LensProtocol>(json);
            
            Debug.Log($"Got result from {PostUri()}\n{json}");
            return true;
        }
    }
}


using System.IO;
using System.Net;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace CryptoMandala
{
    public class LensProtocolSingleton : SingletonMonoBehaviour<LensProtocolSingleton>
    {
        // example https://lens-mandala.vercel.app/api/neo4j/seed/0x41db8e68b817abe104cced933c9d8c5030ba1879/1
        string _address = "0x41db8e68b817abe104cced933c9d8c5030ba1879";
        string _hostUri = "https://lens-mandala.vercel.app";
        string PostUri() => $"{_hostUri}/api/neo4j/seed/{_address}/1";

        
        public LensProtocol LensProtocolData;
        
        
        void Start()
        {
            SetEndTime();
        }

        void SetEndTime()
        {
            UniTask.RunOnThreadPool(async () =>
            {
                await GetHttp();
            });
        }
        
        public void SetAddress(string address)
        {
            _address = address;
            Debug.Log($"Set NFT address: {_address}");
        }


        async UniTask<bool> GetHttp()
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


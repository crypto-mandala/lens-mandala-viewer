using System;

namespace CryptoMandala
{
    [Serializable]
    public class LensProtocol
    {
        public string timestamp_minted;
        public int profileCount;
        public int postCount;
        public int mirrorCount;
        public int collectCount;

        int _level = 0;
        public int SeedLevel()
        {
            var l = profileCount * postCount * mirrorCount * collectCount *0.1f;
            _level = (int)Math.Round(l);
            
            return _level == 0 ? 1 : _level;
        }
        
        public int SocialLevel()
        {
            var l = mirrorCount * profileCount;
            return l == 0 ? 1 : l;
        }

        public DateTime GetMintedDate()
        {
            return DateTime.Parse(timestamp_minted);
        }
    }
    
    // {"timestamp_minted":"2022-03-25T23:13:00Z","profileCount":1,"postCount":1,"mirrorCount":0,"collectCount":0}
}
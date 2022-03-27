using System;

namespace CryptoMandala
{
    public class LensProtocol
    {
        public string timestamp_minted;
        public int profileCount;
        public int postCount;
        public int mirrorCount;
        public int collectCount;
        
        public DateTime GetMintedDate()
        {
            return DateTime.Parse(timestamp_minted);
        }
    }
    
    // {"timestamp_minted":"2022-03-25T23:13:00Z","profileCount":1,"postCount":1,"mirrorCount":0,"collectCount":0}
}
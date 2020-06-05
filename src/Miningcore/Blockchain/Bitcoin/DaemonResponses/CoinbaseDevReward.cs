using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Miningcore.Blockchain.Bitcoin.DaemonResponses
{
    public class CoinbaseDevReward
    {
        public string Payee        { get; set; }
        public string Scriptpubkey { get; set; }
        public long   Value        { get; set; }
    }

    public class CoinbaseDevRewardTemplateExtra
    {
        public JToken CoinbaseDevReward { get; set; }
    }
}

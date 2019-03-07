using System.Collections.Generic;
using NBitcoin;

namespace Lykke.Bil2.Bitcoin.SignService.SerializationContexts
{
    public class TransactionInfo
    {
        public string TransactionHex { get; set; }

        public IEnumerable<Coin> UsedCoins { get; set; }
    }
}

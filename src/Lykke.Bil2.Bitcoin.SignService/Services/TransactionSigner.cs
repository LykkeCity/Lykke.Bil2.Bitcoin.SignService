using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Bil2.Bitcoin.SignService.SerializationContexts;
using Lykke.Bil2.Contract.Common;
using Lykke.Bil2.Contract.SignService.Responses;
using Lykke.Bil2.Sdk.SignService.Services;
using NBitcoin;
using NBitcoin.JsonConverters;

namespace Lykke.Bil2.Bitcoin.SignService.Services
{
    public class TransactionSigner : ITransactionSigner
    {
        private readonly Network _network;

        public TransactionSigner(Network network)
        {
            _network = network;
        }

        public Task<SignTransactionResponse> SignAsync(IReadOnlyCollection<string> privateKeys, Base58String requestTransactionContext)
        {
            var context = Serializer.ToObject<TransactionInfo>(requestTransactionContext.DecodeToString());

            var tx = Transaction.Parse(context.TransactionHex, _network);

            var secretKeys = privateKeys.Select(p => Key.Parse(p, _network)).ToArray();

            var signed = _network.CreateTransactionBuilder()
                .AddCoins(context.UsedCoins)
                .AddKeys(secretKeys)
                .SignTransaction(tx);

            return Task.FromResult(new SignTransactionResponse(new Base58String(signed.ToHex()), signed.GetHash().ToString()));
        }
    }
}

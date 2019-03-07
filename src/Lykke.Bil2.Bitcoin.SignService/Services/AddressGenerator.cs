using System.Threading.Tasks;
using Common;
using Lykke.Bil2.Bitcoin.SignService.SerializationContexts;
using Lykke.Bil2.Contract.Common;
using Lykke.Bil2.Contract.SignService.Requests;
using Lykke.Bil2.Contract.SignService.Responses;
using Lykke.Bil2.Sdk.Exceptions;
using Lykke.Bil2.Sdk.SignService.Models;
using Lykke.Bil2.Sdk.SignService.Services;
using NBitcoin;

namespace Lykke.Bil2.Bitcoin.SignService.Services
{
    public class AddressGenerator : IAddressGenerator
    {

        private readonly Network _network;

        public AddressGenerator(Network network)
        {
            _network = network;
        }

        public  Task<AddressCreationResult> CreateAddressAsync()
        {
            var key = new Key();

            var address = key.PubKey.GetAddress(_network).ToString();
            var privateKey = key.GetWif(_network).ToString();
            var addressCtx = new AddressContextContract
            {
                PubKey = key.PubKey.ToString()
            };

            return Task.FromResult(new AddressCreationResult(address, 
                privateKey, 
                addressContext: new Base58String(addressCtx.ToJson())));
        }

        public Task<CreateAddressTagResponse> CreateAddressTagAsync(string address, CreateAddressTagRequest request)
        {
            throw new OperationNotSupportedException();
        }
    }
}

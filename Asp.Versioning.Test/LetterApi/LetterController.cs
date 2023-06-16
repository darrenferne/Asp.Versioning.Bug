using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asp.Versioning.Test.LetterApi
{
    public class LetterController : ODataController
    {
        private static readonly char[] Letters = new[]
        {
            'T','h','e','Q','u','i','c','k','B','r','o','w','n','F','o','x','J','u','m','p','e','d','O','v','e','r','T','h','e','L','a','z','y','D','o','g'
        };


        public IEnumerable<LetterType> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new LetterType
            {
                Id = index,
                Letter = Letters[Random.Shared.Next(Letters.Length)]
            })
            .ToArray();
        }
    }
}
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asp.Versioning.Test.WordApi
{
    public class WordController : ODataController
    {
        private static readonly string[] Words = new[]
        {
            "The", "Quick", "Brown", "Fox", "Jumped", "Over", "The", "Lazy", "Dog"
        };

        
        public IEnumerable<WordType> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WordType
            {
                Id = index,
                Word = Words[Random.Shared.Next(Words.Length)]
            })
            .ToArray();
        }
    }
}
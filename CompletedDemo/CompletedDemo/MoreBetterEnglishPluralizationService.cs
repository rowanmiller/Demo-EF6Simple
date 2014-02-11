using System.Data.Entity.Infrastructure.Pluralization;

namespace CompletedDemo
{
    public class MoreBetterEnglishPluralizationService : IPluralizationService
    {
        private EnglishPluralizationService _service = new EnglishPluralizationService();

        public string Pluralize(string word)
        {
            if (word.EndsWith("Status"))
            {
                return word + "es";
            }

            return _service.Pluralize(word);
        }

        public string Singularize(string word)
        {
            return _service.Singularize(word);
        }
    }

}

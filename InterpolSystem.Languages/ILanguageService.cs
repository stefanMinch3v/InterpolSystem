using InterpolSystem.Languages.Models;
using System.Collections.Generic;

namespace InterpolSystem.Languages
{
    /// <summary>
    /// fake service
    /// </summary>
    public interface ILanguageService
    {
        IReadOnlyCollection<Country> GetCountries();

        IReadOnlyCollection<Continent> GetContinents();

        IReadOnlyCollection<Language> GetLanguages();
    }
}

namespace InterpolSystem.Common.Mapping
{
    using AutoMapper;

    /// <summary>
    /// custom configuration - describes the binding configs for the specific properties
    /// </summary>
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}

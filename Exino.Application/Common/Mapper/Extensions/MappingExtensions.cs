namespace Exino.Application.Common.Mapper.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination Map<TDestination>(this object source)
        {
            //use AutoMapper for mapping objects
            return AutoMapperConfiguration.Mapper.Map<TDestination>(source);
        }
    }
}

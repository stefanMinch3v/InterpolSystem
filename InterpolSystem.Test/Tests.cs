namespace InterpolSystem.Test
{
    using AutoMapper;
    using InterpolSystem.Common.Mapping;
    using InterpolSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void InitializeAutoMapper()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }

        public static InterpolDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<InterpolDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options; // create everytime unique name cuz if the name is equal to all methods it will share the database between them

            return new InterpolDbContext(dbOptions);
        }
    }
}

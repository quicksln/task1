using System;
using System.Linq;

namespace QS.Task2.Infrastructure.Interfaces
{
    public interface ISeedingService
    {
        /// <summary>
        /// Seeds the database with initial data if the configuration is not already seeded.
        /// </summary>
        void Seed();
    }
}

using Microsoft.Extensions.Diagnostics.HealthChecks;
using AdventureServer.Models;

namespace AdventureServer.Interfaces
{
    public interface IGetFortune
    {
        public Fortune ReturnRandomFortune();

        public Fortune ReturnTimeBasedFortune();

        public Fortune ReturnFortuneById(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRover.Logic
{
    public interface IDailyImageAPIOperator
    {
        Task<List<Uri>> GetStoreDailyImageAsync(DateTime date);
    }
}
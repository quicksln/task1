using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.ViewModels;
using System;
using System.Linq;

namespace QS.Task2.Services.Mappers
{
    public static class WindValuesMapper
    {
        public static WindValuesViewModel ToViewModel(WindValues windValues)
        {
            return new WindValuesViewModel
            {
                Country = windValues.Country,
                MaxWindSpeed = windValues.MaxWindSpeed,
            };
        }

        public static WindValues ToEntity(WindValuesViewModel viewModel)
        {
            return new WindValues
            {
                Country = viewModel.Country,
                MaxWindSpeed = viewModel.MaxWindSpeed
            };
        }
    }
}

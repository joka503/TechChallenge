using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TechChallenge.Core.Services
{
    public interface INavigationService
    {
        void Initialize(NavigationPage navigationPage);

        void NavigateTo(string pageKey);

        void Configure(string pageKey, Type pageType);
    }
}

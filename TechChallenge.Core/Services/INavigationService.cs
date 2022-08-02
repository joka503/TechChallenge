using System;
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

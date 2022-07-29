using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TechChallenge.Core.Services
{
    public interface INavigationService
    {
        void Initialize(NavigationPage navigationPage);

        void GoBack();

        bool CanGoBack();

        void NavigateTo(string pageKey);

        void NavigateTo(string pageKey, object parameter);

        void NavigateTo(string pageKey, object parameter1, object parameter2, object parameter3 = null,
            object parameter4 = null, object parameter5 = null, object parameter6 = null);

        void Configure(string pageKey, Type pageType);

        void SetNewRoot(string pageKey);

        void PushModal(string pageKey);

        void PopModal();
    }
}

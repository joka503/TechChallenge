using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechChallenge.Core.Services;
using Xamarin.Forms;

namespace TechChallenge.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;

        public void Initialize(NavigationPage navigationPage)
        {
            _navigation = navigationPage;
        }

        public void NavigateTo(string pageKey)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];
                    var constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());
                    if (constructor == null)
                    {
                        var exceptionMessage = $"No suitable constructor found for page {pageKey}";
                        throw new InvalidOperationException(exceptionMessage);
                    }

                    var page = constructor.Invoke(null) as Page;
                    _navigation?.PushAsync(page, true);
                }
                else
                {
                    var exceptionMessage = $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?";
                    throw new ArgumentException(exceptionMessage, nameof(pageKey));
                }
            }
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }
    }
}

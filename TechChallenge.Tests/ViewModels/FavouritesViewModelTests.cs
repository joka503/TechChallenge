using System.Text.Json;
using Moq;
using TechChallenge.Core.Config;
using TechChallenge.Core.Models;
using TechChallenge.Core.Services;
using TechChallenge.Core.ViewModels;

namespace TechChallenge.Tests.ViewModels
{
    public class FavouritesViewModelTests
    {
        private FavouritesViewModel _sut;
        private Mock<IDialogService> _dialogServiceMock;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IPreferencesService> _preferencesServiceMock;


        [SetUp]
        public void Setup()
        {
            _dialogServiceMock = new Mock<IDialogService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _preferencesServiceMock = new Mock<IPreferencesService>();

            _sut = new FavouritesViewModel(_dialogServiceMock.Object, _navigationServiceMock.Object, _preferencesServiceMock.Object);
        }

        [Test]
        public void Test_OnAppearingCommand_LoadComicsList()
        {
            // Given: OnAppearingCommand gets executed
            // When: Page is loading comics list
            // Then: Should set the lsitview with list of comics

            _preferencesServiceMock.Setup(x => x.GetPreference(It.IsAny<string>())).
                Returns(JsonSerializer.Serialize(new List<Comic>() { new Comic() { Title = "Comic 1", Id = 1 }, new Comic() { Title = "Comic 2", Id = 2 } }));

            _sut.OnAppearingCommand.Execute(null);

            Assert.That(_sut.ComicsList != null && _sut.ComicsList.Count == 2);
        }

        [Test]
        public void Test_ComicSelectedCommand_NavigateToNextPage()
        {
            // Given: ComicSelectedCommand gets executed
            // When: Comic is selected
            // Then: Should navigate to detail page of selected comic

            var selectedComic = new Comic() { Title = "Comic 1" };
            _sut.SelectedComic = selectedComic;

            _sut.ComicSelectedCommand.Execute(null);

            _navigationServiceMock.Verify(x => x.NavigateTo(Constants.SelectedComicPageKey), Times.Once());
        }
    }
}

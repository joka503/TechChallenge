using System.Text.Json;
using Moq;
using TechChallenge.Core.Models;
using TechChallenge.Core.Services;
using TechChallenge.Core.ViewModels;

namespace TechChallenge.Tests.ViewModels
{
    public class SelectedComicViewModelTests
    {
        private SelectedComicPageViewModel _sut;
        private Mock<IDialogService> _dialogServiceMock;
        private Mock<IPreferencesService> _preferencesServiceMock;

        [SetUp]
        public void Setup()
        {
            _dialogServiceMock = new Mock<IDialogService>();
            _preferencesServiceMock = new Mock<IPreferencesService>();

            _sut = new SelectedComicPageViewModel( _dialogServiceMock.Object, _preferencesServiceMock.Object);
        }

        [Test]
        public void Test_OnAppearingCommand_SelectedComicIsFavourite_ShouldSetButtonTextToRemove()
        {
            // Given: OnAppearingCommand gets executed
            // When: Page is loading selected comic info
            // Then: Should set the favourites button description

            _sut.SelectedComic = new Comic() { Title = "Comic 1", Id = 1};
            _sut.ButtonText = string.Empty;

            _preferencesServiceMock.Setup(x => x.GetPreference(It.IsAny<string>())).
                Returns(JsonSerializer.Serialize(new List<Comic>() { new Comic() { Title = "Comic 1", Id = 1 }, new Comic() { Title = "Comic 2", Id = 2 } }));

            _sut.OnAppearingCommand.Execute(null);

            Assert.That(_sut.ButtonText == "Remove from Favourites");
        }

        [Test]
        public void Test_OnAppearingCommand_SelectedComicIsNotFavourite_ShouldSetButtonTextToAdd()
        {
            // Given: OnAppearingCommand gets executed
            // When: Page is loading selected comic info
            // Then: Should set the favourites button description

            _sut.SelectedComic = new Comic() { Title = "Comic 5", Id = 5 };
            _sut.ButtonText = string.Empty;

            _preferencesServiceMock.Setup(x => x.GetPreference(It.IsAny<string>())).
                Returns(JsonSerializer.Serialize(new List<Comic>() { new Comic() { Title = "Comic 1", Id = 1 }, new Comic() { Title = "Comic 2", Id = 2 } }));

            _sut.OnAppearingCommand.Execute(null);

            Assert.That(_sut.ButtonText == "Add to Favourites");
        }

        [Test]
        public void Test_FavoritesCommand_NoFavourites_ShouldAdd()
        {
            // Given: There are no favourites
            // When: FavoritesCommand gets executed
            // Then: Should add selected comic to favourites list

            _sut.SelectedComic = new Comic() { Title = "Comic 5", Id = 5 };

            _preferencesServiceMock.Setup(x => x.GetPreference(It.IsAny<string>())).
                Returns(string.Empty);

            _sut.FavoritesCommand.Execute(null);

            _preferencesServiceMock.Verify(t=>t.SetPreference(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

            _dialogServiceMock.Verify(m => m.ShowAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.That(_sut.ButtonText == "Remove from Favourites");
        }

        [Test]
        public void Test_FavoritesCommand_HasFavourites_ShouldAdd()
        {
            // Given: There are favourites
            // When: FavoritesCommand gets executed
            // Then: Should add selected comic to favourites list

            _sut.SelectedComic = new Comic() { Title = "Comic 5", Id = 5 };

            _preferencesServiceMock.Setup(x => x.GetPreference(It.IsAny<string>())).
                Returns(JsonSerializer.Serialize(new List<Comic>() { new Comic() { Title = "Comic 1", Id = 1 }, new Comic() { Title = "Comic 2", Id = 2 } }));

            _sut.FavoritesCommand.Execute(null);

            _preferencesServiceMock.Verify(t => t.SetPreference(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

            _dialogServiceMock.Verify(m => m.ShowAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.That(_sut.ButtonText == "Remove from Favourites");
        }

        [Test]
        public void Test_FavoritesCommand_HasFavourites_ShouldRemove()
        {
            // Given: There are favourites
            // When: FavoritesCommand gets executed
            // Then: Should remove selected comic from favourites list

            _sut.SelectedComic = new Comic() { Title = "Comic 5", Id = 5 };

            _preferencesServiceMock.Setup(x => x.GetPreference(It.IsAny<string>())).
                Returns(JsonSerializer.Serialize(new List<Comic>() { new Comic() { Title = "Comic 5", Id = 5 }, new Comic() { Title = "Comic 2", Id = 2 } }));

            _sut.FavoritesCommand.Execute(null);

            _preferencesServiceMock.Verify(t => t.SetPreference(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

            _dialogServiceMock.Verify(m => m.ShowAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.That(_sut.ButtonText == "Add to Favourites");
        }
    }
}

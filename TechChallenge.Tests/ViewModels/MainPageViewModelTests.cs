using TechChallenge.Core.Services;
using TechChallenge.Core.ViewModels;
using Moq;
using TechChallenge.Core.Models;
using TechChallenge.Core.Models.Base;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using TechChallenge.Core.Config;
using TechChallenge.Core.Utils;

namespace TechChallenge.Tests.ViewModels
{
    public class MainPageViewModelTests
    {

        private MainPageViewModel _sut;
        private Mock<IMarvelService> _marvelServiceMock;
        private Mock<IDialogService> _dialogServiceMock;
        private Mock<INavigationService> _navigationServiceMock;


        [SetUp]
        public void Setup()
        {
            _marvelServiceMock = new Mock<IMarvelService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _navigationServiceMock = new Mock<INavigationService>();

            _sut = new MainPageViewModel(_marvelServiceMock.Object, _dialogServiceMock.Object, _navigationServiceMock.Object);
        }

        [Test]
        public void Test_GetMarvelComicsCommand_HappyPath()
        {
            // Given: GetMarvelComicsCommand gets executed
            // When: Api returns comics results
            // Then: We should populate comics list of type ObservableCollection

            _marvelServiceMock.Setup(x => x.GetMarvelComicsAsync(It.IsAny<QueryParams>())).Returns(Task.FromResult(new HttpResponse<Comic>()
            {
                Data = new Pagination<Comic>
                {
                    Results = new List<Comic>
                    { new Comic()
                            { Title = "Comic 1" }, new Comic(){ Title = "Comic 2"}
                    }, Count = 2
                }
            }));

            var expectecComicsList = new ObservableCollection<Comic>(new List<Comic>(){ new Comic()
                            { Title = "Comic 1" }, new Comic(){ Title = "Comic 2"}
                    });

            _sut.GetMarvelComicsCommand.Execute(null);
            _marvelServiceMock.Verify(m => m.GetMarvelComicsAsync(It.IsAny<QueryParams>()), Times.Once());

            Assert.That(_sut.ComicsList[0].Title, Is.EqualTo(expectecComicsList[0].Title));
            Assert.That(_sut.ComicsList[1].Title, Is.EqualTo(expectecComicsList[1].Title));
        }

        [Test]
        public void Test_GetMarvelComicsCommand_Fails()
        {
            // Given: GetMarvelComicsCommand gets executed
            // When: Api returns error
            // Then: Should show an Alert

            _marvelServiceMock.Setup(x => x.GetMarvelComicsAsync(It.IsAny<QueryParams>())).Throws(new Exception("An error was found!"));

            _sut.GetMarvelComicsCommand.Execute(null);
            _marvelServiceMock.Verify(m => m.GetMarvelComicsAsync(It.IsAny<QueryParams>()), Times.Once());

            _dialogServiceMock.Verify(m => m.ShowAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [TestCase(20,20,80, true)]
        [TestCase(40, 20, 80, true)]
        [TestCase(60, 20, 80, false)]
        [TestCase(60, 12, 73, true)]
        [TestCase(60, 12, 72, false)]
        public void Test_GetMarvelComicsCommand_Pagination_HappyPath(int offset, int count, int total, bool showButton)
        {
            // Given: GetMarvelComicsCommand gets executed
            // When: Api returns comics results
            // Then: We should populate comics list of type ObservableCollection

            _marvelServiceMock.Setup(x => x.GetMarvelComicsAsync(It.IsAny<QueryParams>())).Returns(Task.FromResult(new HttpResponse<Comic>()
            {
                Data = new Pagination<Comic>
                {
                    Offset = offset,
                    Count = count,
                    Total = total
                }
            }));

            _sut.GetMarvelComicsCommand.Execute(null);

            Assert.That(_sut.ShowLoadMoreButton, Is.EqualTo(showButton));
        }

        [Test]
        public void Test_ComicSelectedCommand_HappyPath()
        {
            // Given: ComicSelectedCommand gets executed
            // When: User selects comic
            // Then: Should open the page of the selected comic

            var selectedComic = new Comic(){Title = "Comic 1"};
            _sut.SelectedComic = selectedComic;

            _sut.ComicSelectedCommand.Execute(null);

            _navigationServiceMock.Verify(x=> x.NavigateTo(Constants.SelectedComicPageKey), Times.Once());
        }

        [Test]
        public void Test_SearchTextChangedCommand_SearchInputIsEmpty_ShouldClearList()
        {
            // Given: SearchTextChangedCommand gets executed
            // When: User deletes all text from search bar
            // Then: Should clear all items from the listview and hide load more button

            _sut.Search = string.Empty;
            _sut.ShowLoadMoreButton = true;
            _sut.ComicsList = new ObservableCollection<Comic>(new List<Comic>()
                { new Comic() { Title = "Comic 1" }, new Comic() { Title = "Comic 2" } });

            _sut.SearchTextChangedCommand.Execute(null);

            Assert.That(_sut.ComicsList.Count == 0);
            Assert.That(_sut.ShowLoadMoreButton == false);
        }
    }
}

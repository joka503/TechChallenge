using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using TechChallenge.Core.Models;
using TechChallenge.Core.Models.Base;

namespace TechChallenge.Core.Services
{
    public interface IMarvelService
    {
        [Get("/comics")]
        Task<HttpResponse<Comic>> GetMarvelComicsAsync(QueryParams param);

        [Get("/comics/")]
        Task<HttpResponse<Comic>> GetSingleComicAsync(QueryParams param);
    }
}

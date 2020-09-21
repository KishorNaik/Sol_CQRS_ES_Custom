using Api.Models;
using System.Threading.Tasks;

namespace Api.Business.Query.Abstracts
{
    public abstract class MovieBaseQueryAbstract
    {
        protected virtual Task<ErrorModel> NotFoundAsync()
        {
            return Task.Run(() =>
            {
                return new ErrorModel()
                {
                    Message = "Not Found",
                    StatusCode = 401
                };
            });
        }
    }
}
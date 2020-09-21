using Api.Models;
using System.Threading.Tasks;

namespace Api.Business.Command.Abstracts
{
    public abstract class MovieBaseCommandAbstract
    {
        protected virtual Task<ErrorModel> MovieExistMessageAsync()
        {
            try
            {
                return Task.Run<ErrorModel>(() =>
                {
                    return new ErrorModel()
                    {
                        Message = "Movie already exist.",
                        StatusCode = 401
                    };
                });
            }
            catch
            {
                throw;
            }
        }
    }
}
using MyProject.Shared.DataTransferObjects;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IAuthApi
    {
        [Post("/api/signup")]
        Task<ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>> SignupAsync(SginupRequestDataTransferObjects sginup); 
        
        [Post("/api/signin")]
        Task<ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>> SigninAsync(SgininRequestDataTransferObjects sginin);

    }
}

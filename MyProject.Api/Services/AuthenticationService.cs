using Microsoft.EntityFrameworkCore;
using MyProject.Api.Data;
using MyProject.Api.Data.Entities;
using MyProject.Shared.DataTransferObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace MyProject.Api.Services
{
    public class AuthenticationService(DataContext dataContext,TokenService tokenService, PasswordService passwordService )
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly TokenService _tokenService = tokenService;
        private readonly PasswordService _passwordService = passwordService;

        public async Task<ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>> SignupAsync(SginupRequestDataTransferObjects sginup) {
            if (await _dataContext.Users.AsNoTracking().AnyAsync(u => u.Email == sginup.Email)) {
                return ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>.Flase("Sorry, this Email exists");
            }

            var user = new User {  Email = sginup.Email, Name= sginup.Name, Address = sginup.Address, };
        (user.Salt, user.Hash)=_passwordService.GenerateHashAndSalt(sginup.Password);

            try
            {
                await _dataContext.Users.AddAsync(user); await _dataContext.SaveChangesAsync();
                return GenerateAuthenticationResponse(user);
            }
            catch (Exception ex){
                return ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>.Flase(ex.Message);

            }

        }



        public async Task<ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>> SigninAsync(SgininRequestDataTransferObjects sginin)
        {
            var dbUser = await _dataContext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == sginin.Email);
            if (dbUser is null)
                return ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>.Flase("Sorry,This user does not exist");

            if (!_passwordService.IsEqual(sginin.Password, dbUser.Salt, dbUser.Hash))
                return ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>.Flase("Incorrect password");
            return GenerateAuthenticationResponse(dbUser);


        }

        private ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects> GenerateAuthenticationResponse(User user)
        {
            var logginUser = new LogginUser(user.Id, user.Name, user.Email, user.Address);
            var token = _tokenService.GenerateJwt(logginUser);
            var authenticationResponse = new AuthenticationResponseDataTransferObjects(logginUser, token);

            return ResultWithDataTransferObjects<AuthenticationResponseDataTransferObjects>.Success(authenticationResponse);
        }
    }
}

   


using System.Collections.Generic;

namespace HexagonLikeThomas.Hexagon
{
    public record User(int Id, string FirstName, string LastName);

    public interface IFetchUsersData
    {
        IEnumerable<User> Fetch();
    }

    public class MyUseCase
    {
        private readonly IFetchUsersData _fetchUsersData;

        public MyUseCase(IFetchUsersData fetchUsersData)
        {
            _fetchUsersData = fetchUsersData;
        }

        public IEnumerable<User> RetrieveAllUsers()
        {
            var users = _fetchUsersData.Fetch();
            return users;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HexagonLikeJuan.Hexagon
{
    public record User(int Id, string FirstName, string LastName);

    public interface IFetchUsersData
    {
        string Fetch();
    }

    public interface IMapToUsers
    {
        IEnumerable<User> Map(string usersData);
    }

    public class MapToUsersFromXml : IMapToUsers
    {
        // usersData est la représentation des users en XML
        public IEnumerable<User> Map(string usersData)
        {
            var xmlData = XDocument.Load(new StringReader(usersData));
            var users = xmlData.Descendants("User")
                                .Select(d =>
                                {
                                    var id = (int)d.Element("Id");
                                    var fullName = (string)d.Element("FullName");
                                    var firstName = fullName.Split(' ')[0];
                                    var lastName = fullName.Split(' ')[1];
                                    return new User(id, firstName, lastName);
                                });

            return users;
        }
    }

    public class MyUseCase
    {
        private readonly IMapToUsers _mapToUsers;
        private readonly IFetchUsersData _fetchUsersData;

        public MyUseCase(IMapToUsers mapToUsers, IFetchUsersData fetchUsersData)
        {
            _mapToUsers = mapToUsers;
            _fetchUsersData = fetchUsersData;
        }

        public IEnumerable<User> RetrieveAllUsers()
        {
            var usersData = _fetchUsersData.Fetch();
            var users = _mapToUsers.Map(usersData);
            return users;
        }
    }
}
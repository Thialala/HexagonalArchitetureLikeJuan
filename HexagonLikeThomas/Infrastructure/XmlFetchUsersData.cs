using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using HexagonLikeThomas.Hexagon;

namespace HexagonLikeThomas.Infrastructure
{
    public class XmlFetchUsersData : IFetchUsersData
    {
        private readonly IReadFile _fileReader;

        public XmlFetchUsersData(IReadFile fileReader)
        {
            _fileReader = fileReader;
        }

        IEnumerable<User> IFetchUsersData.Fetch()
        {
            string xmlFileContent = _fileReader.Read("Monfichier.xml");

            var xmlData = XDocument.Load(new StringReader(xmlFileContent));
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
}

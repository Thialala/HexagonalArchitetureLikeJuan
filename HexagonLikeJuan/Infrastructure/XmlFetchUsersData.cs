using System.IO;
using HexagonLikeJuan.Hexagon;

namespace HexagonLikeJuan.Infrastructure
{
    public class XmlFetchUsersData : IFetchUsersData
    {
        public string Fetch()
        {
            // Chargement depuis un fichier ou appel à une API....
            return File.ReadAllText("Monfichier.xml");
        }
    }
}

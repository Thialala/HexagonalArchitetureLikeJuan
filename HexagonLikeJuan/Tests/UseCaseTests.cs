using System.Collections.Generic;
using System.Linq;
using HexagonLikeJuan.Hexagon;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using static NSubstitute.Substitute;

namespace HexagonLikeJuan
{
    public class MyUseCaseTests
    {
        [Test]
        public void ShouldRetrieveAllUsers()
        {
            // Arrange
            var fetchUsersDataStub = For<IFetchUsersData>();
            fetchUsersDataStub.Fetch().Returns(
                @"<Users>
                      <User>
                        <Id>1</Id>
                        <FullName>Ousmane Barry</FullName>
                      </User>
                      <User>
                        <Id>2</Id>
                        <FullName>Ekrem Yilmaz</FullName>
                      </User>
                 </Users>"
                );

            var mapper = new MapToUsersFromXml();
            var useCase = new MyUseCase(mapper, fetchUsersDataStub);

            // Act
            var actualUsers = useCase.RetrieveAllUsers().ToList();

            // Assert
            var expectedUsers = new List<User>
            {
                new User(1, "Ousmane", "Barry"),
                new User(2, "Ekrem", "Yilmaz")
            };

            actualUsers.ShouldBe(expectedUsers);
        }
    }
}
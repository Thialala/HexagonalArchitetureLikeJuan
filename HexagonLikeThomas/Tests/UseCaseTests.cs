using System.Collections.Generic;
using System.Linq;
using HexagonLikeThomas.Hexagon;
using HexagonLikeThomas.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using static NSubstitute.Substitute;

namespace HexagonLikeThomas.Tests
{
    public class MyUseCaseTests
    {
        [Test]
        public void ShouldRetrieveAllUsers()
        {
            // Arrange
            var fileReaderStub = For<IReadFile>();
            fileReaderStub.Read(Arg.Any<string>()).Returns(
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

            var fetchUsersDataStub = new XmlFetchUsersData(fileReaderStub);
            var useCase = new MyUseCase(fetchUsersDataStub);

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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Moq;
using Xunit;
using corewcfservice;
using corewcfservice.ServiceReference;

namespace LookupServiceTests
{
    public class LookupServiceTests
    {
        private readonly Mock<SqlConnection> _mockConnection;
        private readonly Mock<SqlCommand> _mockCommand;
        private readonly Mock<SqlDataReader> _mockReader;
        private readonly string _connectionString = "Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True";

        public LookupServiceTests()
        {
            _mockConnection = new Mock<SqlConnection>(_connectionString);
            _mockCommand = new Mock<SqlCommand>();
            _mockReader = new Mock<SqlDataReader>();
        }

        [Fact]
        public void GetLookups_ShouldReturnAllLookups()
        {
            // Arrange
            var lookups = new List<Lookup>
            {
                new Lookup { Lookup_id = 1, Lookup_type = "Type1", Lookup_desc = "Desc1", Is_active = "Y", Createdby = "system", CreatedDATE = DateTime.Now },
                new Lookup { Lookup_id = 2, Lookup_type = "Type2", Lookup_desc = "Desc2", Is_active = "N", Createdby = "system", CreatedDATE = DateTime.Now }
            };

            _mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockReader.Object);
            _mockReader.SetupSequence(reader => reader.Read())
                .Returns(true)
                .Returns(true)
                .Returns(false);

            _mockReader.Setup(reader => reader.GetInt32(0)).Returns(lookups[0].Lookup_id);
            _mockReader.Setup(reader => reader.GetString(1)).Returns(lookups[0].Lookup_type);
            _mockReader.Setup(reader => reader.GetString(2)).Returns(lookups[0].Lookup_desc);
            _mockReader.Setup(reader => reader.GetString(3)).Returns(lookups[0].Is_active);
            _mockReader.Setup(reader => reader.GetString(4)).Returns(lookups[0].Createdby);
            _mockReader.Setup(reader => reader.GetDateTime(5)).Returns(lookups[0].CreatedDATE);

            var service = new LookupService();

            // Act
            var result = service.GetLookups();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(lookups.Count, result.Count);
        }

        [Fact]
        public void GetLookup_ShouldReturnCorrectLookup()
        {
            // Arrange
            var lookup = new Lookup { Lookup_id = 1, Lookup_type = "Type1", Lookup_desc = "Desc1", Is_active = "Y", Createdby = "system", CreatedDATE = DateTime.Now };

            _mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(_mockReader.Object);
            _mockReader.SetupSequence(reader => reader.Read())
                .Returns(true)
                .Returns(false);

            _mockReader.Setup(reader => reader.GetInt32(0)).Returns(lookup.Lookup_id);
            _mockReader.Setup(reader => reader.GetString(1)).Returns(lookup.Lookup_type);
            _mockReader.Setup(reader => reader.GetString(2)).Returns(lookup.Lookup_desc);
            _mockReader.Setup(reader => reader.GetString(3)).Returns(lookup.Is_active);
            _mockReader.Setup(reader => reader.GetString(4)).Returns(lookup.Createdby);
            _mockReader.Setup(reader => reader.GetDateTime(5)).Returns(lookup.CreatedDATE);

            var service = new LookupService();

            // Act
            var result = service.GetLookup(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(lookup.Lookup_id, result.Lookup_id);
        }

        [Fact]
        public void AddLookup_ShouldAddNewLookup()
        {
            // Arrange
            var lookup = new Lookup { Lookup_id = 1, Lookup_type = "Type1", Lookup_desc = "Desc1", Is_active = "Y", Createdby = "system", CreatedDATE = DateTime.Now };

            _mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Returns(1);

            var service = new LookupService();

            // Act
            service.AddLookup(lookup);

            // Assert
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void UpdateLookup_ShouldUpdateExistingLookup()
        {
            // Arrange
            var lookup = new Lookup { Lookup_id = 1, Lookup_type = "Type1", Lookup_desc = "Desc1", Is_active = "Y", Createdby = "system", CreatedDATE = DateTime.Now };

            _mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Returns(1);

            var service = new LookupService();

            // Act
            service.UpdateLookup(lookup);

            // Assert
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void DeleteLookup_ShouldRemoveLookup()
        {
            // Arrange
            _mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Returns(1);

            var service = new LookupService();

            // Act
            service.DeleteLookup(1);

            // Assert
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }
    }
}

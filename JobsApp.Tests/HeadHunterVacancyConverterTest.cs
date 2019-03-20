using AutoFixture;
using FluentAssertions;
using JobsApp.Dto.HeadHunter;
using JobsApp.Services.HeadHunterService.Impl;
using System;
using System.Collections.Generic;
using Xunit;

namespace JobsApp.Tests
{
    public class HeadHunterVacancyConverterTest
    {
        [Fact]
        public void Convert_Should_Return_ValidModel()
        {
            // Arrange
            var conveter = new HeadHunterVacancyConverter();
            var vacancy = new Vacancy()
            {
                Name = "TestVacancy",
                Employer = new Employer()
                {
                    Name = "TestOrganization"
                },
                Contacts = new Contacts()
                {
                    Name = "TestContact",
                    Phones = new List<Phone>()
                    {
                        new Phone
                        {
                            Country = "7",
                            City ="111",
                            Number = "111111"
                        }
                    },
                },
                Address = new Address()
                {
                    City = "Ufa",
                    Street = "Pushkina",
                    Building = "12a"
                },
                Salary = new Salary()
                {
                    From = 30000,
                    To = 50000,
                },
                Description = "TestDescription",
                Snippet = new Snippet()
                {
                    Requirement = "TestRequirement",
                    Responsibility = "TestResponsibility"
                },
                Employment = new Employment()
                {
                    Name = "Contract"
                }
            };

            // Act
            var result = conveter.Convert(vacancy);


            //Assert
            result.Name.Should().Be("TestVacancy");
            result.Organization.Should().Be("TestOrganization");
            result.ContactFullName.Should().Be("TestContact");
            result.Phone.Should().Be("+7111111111");
            result.Address.Should().Be("Ufa Pushkina 12a");
            result.SalaryFrom.Should().Be(30000);
            result.SalaryTo.Should().Be(50000);
            result.SalaryTo.Should().Be(50000);
            result.Description.Should().Be("TestDescription");
            result.Type = "Contract";
            result.Requierments = "TestRequirement";
            result.Responsibility = "TestResponsibility";
        }

        [Fact]
        public void Convert_Should_HaveValidType_IfEmpty()
        {
            // Arrange
            var fixture = new Fixture();
            var conveter = new HeadHunterVacancyConverter();
            var vacancy = fixture.Build<Vacancy>()
                .With(_ => _.Employment, null)
                .Create();

            // Act
            var result = conveter.Convert(vacancy);


            //Assert
            result.Type.Should().Be("Полная");
        }
    }
}

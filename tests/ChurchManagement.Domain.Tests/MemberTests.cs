using ChurchManagement.Domain.Entities;
using ChurchManagement.Domain.Enums;

namespace ChurchManagement.Domain.Tests;

public class MemberTests
{
    [Fact]
    public void SetSpouse_WithOppositeGender_SetsSpouseForBoth()
    {
        var john = new Member("John", "Doe", new DateOnly(1990, 1, 1), new DateOnly(2020, 1, 1), Gender.Male);
        var jane = new Member("Jane", "Smith", new DateOnly(1991, 2, 2), new DateOnly(2021, 2, 2), Gender.Female);

        john.SetSpouse(jane);

        Assert.Equal(jane, john.Spouse);
        Assert.Equal(john, jane.Spouse);
    }

    [Fact]
    public void SetSpouse_WithSameGender_ThrowsInvalidOperationException()
    {
        var alice = new Member("Alice", "Jones", new DateOnly(1992, 3, 3), new DateOnly(2022, 3, 3), Gender.Female);
        var beth = new Member("Beth", "Brown", new DateOnly(1993, 4, 4), new DateOnly(2023, 4, 4), Gender.Female);

        Assert.Throws<InvalidOperationException>(() => alice.SetSpouse(beth));
        Assert.Null(alice.Spouse);
        Assert.Null(beth.Spouse);
    }

    [Fact]
    public void UpdateGender_WithSpouseOfSameGender_ThrowsInvalidOperationException()
    {
        var john = new Member("John", "Doe", new DateOnly(1990, 1, 1), new DateOnly(2020, 1, 1), Gender.Male);
        var jane = new Member("Jane", "Smith", new DateOnly(1991, 2, 2), new DateOnly(2021, 2, 2), Gender.Female);

        john.SetSpouse(jane);

        Assert.Throws<InvalidOperationException>(() => john.UpdateGender(Gender.Female));
        Assert.Equal(Gender.Male, john.Gender);
        Assert.Equal(jane, john.Spouse);
        Assert.Equal(john, jane.Spouse);
    }
}

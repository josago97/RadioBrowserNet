namespace RadioBrowserNet.Tests;

public class StationsTests : BaseRadioBrowserClientTest
{
    [Fact]
    public async Task GetAll()
    {
        StationInfo[] stations = await Client.Stations.GetAllAsync();

        Assert.True(stations.Length > 1000);
    }

    [Fact]
    public async Task AdvancedSearch()
    {
        AdvancedSearchOptions options = new AdvancedSearchOptions()
        {
            Limit = 10,
            Order = StationOrders.Votes,
            Reverse = true,
        };

        StationInfo[] stations = await Client.Stations.AdvancedSearchAsync(options);

        Assert.True(stations.Length == 10);
        Assert.True(stations.First().Votes > stations.Last().Votes);
    }

    [Fact]
    public async Task SearchByUuid()
    {
        Guid uuid = Guid.Parse("941ef6f1-0699-4821-95b1-2b678e3ff62e");
        StationInfo[] stations = await Client.Stations.SearchByUuidAsync(uuid);

        Assert.Single(stations);
    }

    [Fact]
    public async Task SearchByUuids()
    {
        Guid uuid1 = Guid.Parse("941ef6f1-0699-4821-95b1-2b678e3ff62e");
        Guid uuid2 = Guid.Parse("eb3ea075-a22f-4289-bfa1-b030ae6cae9d");
        StationInfo[] stations = await Client.Stations.SearchByUuidAsync(new[] { uuid1, uuid2 });

        Assert.True(stations.Length == 2);
    }

    [Fact]
    public async Task SearchByName()
    {
        StationInfo[] stations = await Client.Stations.SearchByNameAsync("kiss", false);
        Assert.NotEmpty(stations);

        StationInfo[] stationsExact = await Client.Stations.SearchByNameAsync("kiss", true);
        Assert.NotEmpty(stationsExact);
    }

    [Fact]
    public async Task GetByMostClicked()
    {
        StationInfo[] stations = await Client.Stations.GetByMostClickedAsync();
        Assert.NotEmpty(stations);

        StationInfo[] stationsSingle = await Client.Stations.GetByMostClickedAsync(1);
        Assert.NotEmpty(stationsSingle);
    }

    [Fact]
    public async Task GetByMostVoted()
    {
        StationInfo[] stations = await Client.Stations.GetByMostVotedAsync();
        Assert.NotEmpty(stations);

        StationInfo[] stationsSingle = await Client.Stations.GetByMostVotedAsync(1);
        Assert.NotEmpty(stationsSingle);
    }

    [Fact]
    public async Task GetByRecentClick()
    {
        StationInfo[] stations = await Client.Stations.GetByRecentClickAsync();
        Assert.NotEmpty(stations);

        StationInfo[] stationsSingle = await Client.Stations.GetByRecentClickAsync(1);
        Assert.NotEmpty(stationsSingle);
    }

    [Fact]
    public async Task GetByLastChanges()
    {
        StationInfo[] stations = await Client.Stations.GetByLastChangesAsync();
        Assert.NotEmpty(stations);

        StationInfo[] stationsSingle = await Client.Stations.GetByLastChangesAsync(1);
        Assert.NotEmpty(stationsSingle);
    }

    [Fact]
    public async Task GetBroken()
    {
        StationInfo[] stations = await Client.Stations.GetBrokenAsync();
        Assert.NotEmpty(stations);

        uint limit = 50;
        StationInfo[] stationsLimited = await Client.Stations.GetBrokenAsync(10, limit);
        Assert.NotEmpty(stationsLimited);
        Assert.True(limit == stationsLimited.Length);
    }

    [Fact]
    public async Task GetChecks()
    {
        StationCheck[] checks = await Client.Stations.GetChecksAsync(limit: 1000);
        Assert.NotEmpty(checks);

        Guid stationUuid = Guid.Parse("941ef6f1-0699-4821-95b1-2b678e3ff62e");
        StationCheck[] stationChecks = await Client.Stations.GetChecksAsync(stationUuid);
        Assert.NotEmpty(stationChecks);
    }

    [Fact]
    public async Task GetCheckSteps()
    {
        Guid uuid1 = Guid.Parse("941ef6f1-0699-4821-95b1-2b678e3ff62e");
        Guid uuid2 = Guid.Parse("eb3ea075-a22f-4289-bfa1-b030ae6cae9d");
        StationCheckStep[] checkSteps = await Client.Stations.GetCheckStepsAsync(new[] { uuid1, uuid2 });
        Assert.NotEmpty(checkSteps);
    }
}
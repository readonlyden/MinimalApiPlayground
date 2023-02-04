using NBomber.CSharp;
using NBomber.Http.CSharp;

using var httpClient = new HttpClient();

var oldStyleApiScenario = Scenario.Create("oldstyle_api", async context =>
    {
        var request =
            Http.CreateRequest("GET", "http://localhost:5159/users/3fa85f64-5717-4562-b3fc-2c963f66afa6");

        var response = await Http.Send(httpClient, request);
        return response;
    })
    .WithLoadSimulations(
        Simulation.KeepConstant(24, TimeSpan.FromSeconds(60))
    );

var minimalApiScenario = Scenario.Create("minimal_api", async context =>
    {
        var request =
            Http.CreateRequest("GET", "http://localhost:5177/users/3fa85f64-5717-4562-b3fc-2c963f66afa6");

        var response = await Http.Send(httpClient, request);
        return response;
    })
    .WithLoadSimulations(
        Simulation.KeepConstant(24, TimeSpan.FromSeconds(60))
    );

NBomberRunner
    .RegisterScenarios(oldStyleApiScenario, minimalApiScenario)
    .Run();
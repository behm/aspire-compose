var builder = DistributedApplication.CreateBuilder(args);

// Allow aspire publish -p docker-compose to work
builder.AddDockerComposePublisher(config =>
{
    config.OutputPath = "../docker-compose";
});     // todo: this is preview in initial Aspire 9.2 release

// Allow aspire publish -p kubernetes to work
builder.AddKubernetesPublisher(config => 
{
    config.OutputPath = "../k8s";
});     // todo: this is preview in initial Aspire 9.2 release

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireComposeDemo_ApiService>("apiservice");

builder.AddProject<Projects.AspireComposeDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

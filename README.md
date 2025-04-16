# Aspire Demonstration of Publish to Docker Compose

In .NET Aspire 9.2.0, we have added the ability to publish a .NET application to Docker Compose. This allows you to easily deploy your application in a containerized environment using Docker Compose.

## Getting Started

For the most part, this is the standard .NET Aspire Starter template but upgraded to .NET Aspire 9.2.  The docker-compose publisher is a preview feature in .NET Aspire 9.2 so to use it, the `Aspire.Hosting.Docker` NuGet package had to be installed and then add this line to your AppHost project.

There is also support for a Kubernetes publisher by adding the `Aspire.Hosting.Kubernetes` NuGet Package

```csharp
builder.AddDockerComposePublisher();
```

To publish the project as a docker-compose, you need to do the following.

1. Install the .NET Aspire CLI (Preview) using the following command:
   ```bash
   dotnet tool install --global aspire.cli --prerelease
   ```

2. Run the following command to publish the project as a docker-compose:
   ```bash
   aspire publish -p docker-compose --project aspire publish -p docker-compose --project .\AspireComposeDemo.AppHost\AspireComposeDemo.AppHost.csproj
   ```

3. Locate the generated .env file and fill a value for CACHE_PASSWORD (you make up the value)

4. Assuming you already have Docker Desktop (or equivalent), you can then run 
   ```bash
   cd docker-compose
   docker compose up
   ```

5. Go to Docker Desktop and see your resources running.

---

To publish your app for Kubernetes, you can run one of the following comamands.

```bash
aspire publish -p kubernetes -o k8s --project .\AspireComposeDemo.AppHost\AspireComposeDemo.AppHost.csproj
```

You can also skip the `-p` switch, which specifies the publisher, and pick it from a list in the command-line.

```
aspire publish -o k8s --project .\AspireComposeDemo.AppHost\AspireComposeDemo.AppHost.csproj

Select a publisher:

  manifest
  docker-compose
> kubernetes
```

The `-o` switch just specifies where you want the publishing output to go so you can leave that out if you like or you can also configure it in the AppHost code as well.  The OutputPath is relative to the AppHost folder.

```charp
builder.AddDockerComposePublisher(config =>
{
    config.OutputPath = "../docker-compose";
});

builder.AddKubernetesPublisher(config => 
{
    config.OutputPath = "../k8s";
});
```

## References

- What's new in .NET Aspire 9.2
  - https://learn.microsoft.com/en-us/dotnet/aspire/whats-new/dotnet-aspire-9.2
  - Aspire CLI (Preview)
    - https://learn.microsoft.com/en-us/dotnet/aspire/whats-new/dotnet-aspire-9.2#-aspire-cli-preview
        - Install using `dotnet tool install --global aspire.cli --prerelease`
    - To publish as docker-compose, run: `aspire publish -p docker-compose`

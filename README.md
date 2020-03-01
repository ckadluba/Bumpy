[![Build Status](https://kadluba.visualstudio.com/Bumpy/_apis/build/status/ckadluba.Bumpy?branchName=master)](https://kadluba.visualstudio.com/Bumpy/_build/latest?definitionId=3&branchName=master)

# Bumpy
A simple ASP.Net Core WebAPI DDD application running in a Docker container.

## To do
* Add Blazor unit tests to Bumpy.Frontend using https://github.com/egil/bunit
* Use Flurl in Bumpy.Frontend to make code that sends HTTP requests testable.
* Write an injectable wrapper for JsonSerializer.DeserializeAsync() etc. to make code testable (separate project maybe).
* Use Kubernetes for orchestration (first in F5 environment)
* Create a release which deploys to AKS
* Repair VS Code tasks.json and launch.json to work again after DDD project restructuring
* Track all items of this list as GitHub issues within a project
* Make Docker login secure (see warning output of command)
* Add another project with a gRPC API and build another Docker container

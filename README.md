[![Build Status](https://kadluba.visualstudio.com/Bumpy/_apis/build/status/ckadluba.Bumpy?branchName=master)](https://kadluba.visualstudio.com/Bumpy/_build/latest?definitionId=3&branchName=master)

# Bumpy
A simple ASP.Net Core WebAPI application running in a Docker container.

## To do
* Make Docker login secure (see warning output of command)
* Push image to docker.io registry too
* Separate Data backend into separate project and container (namespaces: Bumpy.API, Bumpy.Domain, Bumpy.Infrastructure)
* Add another project with a gRPC API and build another Docker container
* Create a release which deploys to ACI
* Deploy all containers on AKS using Helm
* Use Azure Service Bus

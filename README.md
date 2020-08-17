[![Build Status](https://kadluba.visualstudio.com/Bumpy/_apis/build/status/ckadluba.Bumpy?branchName=master)](https://kadluba.visualstudio.com/Bumpy/_build/latest?definitionId=3&branchName=master)

# Bumpy
A simple demo application with a Blazor frontend and a REST API backend running in Docker containers on Kubernetes. It can be built, packaged and deployed to AKS using [Tye](https://github.com/dotnet/tye).

## To do
* Add Blazor unit tests to Bumpy.Frontend using [bUnit](https://github.com/egil/bunit).
* Create an Azure DevOps Release Pipeline or GiHub Action which automates AKS deployment.
* Add a gRPC backend service which runs in a separate container.

[![Build Status](https://kadluba.visualstudio.com/Bumpy/_apis/build/status/ckadluba.Bumpy?branchName=master)](https://kadluba.visualstudio.com/Bumpy/_build/latest?definitionId=2&branchName=master)

# Bumpy
A simple ASP.Net Core WebAPI application running in a Docker container.

## To do
* Create 2 builds in Azure pipelines: a validation build and a Docker build
* Validation build
    - Triggered by a commit to master
    - Runs tests
    - Collects and publishes results and code coverage
* Docker build
    - Triggered when validation build succeeds
    - Push image to docker.io registry too
* Create a release which deploys to ACS or AKS
* Program enhancements
    - Read data from Azure SQL or Cosmos DB
    - Use Azure Service Bus

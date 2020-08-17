# Tye Deploy Bumpy on AKS

## Source Preparation

Clone Bumpy from github and remove existing Dockerfiles.

```bash
git clone https://github.com/ckadluba/Bumpy.git
cd Bumpy
git checkout afb8ea575571fd05e71db475a93c68bebf4f7a0e
rm .\Bumpy.Frontend\Dockerfile
rm .\Bumpy.API.WebApi\Dockerfile
```

## Create Azure Prerequisites

1. Create Azure resource group.

   ```bash
   az group create --name tye --location westeurope
   ```

1. Create Azure container registry (ACR).

   ```bash
   az acr create --name tyekadluba --resource-group tye --sku standard --location westeurope
   ```

   Output:
   ```json
   "loginServer": "tyekadluba.azurecr.io",
   ```

1. Create Azure Kubernetes cluster (AKS) and configure it to use the newly created ACR registry.

   ```bash
   az aks create --resource-group tye --name tye --attach-acr tyekadluba
   ```

## Configure local Tooling to use ACR Registry and AKS Cluster

1. Configure local docker/tye tooling to use the newly created ACR registry.

   ```bash
   az acr login --name tyekadluba
   ```

1. Configure local kubernetes/tye tooling to use the newly created AKS cluster.

   ```bash
   az aks get-credentials --resource-group tye --name tye
   ```

## Perform Deployment using Tye

1. Add a `registry:` line in the file tye.yaml in Bumpy source root directory. Use the `loginServer:` value from the output of the `az acr create` command from before.

   ```yaml
   name: bumpy
   registry: tyekadluba.azurecr.io
   services:
   ```

1. Deploy

   ```bash
   tye deploy
   ```

2. Create RBAC role assignment for the AKS cluster service principal to read the public IP for the load balancer.

   Get the relevant info from the following command.
   ```bash
   az aks show --name tye --resource-group tye
   ```
   Output:
   ```json
   ...
   "effectiveOutboundIps": [
     {
       "id": "/subscriptions/e9ac2701-f71f-422d-915d-1318aed70186/resourceGroups/MC_tye_tye_westeurope/providers/Microsoft.Network/publicIPAddresses/e0f13d46-a660-4f9f-be7e-1564576dca2c",
   ...
     "servicePrincipalProfile": {
       "clientId": "3bcabb52-33be-4997-b5f5-9c803a3da273"
     },
   ...
   ```

   Then assign the role like this.
   ```bash
   az role assignment create --assignee "3bcabb52-33be-4997-b5f5-9c803a3da273" --role "Network Contributor" --scope "/subscriptions/e9ac2701-f71f-422d-915d-1318aed70186/resourceGroups/MC_tye_tye_westeurope"
   ```

3. Create file bumpy-lb.yaml and manually deploy it. Tye cannot create a load balancer in AKS at the moment.

   ```yaml
   apiVersion: v1
   kind: Service
   metadata:
     name: bumpy-lb
   spec:
     type: LoadBalancer
     ports:
     - port: 80
     selector:
       "app.kubernetes.io/name": bumpy-frontend
   ```

   ```bash
   kubectl apply -f bumpy-lb.yaml
   ```

# Check Deployment

## Check Services including Loadbalancer with Public IP

```bash
kubectl get service
```

Output:
```
NAME             TYPE           CLUSTER-IP     EXTERNAL-IP   PORT(S)        AGE
bumpy-frontend   ClusterIP      10.0.26.196    <none>        80/TCP         17m
bumpy-lb         LoadBalancer   10.0.23.35     40.74.6.0     80:30900/TCP   5s
bumpy-webapi     ClusterIP      10.0.149.206   <none>        80/TCP         17m
```

## Check Loadbalancer Creation and Backend Endpoint(s)

```bash
kubectl describe service bumpy-lb
```

Output:
```
Name:                     bumpy-lb
Namespace:                default
Labels:                   <none>
Annotations:              Selector:  app.kubernetes.io/name=bumpy-frontend
Type:                     LoadBalancer
IP:                       10.0.23.35
LoadBalancer Ingress:     40.74.6.0
Port:                     <unset>  80/TCP
TargetPort:               80/TCP
NodePort:                 <unset>  30487/TCP
Endpoints:                10.244.2.2:80
Session Affinity:         None
External Traffic Policy:  Cluster
Events:
  Type    Reason                Age   From                Message
  ----    ------                ----  ----                -------
  Normal  EnsuringLoadBalancer  77s   service-controller  Ensuring load balancer
  Normal  EnsuredLoadBalancer   73s   service-controller  Ensured load balancer
```

## Check Pods and their IPs

```bash
kubectl get pods -o wide
```

Output:
```
NAME                              READY   STATUS    RESTARTS   AGE   IP           NODE
   NOMINATED NODE   READINESS GATES
bumpy-frontend-6c677df5d9-6g8sb   1/1     Running   0          29m   10.244.2.2   aks-nodepool1-10248936-vmss000002   <none>           <none>
bumpy-webapi-7d8bc67d87-frp9k     1/1     Running   0          29m   10.244.1.3   aks-nodepool1-10248936-vmss000001   <none>           <none>
```

Note: bumpy-frontend-6c677df5d9-6g8sb IP address (10.244.2.2) is included in Endpoints list shown by `kubectl describe service bumpy-lb`.

## Check Pod with Tye Environment Variables for Service Discovery

```bash
kubectl describe pod bumpy-frontend-6c677df5d9-6g8sb
```

Output:
```
    Environment:
      DOTNET_LOGGING__CONSOLE__DISABLECOLORS:  true
      ASPNETCORE_URLS:                         http://*
      PORT:                                    80
      SERVICE__BUMPY-FRONTEND__PROTOCOL:       http
      SERVICE__BUMPY-FRONTEND__PORT:           80
      SERVICE__BUMPY-FRONTEND__HOST:           bumpy-frontend
      SERVICE__BUMPY-WEBAPI__PROTOCOL:         http
      SERVICE__BUMPY-WEBAPI__PORT:             80
      SERVICE__BUMPY-WEBAPI__HOST:             bumpy-webapi
```

## Check Logs from a Pod

```bash
kubectl logs bumpy-frontend-6c677df5d9-6g8sb
```

Output:
```
warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
      Storing keys in a directory '/root/.aspnet/DataProtection-Keys' that may not be persisted outside of the container. Protected data will be unavailable when container is destroyed.
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {a91c58e1-e02c-4929-b8d7-8f30f8394648} may be persisted to storage in unencrypted form.
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
Sending GetAllQuotes request to http://bumpy-webapi/
Sending GetAllQuotes request to http://bumpy-webapi/
Sending GetAllQuotes request to http://bumpy-webapi/
```

## Check Kubernetes Deployment Manifest generated by Tye

```bash
kubectl get deploy bumpy-frontend -o yaml
```

Output:
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  annotations:
    deployment.kubernetes.io/revision: "1"
    kubectl.kubernetes.io/last-applied-configuration: |
      {"apiVersion":"apps/v1","kind":"Deployment","metadata":{"annotations":{},"labels":{"app.kubernetes.io/name":"bumpy-frontend","app.kubernetes.io/part-of":"bumpy"},"name":"bumpy-frontend","namespace":"default"},"spec":{"replicas":1,"selector":{"matchLabels":{"app.kubernetes.io/name":"bumpy-frontend"}},"template":{"metadata":{"labels":{"app.kubernetes.io/name":"bumpy-frontend","app.kubernetes.io/part-of":"bumpy"}},"spec":{"containers":[{"env":[{"name":"DOTNET_LOGGING__CONSOLE__DISABLECOLORS","value":"true"},{"name":"ASPNETCORE_URLS","value":"http://*"},{"name":"PORT","value":"80"},{"name":"SERVICE__BUMPY-FRONTEND__PROTOCOL","value":"http"},{"name":"SERVICE__BUMPY-FRONTEND__PORT","value":"80"},{"name":"SERVICE__BUMPY-FRONTEND__HOST","value":"bumpy-frontend"},{"name":"SERVICE__BUMPY-WEBAPI__PROTOCOL","value":"http"},{"name":"SERVICE__BUMPY-WEBAPI__PORT","value":"80"},{"name":"SERVICE__BUMPY-WEBAPI__HOST","value":"bumpy-webapi"}],"image":"tyekadluba.azurecr.io/bumpy-frontend:1.0.0","imagePullPolicy":"Always","name":"bumpy-frontend","ports":[{"containerPort":80}]}]}}}}
  creationTimestamp: "2020-08-12T15:31:28Z"
  generation: 1
  labels:
    app.kubernetes.io/name: bumpy-frontend
    app.kubernetes.io/part-of: bumpy
...
```

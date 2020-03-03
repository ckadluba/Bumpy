docker build --rm -f Bumpy.API.WebApi\Dockerfile -t kadluba/samples/bumpy-webapi:latest .
docker build --rm -f Bumpy.Frontend\Dockerfile -t kadluba/samples/bumpy-frontend:latest .

docker-compose up
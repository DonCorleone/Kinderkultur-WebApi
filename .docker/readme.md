# Server

    docker build -t angularwebpackvisualstudio .  
    docker run -it -p 5000:80 angularwebpackvisualstudio


    docker build -f .docker/Dockerfile -t dotnetserver .
    docker run --name dotnetserver  -p 5000:80 -d dotnetserver

    https://hub.docker.com/r/microsoft/aspnetcore-build/
# Lustars is a dating app focused targeting bulgarians living in the country or abroad. 
The app is base on microservices architecture and technologies sucha as: 
  - ReactJS for front-end browser access
  - React Native for mobile app
  - ASP.NET Core 3.1 for back-end 
  - PostgreSQL and MongoDB for databases
  - RabbitMQ for messaging queue system
  - SignalR for real-time communication
  - Docker for containerizing
  - Docker compose for container development orchestration
  - Kubernetes for container production orchestration

# How to run Lustars app
1. Make sure you have installed on your machine: 
    - Docker for Windows(if you are with Window OS)
    - Docker gitbash

2. Open Gitbash in the directory where you want to have the project and put the following command to download the project:
    - git clone https://github.com/StoychoMihaylov/lustars-microservices.git

3. Go inside the project, open Powershell or CMD in the main directory of the project and execute the following commands: (Make sure Docker for Windows is started and running)
    - docker-compose build
    - docker-compose up
    
4. Go in your favorite browser and hit http://localhost:3001




# Architecture basic scheme
                                                |-----------------|                  |-------------------------|
                                                |   ReactJS app   |                  | React Native mobile app |
                                                |-----------------|                  |-------------------------|
                                                         |                                        |
                                                         |                                        |
                                                         |                                        |
                                                         ------------------------------------------
                                                                            |
                                                                            |
                                                         |-----------------------------------------|
                                                         |             WebGateway API              |-------------------|
                                                         |-----------------------------------------|                   |  
                                                                |          |                   |                       |
                                                                |     |---------|              |                       |
                         ---------------------------------------|     | SignalR |              |                       |
                         |             |                  |           |---------|              |                       |
                         |             |                  |                |                   |                       |
                         |             |                  |                |                   |                       |
             |--------------| |-----------------| |---------------| |--------------| |--------------------| |---------------------|
             |   Auth API   | |   Profile API   | |   Email API   | |   Chat API   | |  Notification API  | | Payment and billing |
             |--------------| |-----------------| |---------------| |--------------| |--------------------| |---------------------|
                    |                  |                                    |                     |                    |
                    |                  |                                    |                     |                    |
             |------------|      |------------|                       |-----------|         |-----------|        |------------|
             |            |      |            |                       |-----------|         |-----------|        |            |
             | PostgreSQL |      | PostgreSQL |                       |--MongoDB--|         |--MongoDB--|        | PostgreSQL |
             |            |      |            |                       |-----------|         |-----------|        |            |
             |------------|      |------------|                       |-----------|         |-----------|        |------------|

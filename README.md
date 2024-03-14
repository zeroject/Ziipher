
# Ziipher

Ziipher is a twitter clone, but also it is a project made for System Integration Course at EASV.

## Services
These are the services that are made specific for this project nearly all of them can be deployed independent from each other (all of them can be deployed but will not have a use case as it depends that another service does something)
### Auth
Auth Services can be deployed with everything else by running the docker compose command at root of the project. Auth Service has a dependency on the SQL Database found in the compose file, so the service can run without the database running it is not functional able to run everything other than token validation.

Auth Service has one purpose in this project and that is validation. It is connected to the Gateway as these two together validate any tokens coming their way.

The tokens used for the project is the standard JWT.
### User
User Service can be deployed with the Docker compose at the root project or be deployed as a independent Service, the User Service is using an in memory database. The choice for using in memory was for simplicity of the service so if the service crashes all data is lost.

User service is one of the services that one function has no token validation on, that is the create user as it also creates a user login for the auth service to compare to. The user service has a job to get User info if request or change this info or delete that info if needed.
### Comment
Comment Service can be deployed with docker compose at the root project or as an independent service this service also has a in memory database and will lose all data on shutdown or crash.

The comment service keeps track on all comments for each post, users can update or delete their already existing comments. Comment service can be independent but for the most part is dependent to Post as there can’t exist any comments without any posts.
### Post
Post Service can be deployed with the docker compose at root project it can also be deployed as an independent service. this service also uses a in memory database so all data will be lost on shutdown.

Post service is the service to make new post to Ziipher or update or delete posts. The service also send post to the user when requested.
### Like
Like Service can be deployed with the docker compose but also be deployed as an independent service. this service uses a in memory database so all data will be lost on shutdown.

The Like service keeps track of all the likes on each post. It is the only service with no create, the user can’t create a like as he only can like a post one time.
### Health
Health service can be deployed with docker compose or as an independent service. Health service uses a in memory database so on shutdown all data will be lost.

The health service keeps track of every other service how they are doing, it then logs all the data in its own website that can be accessed and viewed. The data being collected with a middleware is CPU Usage, Ram usage, Disk Space and Ping status.
### Timeline
Timeline service can be deployed with a docker compose or as an independent service. Timeline Service uses an in-memory database so on shutdown all data will be lost.

Timeline service is for creating a new timeline for a user.
### DM
DM Service can be deployed with a docker compose or as an independent service. DM Service uses a in memory database so on shutdown all data is lost.

DM Service has the Normal Crud operations, but it has one unique trade as once created it will use signalR to send the DM instead of having a frontend keep asking the database for new results.
### Gateway
Gateway can only be deployed with docker compose as it is dependent on every other service.
## Third Party Services
this project uses also some third-party services.
One of them is the Microsoft Azure SQL database. The second is Seq so all logging is centralized.

## How Each service works
There is a diagram for each action that can be made and what parts of services it touches. It can be found here []

## Authors

- [@Casper F](https://github.com/zeroject)
- [@Anders S](https://github.com/Ominousity)
- [@Kasper R](https://github.com/kasp441)
- [@Jens I](https://github.com/JensIssa)

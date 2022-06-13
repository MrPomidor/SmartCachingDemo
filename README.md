# SmartCachingDemo
Demo implementation using smart caching techniques (LRU priority caching, etc)


## Developers guide
In order to start testing, you need to have dependencies up and running. Dependencies are hosted in docker containers, so to run them navigate to `src/Solution` and run `docker compose up -d` to load all required images and run dependencies on required ports.

In order to use database, you should first populate it with EF Core migration. Choose `APIs/NoCache` as startup project, open Package Manager console in Visual Studio, set `APIs/Reusables` as default project, run `Update-Database` command. Filling database will take some time (several minutes).

Redis is running using custom dockerfile, which includes some configuration for setting memory limit and cache eviction policy. File is located in `src/Solution/redis-lru/redis.conf`. If you want to modify some parameters - change config file, remove old image and re-run docker compose. 
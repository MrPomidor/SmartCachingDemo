> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1620000`, fail count: `0`, all data: `738,5065` MB MB

load simulation: `inject_per_sec`, rate: `2700`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1620000`, ok = `1620000`, RPS = `2700`|
|latency|min = `0,2`, mean = `128,65`, max = `681,83`, StdDev = `85,28`|
|latency percentile|50% = `113,54`, 75% = `183,3`, 95% = `289,79`, 99% = `365,57`|
|data transfer|min = `0,308` KB, mean = `0,467` KB, max = `0,647` KB, all = `738,5065` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1620000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1620000`, fail count: `0`, all data: `740,0909` MB MB

load simulation: `inject_per_sec`, rate: `2700`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1620000`, ok = `1620000`, RPS = `2700`|
|latency|min = `0,46`, mean = `164,74`, max = `631,39`, StdDev = `83,35`|
|latency percentile|50% = `155,39`, 75% = `218,88`, 95% = `316,42`, 99% = `384,51`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,691` KB, all = `740,0909` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1620000||


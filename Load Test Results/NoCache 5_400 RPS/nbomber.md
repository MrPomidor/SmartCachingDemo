> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1620000`, fail count: `0`, all data: `738,2850` MB MB

load simulation: `inject_per_sec`, rate: `2700`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1620000`, ok = `1620000`, RPS = `2700`|
|latency|min = `4,5`, mean = `241,21`, max = `813,53`, StdDev = `102,27`|
|latency percentile|50% = `235,26`, 75% = `306,94`, 95% = `418,3`, 99% = `506,62`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `738,2850` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1620000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1620000`, fail count: `0`, all data: `740,0939` MB MB

load simulation: `inject_per_sec`, rate: `2700`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1620000`, ok = `1620000`, RPS = `2700`|
|latency|min = `4,06`, mean = `245,21`, max = `783,02`, StdDev = `106,8`|
|latency percentile|50% = `237,44`, 75% = `314,88`, 95% = `436,22`, 99% = `509,18`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,691` KB, all = `740,0939` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1620000||


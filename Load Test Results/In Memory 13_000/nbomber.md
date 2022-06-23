> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `683,7390` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `0,28`, mean = `143,02`, max = `613,35`, StdDev = `85,87`|
|latency percentile|50% = `133,38`, 75% = `200,06`, 95% = `300,8`, 99% = `365,06`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `683,7390` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `685,2068` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `0,91`, mean = `174,96`, max = `613,73`, StdDev = `84,46`|
|latency percentile|50% = `168,83`, 75% = `233,09`, 95% = `324,35`, 99% = `381,44`|
|data transfer|min = `0,3` KB, mean = `0,467` KB, max = `0,691` KB, all = `685,2068` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||


> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `683,7362` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `1,43`, mean = `140,11`, max = `723,64`, StdDev = `91,48`|
|latency percentile|50% = `119,55`, 75% = `192,77`, 95% = `324,61`, 99% = `399,1`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `683,7362` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `685,3744` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `3,61`, mean = `174,24`, max = `722,68`, StdDev = `89,42`|
|latency percentile|50% = `161,28`, 75% = `231,55`, 95% = `338,69`, 99% = `413,7`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,691` KB, all = `685,3744` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||


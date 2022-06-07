> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:03:00`, ok count: `644644`, fail count: `0`, all data: `294,0797` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `644644`, ok = `644644`, RPS = `3581,4`|
|latency|min = `1,28`, mean = `2,78`, max = `24,88`, StdDev = `0,59`|
|latency percentile|50% = `2,71`, 75% = `3`, 95% = `3,63`, 99% = `4,85`|
|data transfer|min = `0,301` KB, mean = `0,467` KB, max = `0,654` KB, all = `294,0797` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|644644||

> scenario: `Rare Requested Items`, duration: `00:03:00`, ok count: `645870`, fail count: `0`, all data: `295,1336` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `645870`, ok = `645870`, RPS = `3588,2`|
|latency|min = `1,35`, mean = `2,77`, max = `23,99`, StdDev = `0,59`|
|latency percentile|50% = `2,7`, 75% = `3`, 95% = `3,63`, 99% = `4,84`|
|data transfer|min = `0,299` KB, mean = `0,468` KB, max = `0,687` KB, all = `295,1336` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|645870||


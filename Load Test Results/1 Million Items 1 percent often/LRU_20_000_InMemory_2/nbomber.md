> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:03:00`, ok count: `4478274`, fail count: `0`, all data: `2041,3378` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `4478274`, ok = `4478274`, RPS = `24879,3`|
|latency|min = `0,15`, mean = `0,39`, max = `20,84`, StdDev = `0,59`|
|latency percentile|50% = `0,28`, 75% = `0,35`, 95% = `0,64`, 99% = `3,23`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `2041,3378` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|4478274||

> scenario: `Rare Requested Items`, duration: `00:03:00`, ok count: `520624`, fail count: `0`, all data: `237,8970` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `520624`, ok = `520624`, RPS = `2892,4`|
|latency|min = `0,19`, mean = `3,45`, max = `86,92`, StdDev = `1,4`|
|latency percentile|50% = `3,2`, 75% = `3,72`, 95% = `5,39`, 99% = `9,12`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,691` KB, all = `237,8970` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|520624||


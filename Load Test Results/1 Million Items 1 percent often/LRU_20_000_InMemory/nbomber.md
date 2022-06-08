> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:03:00`, ok count: `4523262`, fail count: `0`, all data: `2059,9272` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `4523262`, ok = `4523262`, RPS = `25129,2`|
|latency|min = `0,14`, mean = `0,39`, max = `46,76`, StdDev = `0,62`|
|latency percentile|50% = `0,28`, 75% = `0,34`, 95% = `0,6`, 99% = `3,06`|
|data transfer|min = `0,313` KB, mean = `0,466` KB, max = `0,647` KB, all = `2059,9272` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|4523262||

> scenario: `Rare Requested Items`, duration: `00:03:00`, ok count: `502181`, fail count: `0`, all data: `229,3805` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `502181`, ok = `502181`, RPS = `2789,9`|
|latency|min = `1,2`, mean = `3,57`, max = `44,94`, StdDev = `1,33`|
|latency percentile|50% = `3,34`, 75% = `3,81`, 95% = `5,43`, 99% = `8,7`|
|data transfer|min = `0,299` KB, mean = `0,467` KB, max = `0,699` KB, all = `229,3805` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|502181||


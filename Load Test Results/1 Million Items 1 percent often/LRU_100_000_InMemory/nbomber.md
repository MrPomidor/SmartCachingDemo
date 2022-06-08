> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:03:00`, ok count: `4419237`, fail count: `0`, all data: `2015,9397` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `4419237`, ok = `4419237`, RPS = `24551,3`|
|latency|min = `0,15`, mean = `0,4`, max = `33,33`, StdDev = `0,55`|
|latency percentile|50% = `0,29`, 75% = `0,37`, 95% = `0,71`, 99% = `2,93`|
|data transfer|min = `0,31` KB, mean = `0,467` KB, max = `0,645` KB, all = `2015,9397` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|4419237||

> scenario: `Rare Requested Items`, duration: `00:03:00`, ok count: `563590`, fail count: `0`, all data: `257,4232` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `563590`, ok = `563590`, RPS = `3131,1`|
|latency|min = `1,14`, mean = `3,18`, max = `35,58`, StdDev = `1,54`|
|latency percentile|50% = `2,81`, 75% = `3,39`, 95% = `5,51`, 99% = `10,32`|
|data transfer|min = `0,299` KB, mean = `0,467` KB, max = `0,699` KB, all = `257,4232` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|563590||


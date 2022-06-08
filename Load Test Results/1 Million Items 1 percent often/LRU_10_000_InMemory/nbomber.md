> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:03:00`, ok count: `623321`, fail count: `0`, all data: `285,0499` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `623321`, ok = `623321`, RPS = `3462,9`|
|latency|min = `1,32`, mean = `2,87`, max = `24,84`, StdDev = `0,7`|
|latency percentile|50% = `2,77`, 75% = `3,08`, 95% = `3,84`, 99% = `5,77`|
|data transfer|min = `0,312` KB, mean = `0,468` KB, max = `0,657` KB, all = `285,0499` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|623321||

> scenario: `Rare Requested Items`, duration: `00:03:00`, ok count: `624084`, fail count: `0`, all data: `285,0736` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `624084`, ok = `624084`, RPS = `3467,1`|
|latency|min = `1,33`, mean = `2,87`, max = `23,29`, StdDev = `0,7`|
|latency percentile|50% = `2,77`, 75% = `3,08`, 95% = `3,83`, 99% = `5,76`|
|data transfer|min = `0,298` KB, mean = `0,467` KB, max = `0,699` KB, all = `285,0736` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|624084||


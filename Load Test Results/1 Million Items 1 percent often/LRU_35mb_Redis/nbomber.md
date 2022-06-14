> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:03:00`, ok count: `1173964`, fail count: `0`, all data: `535,0408` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1173964`, ok = `1173964`, RPS = `6522`|
|latency|min = `0,81`, mean = `1,52`, max = `27,05`, StdDev = `0,73`|
|latency percentile|50% = `1,34`, 75% = `1,47`, 95% = `3,6`, 99% = `4,58`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `535,0408` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1173964||

> scenario: `Rare Requested Items`, duration: `00:03:00`, ok count: `440636`, fail count: `0`, all data: `201,2668` MB MB

load simulation: `keep_constant`, copies: `10`, during: `00:03:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `440636`, ok = `440636`, RPS = `2448`|
|latency|min = `0,78`, mean = `4,07`, max = `24,62`, StdDev = `0,89`|
|latency percentile|50% = `4,08`, 75% = `4,33`, 95% = `4,94`, 99% = `6,63`|
|data transfer|min = `0,3` KB, mean = `0,467` KB, max = `0,691` KB, all = `201,2668` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|440636||


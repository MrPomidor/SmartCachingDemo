> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `683,7294` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `0,23`, mean = `145,79`, max = `628,27`, StdDev = `85,81`|
|latency percentile|50% = `135,55`, 75% = `202,88`, 95% = `302,59`, 99% = `366,59`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `683,7294` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `685,2936` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `0,46`, mean = `169,11`, max = `650,08`, StdDev = `83,67`|
|latency percentile|50% = `160,64`, 75% = `224,13`, 95% = `320,77`, 99% = `379,39`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,674` KB, all = `685,2936` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||


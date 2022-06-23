> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `683,7434` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `0,22`, mean = `115,66`, max = `628,79`, StdDev = `75,53`|
|latency percentile|50% = `102,27`, 75% = `164,35`, 95% = `258,18`, 99% = `321,02`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `683,7434` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `685,3186` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `0,36`, mean = `145,96`, max = `594,72`, StdDev = `74,63`|
|latency percentile|50% = `137,73`, 75% = `195,46`, 95% = `280,06`, 99% = `335,1`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,691` KB, all = `685,3186` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||


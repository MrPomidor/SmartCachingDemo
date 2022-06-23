> test suite: `nbomber_default_test_suite_name`

> test name: `nbomber_default_test_name`

> scenario: `Often Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `683,7374` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Often Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `4,12`, mean = `194,93`, max = `689,65`, StdDev = `80,35`|
|latency percentile|50% = `192,9`, 75% = `245,63`, 95% = `331,26`, 99% = `394,75`|
|data transfer|min = `0,308` KB, mean = `0,466` KB, max = `0,647` KB, all = `683,7374` MB|
> status codes for scenario: `Often Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||

> scenario: `Rare Requested Items`, duration: `00:10:00`, ok count: `1500000`, fail count: `0`, all data: `685,2643` MB MB

load simulation: `inject_per_sec`, rate: `2500`, during: `00:10:00`
|step|ok stats|
|---|---|
|name|`Get Rare Requested Items`|
|request count|all = `1500000`, ok = `1500000`, RPS = `2500`|
|latency|min = `4,11`, mean = `196`, max = `748,13`, StdDev = `81,77`|
|latency percentile|50% = `194,43`, 75% = `248,83`, 95% = `334,34`, 99% = `396,29`|
|data transfer|min = `0,3` KB, mean = `0,468` KB, max = `0,691` KB, all = `685,2643` MB|
> status codes for scenario: `Rare Requested Items`

|status code|count|message|
|---|---|---|
|200|1500000||


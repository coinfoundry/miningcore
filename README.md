
[![Docker Build Statu](https://img.shields.io/docker/build/calebcall/miningcore-docker.svg)](https://hub.docker.com/r/calebcall/miningcore-docker/)
[![Docker Stars](https://img.shields.io/docker/stars/calebcall/miningcore-docker.svg)](https://hub.docker.com/r/calebcall/miningcore-docker/)
[![Docker Pulls](https://img.shields.io/docker/pulls/calebcall/miningcore-docker.svg)]()


## Miningcore

Miningcore a the multi-currency stratum-engine.

Even though the pool engine can be used to run a production-pool, doing so currently requires to
develop your own website frontend talking to the pool's API-Endpoint at http://127.0.0.1:4000.
This is going to change in the future.

### Features

- Supports clusters of pools each running individual currencies
- Ultra-low-latency Stratum implementation using asynchronous I/O (LibUv)
- Adaptive share difficulty ("vardiff")
- PoW validation (hashing) using native code for maximum performance
- Session management for purging DDoS/flood initiated zombie workers
- Payment processing
- Banning System for banning peers that are flooding with invalid shares
- Live Stats API on Port 4000
- POW (proof-of-work) & POS (proof-of-stake) support
- Detailed per-pool logging to console & filesystem
- Runs on Linux and Windows

### Algorithms

Algo | Implemented | Tested | Notes
:--- | :---: | :---: | :---:
sha256S  | Yes | Yes |
sha256D | Yes | Yes |
sha256DReverse | Yes | Yes |
x11 | Yes | Yes |
blake2s | Yes | Yes |
x17 | Yes | Yes |
x16r | Yes | Yes |
x16s | Yes | Yes |
groestl | Yes | Yes |
lyra2Rev2 | Yes | Yes |
lyra2z | Yes | Yes |
scrypt | Yes | Yes |
skein | Yes | Yes |
qubit | Yes | Yes |
groestlMyriad | Yes | Yes |
NeoScrypt | Yes | Yes |
DigestReverser(vergeblockhasher) | Yes | Yes |

### Coins

Coin | Implemented | Tested | Planned | Notes | Website
:--- | :---: | :---: | :---: | :---: | :---:
Bitcoin | Yes | Yes | | |
Litecoin | Yes | Yes | | |
Zcash | Yes | Yes | | |
Monero | Yes | Yes | | |
Ethereum | Yes | Yes | | Requires [Parity](https://github.com/paritytech/parity/releases) |
Ethereum Classic | Yes | Yes | | Requires [Parity](https://github.com/paritytech/parity/releases) |
Expanse | Yes | Yes | | - **Not working for Byzantinium update**<br>- Requires [Parity](https://github.com/paritytech/parity/releases) |
DASH | Yes | Yes | | |
Bitcoin Gold | Yes | Yes | | |
Bitcoin Cash | Yes | Yes | | |
Vertcoin | Yes | Yes | | |
Monacoin | Yes | Yes | | |
Globaltoken | Yes | Yes | | Requires [GLT Daemon](https://globaltoken.org/#downloads) |
Ellaism | Yes | Yes | | Requires [Parity](https://github.com/paritytech/parity/releases) |
Groestlcoin | Yes | Yes | | | 
Dogecoin | Yes | No | | |
DigiByte | Yes | Yes | | |
Namecoin | Yes | No | | |
Viacoin | Yes | No | | |
Peercoin | Yes | No | | |
Straks | Yes | Yes | | |
Electroneum | Yes | Yes | | |
MoonCoin | Yes | Yes | | |
Ravencoin | Yes | Yes | | | https://ravencoin.org 
Pigeoncoin | Yes | No | | |
Actinium | Yes | Yes | | |
CrowdCoin | Yes | No | | |
Help The Homeless | Yes | Yes | | |
Gincoin | Yes | Yes | | |
REDEN | Yes | Yes | | | https://www.reden.io

#### Ethereum

Miningcore implements the [Ethereum stratum mining protocol](https://github.com/nicehash/Specifications/blob/master/EthereumStratum_NiceHash_v1.0.0.txt) authored by NiceHash. This protocol is implemented by all major Ethereum miners.

- Claymore Miner must be configured to communicate using this protocol by supplying the <code>-esm 3</code> command line option
- Genoil's ethminer must be configured to communicate using this protocol by supplying the <code>-SP 2</code> command line option

#### ZCash

- Pools needs to be configured with both a t-addr and z-addr (new configuration property "z-address" of the pool configuration element)
- First configured zcashd daemon needs to control both the t-addr and the z-addr (have the private key)
- To increase the share processing throughput it is advisable to increase the maximum number of concurrent equihash solvers through the new configuration property "equihashMaxThreads" of the cluster configuration element. Increasing this value by one increases the peak memory consumption of the pool cluster by 1 GB.
- Miners may use both t-addresses and z-addresses when connecting to the pool

### Runtime Requirements

- [.Net Core 2.0 Runtime](https://www.microsoft.com/net/download/core#/runtime)
- [PostgreSQL Database](https://www.postgresql.org/)
- Coin Daemon (per pool)

### PostgreSQL Database setup

Create the database:

```console
$ createuser miningcore
$ createdb miningcore
$ psql (enter the password for postgres)
```

Run the query after login:

```sql
alter user miningcore with encrypted password 'some-secure-password';
grant all privileges on database miningcore to miningcore;
```

Import the database schema:

```console
$ wget https://raw.githubusercontent.com/coinfoundry/miningcore/master/src/MiningCore/Persistence/Postgres/Scripts/createdb.sql
$ psql -d miningcore -U miningcore -f createdb.sql
```

### [Configuration](https://github.com/calebcall/miningcore/wiki/Configuration)

### [API](https://github.com/coinfoundry/calebcall/wiki/API) 

### Docker

The [miningcore docker image](https://hub.docker.com/r/calebcall/miningcore-docker/) expects a valid pool configuration file as volume argument:

```console
$ docker run -d -p 3032:3032 -p 80:80 -v /path/to/config.json:/config.json:ro calebcall/miningcore-docker
```

You also need to expose all stratum ports specified in your configuration file.  The swagger api documentation will be available on port 80.

### Building from Source (Shell)

Install the [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core) for your platform

#### Linux (Ubuntu example)

```console
$ apt-get update -y 
$ apt-get -y install git cmake build-essential libssl-dev pkg-config libboost-all-dev libsodium-dev
$ git clone https://github.com/calebcall/miningcore
$ cd miningcore/src/MiningCore
$ ./linux-build.sh
```

#### Windows

```dosbatch
> git clone https://github.com/calebcall/miningcore
> cd miningcore/src/MiningCore
> windows-build.bat
```

#### After successful build

Now copy `config.json` to `../../build`, edit it to your liking and run:

```
cd ../../build
dotnet MiningCore.dll -c config.json
```

### Building from Source (Visual Studio)

- Install [Visual Studio 2017](https://www.visualstudio.com/vs/) (Community Edition is sufficient) for your platform
- Install [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core) for your platform
- Open `MiningCore.sln` in VS 2017

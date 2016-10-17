#r "../packages/FSharp.Data.2.3.2/lib/net40/FSharp.Data.dll"
#r "C:/Code/FSharp/Terminus/Terminus/Terminus.Domain.Common/bin/Debug/Terminus.Domain.Common.dll"

#load "DomainTypes.fs"
#load "JsonParser.fs"

open System
open System.Linq
open FSharp.Data
open FSharp.Data.JsonExtensions
open Terminus.Domain.Common 
open Terminus.Leaderboard.Domain.JsonParser
open Terminus.Domain.Leaderboard 

let leaderboard = "https://us.api.battle.net/data/d3/era/7/leaderboard/rift-team-2?access_token=ux4q8cw2xcqpr6wqz5jh4ed5"

let getData url = 
    async { let! data = Http.AsyncRequestString(url);
            return data 
    }

let jsonData = Async.RunSynchronously(getData leaderboard)
let entries  = parseLeaderBoardData jsonData 

let printResults filename =
    use file = System.IO.File.CreateText(filename)
    for e in entries do
        fprintfn file "%A" e

printResults @"C:\Code\FSharp\Terminus\Terminus\test.txt"

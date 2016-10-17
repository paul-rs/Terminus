[<AutoOpen>]
module Terminus.Leaderboard.Domain.JsonParser

open System
open System.Linq
open System.Collections.Generic
open FSharp.Data
open FSharp.Data.JsonExtensions
open Terminus.Domain.Common
open Terminus.Domain.Leaderboard

let private flattenNodes records =
    records |> 
    Seq.map (fun (r: JsonValue) -> r.GetProperty("player").AsArray().[0].GetProperty("data"), 
                                   r.GetProperty("data"))

let private toDictionary (node: JsonValue) = 
    dict [for n in node.AsArray() -> (snd n.Properties.[0]).AsString(), (snd n.Properties.[1]).AsString()]

let private toPlayer player = 
    let lookup = toDictionary player
    { 
        tag       = lookup.["HeroBattleTag"]
        heroClass = lookup.["HeroClass"]
        gender    = lookup.["HeroGender"]
        paragon   = int lookup.["ParagonLevel"]
        clan      = if lookup.ContainsKey("HeroClanTag") then 
                       Some { tag = lookup.["HeroClanTag"]; name = lookup.["ClanName"] };
                    else None
    }

let private toRiftClear node = 
    let lookup = toDictionary node
    {
        level     = int lookup.["RiftLevel"]
        duration  = int lookup.["RiftTime"]
        clearTime = float lookup.["CompletedTime"] |> fromUnixTime
        rank      = if lookup.ContainsKey("Rank") then 
                       Some (int lookup.["Rank"])
                    else None
    }   

let private toLeaderBoardEntry (playerNode, riftClearNode) =
    { 
        player    = toPlayer playerNode
        riftClear = toRiftClear riftClearNode
    }

let parseLeaderBoardData jsonData =
    JsonValue.Parse(jsonData).GetProperty("row").AsArray() |>
    flattenNodes |>
    Seq.map (fun a -> toLeaderBoardEntry a)

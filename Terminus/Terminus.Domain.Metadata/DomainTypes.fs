namespace Terminus.Domain.Metadata
open System

[<CLIMutable>]        
type Schedule = {
    region      :string
    eraIndex    :int
    seasonIndex :int 
}

[<CLIMutable>]
type Leaderboard = {
    region     :string
    baseUrl    :string
    index      :int
    identifier :string
}

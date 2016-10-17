namespace Terminus.Domain.Leaderboard 
    open System

    [<CLIMutable>]        
    type Clan = { tag: string; name: string; }

    [<CLIMutable>]
    type Player = { 
        tag        :string
        heroClass  :string
        gender     :string
        paragon    :int
        clan       :Clan option 
    }

    [<CLIMutable>]
    type RiftClear = {
        level     :int
        duration  :int
        clearTime :DateTime
        rank      :int option 
    }

    [<CLIMutable>]
    type LeaderBoardEntry = { player: Player; riftClear: RiftClear }           
[<CompilationRepresentationAttribute(CompilationRepresentationFlags.ModuleSuffix)>]
module Terminus.Domain.Common 
    open System

    let fromUnixTime ms = 
        let epoch = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc)
        epoch.AddMilliseconds ms


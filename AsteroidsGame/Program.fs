﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    use game = new AsteroidsGame()
    game.Run()
    0 // return an integer exit code

module State

open Microsoft.Xna.Framework.Graphics

type State<'a, 's> = 's -> 'a* 's

let ret a : State<'a, 's> = fun s -> (a, s)

let bind p k : State<'b, 's> =
  fun s ->
    let a, s' = p s
    k a s'

type StateBuilder() =
  member this.Return a = ret a
  member this.ReturnFrom a : State<'a, 's> = a
  member this.Bind p k = bind p k
let st = StateBuilder()

type GameState<'a> =
  {
    Players : Player list
    Asteroids : Asteroid list
    Projectiles : Projectile list
    Textures : (string * Option<Texture2D>) list
  }

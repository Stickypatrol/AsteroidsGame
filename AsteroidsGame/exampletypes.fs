module exampletypes

(*
    Records for all types
*)
module Types
open Math
open StateMonad
open Microsoft.Xna.Framework.Input
open Input

type MouseAction =
  | LeftButton = 0
  | MiddleButton = 1
  | RightButton = 2

type KeyboardAction =
  | KeyW = 0
  | KeyS = 1
  | KeyA = 2
  | KeyD = 3

type Reaction<'a> =
  | Create of (Unit -> 'a)
  | Modify of ('a -> 'a)

type Action =
  | MouseAction of MouseAction
  | KeyboardAction of Keys

//The physical body of the actor
type Body =
    {
        Position : Vector2<p>;
        Velocity : Vector2<p>;
        Orientation : float;
    }
    with
    static member Zero =
        {
            Position = Vector2.Zero;
            Orientation = 0.0;
            Velocity = Vector2.Zero
        }

//Player ships
type PlayerShip =
    {
        Body : Body;
        Health : int;
        Name : string; //Used to identify the player
        InputReactions : Map<Action,Reaction<PlayerShip>>
    }
    with
    static member Update player =
      //let movement = call the processinput function here with the inputreactions
      let velocity' = player.Body.Velocity + (List.reduce (fun acc elem -> acc + elem) (processInput player.InputReactions))
      {player with Body = {player.Body with Velocity = player.Body.Velocity}}
    static member Create () =
      //check conditions and based on that fill the list to return
      []
    static member Remove playership =
      false
    static member Zero =
        {
            Body = Body.Zero;
            Health = 1;
            Name = "player"
            InputReactions = Map.ofList[(*action*), (fun x -> x);//kijk hier naar de map of list shit
                                        ]
        }

type Ship =
    {
        Body : Body;
        Health : int;
        Name : string; //Used to identify the player
    }
    with
    static member Update (ship:Ship) =
      //let movement = call the processinput function here with the inputreactions
      { Body = {
                Position = ship.Body.Position + ship.Body.Velocity;
                Velocity = ship.Body.Velocity;
                Orientation = ship.Body.Orientation}
        Health = ship.Health
        Name = ship.Name
        }
    static member Create () =
      //check conditions and based on that fill the list to return
      []
    static member Remove ship =
      false
    static member Zero =
        {
            Body = Body.Zero;
            Health = 1;
            Name = "player"
        }


//Type for asteroids
type Asteroid =
    {
        Body : Body;
        Health : int;
    }
    with
    static member Update (asteroid:Asteroid) =
      //let movement = call the processinput function here with the inputreactions
      { Body = {
                Position = asteroid.Body.Position + asteroid.Body.Velocity;
                Velocity = asteroid.Body.Velocity;
                Orientation = asteroid.Body.Orientation};
        Health = asteroid.Health;
        }
    static member Create () =
      //check conditions and based on that fill the list to return
      []
    static member Remove ship =
      false
    static member Zero =
        {
            Body = Body.Zero;
            Health = 3;
        }
//Projectiles such as bullets
type Projectile =
    {
        Body : Body;
        Owner : string; //Corresponds to name of the firing player

    }
    with
    static member Update (projectile:Projectile) =
      //let movement = call the processinput function here with the inputreactions
      { Body = {
                Position = projectile.Body.Position + projectile.Body.Velocity;
                Velocity = projectile.Body.Velocity;
                Orientation = projectile.Body.Orientation};
        Owner = "player"
        }
    static member Create () =
      //check conditions and based on that fill the list to return
      []
    static member Remove asteroidlist =
      false
    static member Zero =
        {
            Body = Body.Zero;
            Owner = "player";
        }
//Generic actor label

type ActorWrapper<'a> =
  {
    Actors : List<'a>
    Update : 'a -> 'a
    Create : Unit -> List<'a>
    Remove : 'a -> bool
  }
  with
  static member MainUpdate (wr : ActorWrapper<'a>) =
    {wr with
      Actors = wr.Create() @
                [for (en:'a) in wr.Actors do
                  let en' = wr.Update en
                  if wr.Remove en' then yield en'
                ]
    }
  //this is the definition of the gamestate, the thing that gets passed around
type GameState = 
    {
        PlayerShip : ActorWrapper<PlayerShip>
        Ships : ActorWrapper<Ship>
        Asteroids : ActorWrapper<Asteroid>
        Projectiles : ActorWrapper<Projectile>
    }
    with
    static member UpdateAll (gs:GameState) =
      {
        PlayerShip = ActorWrapper.MainUpdate gs.PlayerShip
        Ships = ActorWrapper.MainUpdate gs.Ships
        Asteroids = ActorWrapper.MainUpdate gs.Asteroids
        Projectiles = ActorWrapper.MainUpdate gs.Projectiles
      }
    static member Zero =
      {
        PlayerShip = {
                      Actors = [PlayerShip.Zero]
                      Update = PlayerShip.Update
                      Create = PlayerShip.Create
                      Remove = PlayerShip.Remove
                      }
        Ships = {
                      Actors = [Ship.Zero]
                      Update = Ship.Update
                      Create = Ship.Create
                      Remove = Ship.Remove
                      }
        Asteroids = {
                      Actors = [Asteroid.Zero]
                      Update = Asteroid.Update
                      Create = Asteroid.Create
                      Remove = Asteroid.Remove
                      }
        Projectiles = {
                      Actors = [Projectile.Zero]
                      Update = Projectile.Update
                      Create = Projectile.Create
                      Remove = Projectile.Remove
                      }
      }

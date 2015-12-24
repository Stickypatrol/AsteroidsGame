(*
  MonoGame base game class overrides
*)

module Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Graphics
open Actors
open StateMonad
open GameState

//The base game class provided by MonoGame
type AsteroidsGame () as context =
  inherit Game ()

  //Required for graphics functionality, might remove mutable if added to gamestate
  let mutable graphics = new GraphicsDeviceManager (context)
  let mutable spriteBatch : SpriteBatch = null

  //The latest complete game state is passed to different MonoGame functions with this
  let mutable gameState = GameState.Zero

  override context.Initialize () =
    context.Content.RootDirectory <- "Content"

    gameState <- {
                    Players        = ActorWrapper<Player>.Zero
                    Asteroids      = ActorWrapper<Asteroid>.Zero
                    Projectiles    = ActorWrapper<Projectile>.Zero
                    GraphicsDevice = Some(new GraphicsDeviceManager (context))
                    SpriteBatch    = Some(new SpriteBatch (context.GraphicsDevice))
                    Textures       =
                      [
                        "Player",     loadTexture context.Content "Player"
                        "Ship",       loadTexture context.Content "Ship"
                        "Asteroid",   loadTexture context.Content "Asteroid"
                        "Projectile", loadTexture context.Content "Projectile"
                      ] |> Map.ofList
                  }

    gameState.GraphicsDevice.Value.GraphicsProfile <- GraphicsProfile.HiDef

    //initializing
    //let _, newState = initializeLogic gameState
    //gameState <- newState

    base.Initialize ()
    ()

  override context.LoadContent () =
    base.LoadContent ()
    ()

  override context.Update gameTime =
    //let _, newState = updateLogic gameState
    //gameState <- newState
    base.Update gameTime
    ()

  override context.Draw gameTime =
    context.GraphicsDevice.Clear Color.CornflowerBlue
    spriteBatch.Begin ()

    //draw all the actors
    //let players, ships, asteroids, projectiles = DeconstructState gameState

    //List.iter (fun x -> spriteBatch.Draw (shipTexture.Value, players.Actors.Head.Body.Position.ToXNA,

    //List.iter (fun x -> spriteBatch.Draw (shipTexture.Value, x.Body.Position.ToXNA, Color.White)) ships.Actors)
    //iterate over the asteroids and draw them
    //List.iter (fun (x : Asteroid) -> spriteBatch.Draw (asteroidTexture.Value, x.Body.Position.ToXNA, Color.White)) asteroids.Actors
    //iterate over the projectiles and draw them
    //List.iter (fun (x : Projectile) -> spriteBatch.Draw (projectileTexture.Value, x.Body.Position.ToXNA, Color.White)) projectiles.Actors

    spriteBatch.End ()
    base.Draw gameTime
    ()

module Game

//open shit
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Graphics

type AsteroidsGame () as context =
  inherit Game()

  let mutable graphics = new GraphicsDeviceManager (context)
  let mutable spritebatch : SpriteBatch = null

  let mutable gameState = GameState.Zero

  override context.Initialize() =
    context.Content.RootDirectory <- "Content"
    graphics.GraphicsProfile <- GraphicsProfile.HiDef
    spriteBatch <- new SpriteBatch (context.GraphicsDevice)

    //initializing
    let _, newState = initializeLogic gameState
    gameState <- newState

    base.Initialize ()
    ()

  override context.LoadContent() =
    base.LoadContent ()
    ()

  override context.Update gameTime =
    let _, newState = updateLogic gameState
    gameState <- newState
    base.Update gameTime
    ()

  override context.Draw gameTime =
    context.GraphicsDevice.Clear Color.CornflowerBlue
    spriteBatch.Begin()

    //draw all the actors
    let players, ships, asteroids, projectiles = DeconstructState gameState

    List.iter (fun x -> spriteBatch.Draw (shipTexture.Value, players.Actors.Head.Body.Position.ToXNA,

    List.iter (fun x -> spriteBatch.Draw (shipTexture.Value, x.Body.Position.ToXNA, Color.White)) ships.Actors)
    //iterate over the asteroids and draw them
    List.iter (fun (x : Asteroid) -> spriteBatch.Draw (asteroidTexture.Value, x.Body.Position.ToXNA, Color.White)) asteroids.Actors
    //iterate over the projectiles and draw them
    List.iter (fun (x : Projectile) -> spriteBatch.Draw (projectileTexture.Value, x.Body.Position.ToXNA, Color.White)) projectiles.Actors

    spriteBatch.End ()
    base.Draw gameTime
    ()
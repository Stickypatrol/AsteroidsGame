module Input

module Input

open System
open Microsoft.Xna.Framework.Input
open StateMonad

//Process any valid input

type InputState =
  | KeyboardState of KeyboardState
  | MouseState of MouseState

type MouseButtons =
  | LeftButton = 0
  | RightButton = 1
  | MiddleButton = 2

type ButtonStates =
  | MouseInput of MouseButtons
  | KeyboardInput of Keys

type InputBehavior<'a> = Map<ButtonStates, 'a -> 'a>

let compose (c:bool) ((map:Map<'k, 'a -> 'a>), k: 'k) (g: 'a -> 'a) : 'a -> 'a =
  if c then
    match map |> Map.tryFind k with
    | Some(f) -> fun x -> g (f (x))
    | None -> g
  else
    g

let (?) c (map, k) = fun g -> compose c (map, k) g

let ProcessInput actions obj =
  

//onaangepast
(*let processInput (i_b : InputBehavior<'a>) (elem : 'a) (i_s : InputState) : 'a =
  match i_s with
  | KeyboardState(k_s) ->
    k_s.IsKeyDown(Keys.W) ? (i_b, KeyboardInput(Keys.W))
      (k_s.IsKeyDown(Keys.S) ? (i_b, KeyboardInput(Keys.S))
      (k_s.IsKeyDown(Keys.A) ? (i_b, KeyboardInput(Keys.A))
      (k_s.IsKeyDown(Keys.D) ? (i_b, KeyboardInput(Keys.D))
      (fun x -> x))))
      elem
    //check for a mouseclick
  | MouseState(m_s) ->
    (m_s.LeftButton = ButtonState.Pressed) ? (i_b, MouseInput(MouseButtons.LeftButton))
      (fun x -> x)
      elem
    


let processInput s =
    let keysState = Keyboard.GetState ()
    let keysList = (keysState.GetPressedKeys () |> Array.toList)
    
    let processKey key state =
        match key with
        | Keys.Left -> {state with PlayerShip = s.PlayerShip}
        | Keys.Right -> {state with PlayerShip = s.PlayerShip}
        | _         -> state
    
    let folder = fun state key -> processKey key state

    (), List.fold folder s keysList
*)
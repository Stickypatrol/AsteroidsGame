module Types

open Math

type Body =
  {
    Position : Vector2<p>
    Velocity : Vector2<p/s>
    Size : Vector2<p>
    Orientation : float32
  }
  with
  static member Zero =
    {
      Position = Vector2<p>.Zero
      Velocity = Vector2<p/s>.Zero
      Size = Vector2<p>.Zero
      Orientation = 0.0f
    }

type Player =
  {
    Body : Body
    Health : int
    Name : string
  }
  with
  static member Zero =
    {
      Body = Body.Zero
      Health = 1
      Name = "Player"
    }
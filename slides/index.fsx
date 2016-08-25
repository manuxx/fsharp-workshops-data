(**
- title : Functional Data Structures
- description : Functional Data Structures
- author : Tomasz Heimowski
- theme : night
- transition : default

***

## F# CAMP

### Functional Data Structures

    [lang=bash]
    git clone https://github.com/theimowski/fsharp-workshops-data.git

or download ZIP from [here](https://github.com/theimowski/fsharp-workshops-data/archive/master.zip), then in **Command Prompt**:

    [lang=bash]
    cd fsharp-workshops-data
    .\build.cmd KeepRunning

slides are regenerated when the script (.\slides\index.fsx) is **saved**

==> **NOTE:** this is a different GIT repository, don't mix it with the one from previous workshops

---

### Agenda 

* Sum Types (Discriminated Unions)
* Product Types (Tuples, Records)
* Lists
* Homework

***

## Sum Types
### Discriminated Unions

---

### New Stuff 1.1
#### Discriminated Unions reminder *)
type Shape =
| Square of edge : float
// `*` in type declarations stands for tuples
| Rectangle of width : float * height : float
| Circle of radius : float

(**

---

#### Binary Tree as DU *)
type Tree =
| Empty
| Node of value: int * left: Tree * right: Tree

(**

---

#### Binary Tree as DU  *)

let tree =
    Node (8, 
     Node (3, 
      Node (1, Empty, Empty), 
      Node (6,
       Node (4, Empty, Empty), 
       Node (7, Empty, Empty))),
     Node (10, 
      Empty,
      Node (14, 
       Node (13, Empty, Empty), 
       Empty)))

(**
![bst](images/bst.png)

---

### printf and sprintf for debugging *)

printf "This is what a tree looks like: %A" tree

let sprintfResult = sprintf "%A" tree

(** #### Value of ``sprintfResult`` *)
(*** include-value: ``sprintfResult`` ***)





(**
---

### Example 1.1
Counting internal nodes (nodes that have at least one non-empty child) *)
let rec countInternal (tree: Tree) : int =
    match tree with
    | Empty -> 0
    | Node (_, Empty, Empty) -> 0
    | Node (_, left, right) -> 
        1 + countInternal left + countInternal right

let ``example 1.1`` = countInternal tree
(** #### Value of ``example 1.1`` *)
(*** include-value: ``example 1.1`` ***)
(**

*Sidenote: How to make `countInternal` tail-recursive?*

---

### Exercise 1.1

Sum values of all leaves in tree

#### --------------- Your code goes below --------------- *)
let rec sumLeaves (tree: Tree) : int = 
    0

let ``exercise 1.1`` = sumLeaves tree
(** #### Value of ``exercise 1.1`` *)
(*** include-value: ``exercise 1.1`` ***)
(**





---

### New Stuff 1.2
#### List concatenation operator *)
let firstList = [1;3;5]
let secondList = [2 .. 10]
let concatenatedList = firstList @ secondList
(** #### Value of ``concatenatedList`` *)
(*** include-value: ``concatenatedList`` ***)
(**

---

#### In-Order traversal

![inorder](images/inorder.png)

---

### Example 1.2
Collecting **leaf values** from tree into a list *)
let rec collectLeaves (tree : Tree) : list<int> =
    match tree with
    | Empty -> []
    | Node (v, Empty, Empty) -> [v]
    | Node (_, left, right) -> 
        collectLeaves left @ collectLeaves right

let ``example 1.2`` = collectLeaves tree
(** #### Value of ``example 1.2`` *)
(*** include-value: ``example 1.2`` ***)
(**

---

### Exercise 1.2
Collect **all values** from tree into a list in-order

#### --------------- Your code goes below --------------- *)
let rec collectInOrder (tree : Tree) : list<int> =
    []

let ``exercise 1.2`` = collectInOrder tree
(** #### Value of ``exercise 1.2`` *)
(*** include-value: ``exercise 1.2`` ***)
(**





---

### Exercise 1.3
Check if tree is sorted

#### --------------- Your code goes below --------------- *)
let isSorted (tree: Tree) : bool =
    false

let ``exercise 1.3`` = isSorted tree
(** #### Value of ``exercise 1.3`` *)
(*** include-value: ``exercise 1.3`` ***)
(**





---

### Example 1.4
Manipulating the tree (immutability) *)
let rec incrementValues (tree: Tree) : Tree =
    match tree with
    | Empty -> Empty
    | Node (v, left, right) ->
        Node (v + 1, incrementValues left, incrementValues right)

let ``example 1.4`` = incrementValues tree |> collectInOrder
(** #### Value of ``example 1.4`` *)
(*** include-value: ``example 1.4`` ***)
(**

---

### Exercise 1.4
Insert element into Binary Search Tree

#### --------------- Your code goes below --------------- *)
let rec insertBST (value: int) (tree: Tree) : Tree =
    Empty

let ``exercise 1.4`` = insertBST 5 tree |> collectInOrder
(** #### Value of ``exercise 1.4`` *)
(*** include-value: ``exercise 1.4`` ***)
(**

---

### Summary: Sum Types (Discriminated Unions)  

* DUs represent distinct cases that **sum up** to the represented Type 
* DUs types can be defined in recursive way (e.g. Node in Tree)
* DUs are immutable - can make a copy, but not mutate

---

### Links

* [Discriminated Unions - Adding types together](https://fsharpforfunandprofit.com/posts/discriminated-unions/) by Scott Wlaschin
* [Introduction to recursive types](https://fsharpforfunandprofit.com/posts/recursive-types-and-folds/) by Scott Wlaschin







***

## Product Types
### Tuples, Records




---

### New Stuff 2.1
#### Modelling cards *)
type Figure = 
    | Two  | Three | Four  | Five 
    | Six  | Seven | Eight | Nine 
    | Ten  | Jack  | Queen | King | Ace

type Suit = Diamonds | Spades | Hearts | Clubs

// Type alias for a tuple
type Card = Figure * Suit

// Type alias for a list
type Hand = list<Card>
(**

---

### Anonymous (lambda) functions  *)
let oddNumbers =
    [1 .. 10]
    |> List.filter (fun n -> n % 2 = 1)
(** #### Value of ``oddNumbers`` *)
(*** include-value: ``oddNumbers`` ***)
(**

---

### Pattern matching tuples *)
let kingSpades = King, Spades
let (figure, suit) = kingSpades
(** #### Value of ``figure`` *)
(*** include-value: ``figure`` ***)
(** #### Value of ``suit`` *)
(*** include-value: ``suit`` ***)
(**

---

### Tuple helper functions *)
let queenHearts = Queen, Hearts
let queen = fst queenHearts
let hearts = snd queenHearts
(** #### Value of ``queen`` *)
(*** include-value: ``queen`` ***)
(** #### Value of ``hearts`` *)
(*** include-value: ``hearts`` ***)
(**

---

### Example 2.1
Checking all cards *)
let threeKings = 
    [King, Clubs
     King, Diamonds
     King, Hearts]

let ``example 2.1`` = 
    threeKings
    |> List.forall (fun (figure,suit) -> figure = King)
(** #### Value of ``example 2.1`` *)
(*** include-value: ``example 2.1`` ***)
(**

---

### Poker hands

![handranks](images/handranks.jpg)

---

### Exercise 2.1
Check if hand is *Flush*

#### --------------- Your code goes below --------------- *)
let handFlush = [King,Clubs;Queen,Clubs;Nine,Clubs;Eight,Clubs;Five,Clubs]

let isFlush (hand: Hand) : bool =
    false

let ``exercise 2.1`` = isFlush handFlush
(** #### Value of ``exercise 2.1`` *)
(*** include-value: ``exercise 2.1`` ***)
(**






---

### New Stuff 2.2
#### List.Map *)
let mapModThree =
    [1 .. 10]
    |> List.map (fun n -> n % 3 = 0)
(** #### Value of ``mapModThree`` *)
(*** include-value: ``mapModThree`` ***)
(**

---

#### List.GroupBy *)
let groupModThree =
    [1 .. 10]
    |> List.groupBy (fun n -> n % 3)
(** #### Value of ``groupModThree`` *)
(*** include-value: ``groupModThree`` ***)
(**

---

### Example 2.2
Counting occurences  *)
let ``example 2.2`` = 
    ["Ananas";"Banan";"Agrest";"Cukinia";"Cebula";"Aronia"]
    |> List.groupBy (fun word -> word.ToCharArray().[0])
    |> List.map (fun (letter,words) -> (letter,words.Length))
(** #### Value of ``example 2.2`` *)
(*** include-value: ``example 2.2`` ***)
(**

---

### Exercise 2.2
Check if hand is *Full House*

#### --------------- Your code goes below --------------- *)
let handFullHouse = [King,Clubs;King,Spades;Nine,Clubs;Nine,Diamonds;Nine,Spades]

let isFullHouse (hand: Hand) : bool =
    false

let ``exercise 2.2`` = isFullHouse handFullHouse
(** #### Value of ``exercise 2.2`` *)
(*** include-value: ``exercise 2.2`` ***)
(**





---

### New Stuff 2.3
#### Records *)

type Point =
  { X : float 
    Y : float }

type PositionedShape =
  { Shape : Shape 
    Center : Point }

(**

---

#### Record fields (labeled) *)

let point = { X = 2.0; Y = 4.5 }
let shape = { Shape = Square 3.0; Center = point }

let pointX = point.X
let shapeField = shape.Shape
(** #### Value of ``pointX`` *)
(*** include-value: ``pointX`` ***)

(** #### Value of ``shapeField`` *)
(*** include-value: ``shapeField`` ***)


(**
---

#### Record structural equality *)

let shapesAreEqual = 
    shape = { Shape = Square 3.0; Center = point }

(** #### Value of ``shapesAreEqual`` *)
(*** include-value: ``shapesAreEqual`` ***)

(** 

---

#### Power and square root *)

let forthAndBack =
    [ 1.0 .. 10.0 ]
    |> List.map (fun x -> x ** 2.0)
    |> List.map (fun x -> sqrt x)

(** #### Value of ``forthAndBack`` *)
(*** include-value: ``forthAndBack`` ***)

(**

---

### Example 2.3
Finding shapes with center in specific point *)
let withCenterIn point shapes =
    shapes
    |> List.filter (fun shape -> shape.Center = point)

let ``example 2.3`` =
    [{ Shape = Circle (sqrt 2.0); Center = { X = 0.0; Y = 0.0 }}
     { Shape = Square 2.0;        Center = { X = 0.0; Y = 0.0 }}
     { Shape = Rectangle (3.,4.); Center = { X = 0.0; Y = 1.0 }}]
    |> withCenterIn { X = 0.0; Y = 0.0 }
(** #### Value of ``example 2.3`` *)
(*** include-value: ``example 2.3`` ***)
(**

---

### Exercise 2.3
Check if first shape is circumcircle of second shape.

First shape must be a circle, second a square or rectangle

#### --------------- Your code goes below --------------- *)
let isCircumcircle (circlePos: PositionedShape) (shapePos: PositionedShape) : bool = 
    false

(** --- *)

let ``exercise 2.3`` = 
    [({ Shape = Circle (sqrt 2.0); Center = { X = 0.0; Y = 0.0 }},
      { Shape = Square 2.0;        Center = { X = 0.0; Y = 0.0 }})

     ({ Shape = Circle (sqrt 2.0); Center = { X = 1.0; Y = 0.0 }},
      { Shape = Square 2.0;        Center = { X = 0.0; Y = 0.0 }})

     ({ Shape = Circle 2.5;        Center = { X = 0.0; Y = 0.0 }},
      { Shape = Rectangle (3.,4.); Center = { X = 0.0; Y = 0.0 }})

     ({ Shape = Circle 2.5;        Center = { X = 0.0; Y = 0.0 }},
      { Shape = Rectangle (3.,4.); Center = { X = 0.0; Y = 1.0 }})]
    |> List.map (fun (first,second) -> isCircumcircle first second)
(** #### Value of ``exercise 2.3`` *)
(*** include-value: ``exercise 2.3`` ***)
(**


---

### New Stuff 2.4
#### Record copy-and-update expression *)

let positionedShape = { Shape = Square 2.0; Center = { X = 0.0; Y = 0.0 } }

let squareMoved =
    { positionedShape with Center = { X = 2.0; Y = 1.0 } }

let circleWithSameCenter =
    { positionedShape with Shape = Circle 3.0 }

(** #### Value of ``squareMoved`` *)
(*** include-value: ``squareMoved`` ***)

(** #### Value of ``circleWithSameCenter`` *)
(*** include-value: ``circleWithSameCenter`` ***)


(**

---

### Example 2.4
Translate positioned shape
*)
let translate (vectorPoint: Point) (shape: PositionedShape) : PositionedShape =
    { shape with Center = 
                 { X = shape.Center.X + vectorPoint.X; 
                   Y = shape.Center.Y + vectorPoint.Y }}

let ``example 2.4`` = 
    [{ Shape = Circle (sqrt 2.0); Center = { X = 0.0; Y = 0.0 }}
     { Shape = Square 2.0;        Center = { X = 0.0; Y = 3.0 }}
     { Shape = Rectangle (3.,4.); Center = { X = 0.0; Y = 1.0 }}]
    |> List.map (translate { X = -2.0; Y = -3.0 })

(** #### Value of ``example 2.4`` *)
(*** include-value: ``example 2.4`` ***)
(**

---

### Exercise 2.4
Scale positioned shape

#### --------------- Your code goes below --------------- *)
let scale (magnitude: float) (shapePos: PositionedShape) : PositionedShape  = 
    shape

(** --- *)

let ``exercise 2.4`` = 
    [{ Shape = Circle (sqrt 2.0); Center = { X = 0.0; Y = 0.0 }}
     { Shape = Square 1.0;        Center = { X = 0.0; Y = 3.0 }}
     { Shape = Rectangle (3.,4.); Center = { X = 0.0; Y = 1.0 }}]
    |> List.map (scale 2.0)

(** #### Value of ``exercise 2.4`` *)
(*** include-value: ``exercise 2.4`` ***)
(**

---

### Summary: Product Types (Tuples, Records)  

* Type aliases are used for better understanding of code
* Tuples represent a **product** of two (or more) types
* Records also represent **product** of subtypes and provide additional functionality
* Tuples are fine to represent intermediate results, Records better for modelling

---

### Links

* [Tuples - Multiplying types together](https://fsharpforfunandprofit.com/posts/tuples/) by Scott Wlaschin
* [Records - Extending tuples with labels](https://fsharpforfunandprofit.com/posts/records/) by Scott Wlaschin


***

## Lists


---

### Bowling score kata ([details](http://codingdojo.org/cgi-bin/index.pl?KataBowling))

![bowling](images/bowling.jpg)

---

### Bowling scoring

![bowling_score](images/bowling_score.png)

    "XXXXXXXXXXXX" // 12 rolls: 12 strikes
    10+10+10 + 10+10+10 + 10+10+10 + 10+10+10 + 10+10+10 + ... = 300

    "9-9-9-9-9-9-9-9-9-9-" // 20 rolls: 10 pairs of 9 and miss
    9 + 9 + 9 + 9 + 9 + 9 + 9 + 9 + 9 + 9 = 90

    "5/5/5/5/5/5/5/5/5/5/5" // 21 rolls: 10 pairs of 5 and spare, with a final 5
    10+5 + 10+5 + 10+5 + 10+5 + 10+5 + ... = 150

    "X9/5/72XXX9-8/9/X"
    10+9+1  + 9+1+5  + 5+5+7 + 7+2   + 10+10+10 + 
    10+10+9 + 10+9+0 + 9+0   + 8+2+9 + 9+1+10   = 187

    "X4/2-" // What is the score?


---

### New Stuff 3.1
#### Active patterns *)
let (|Digit|_|) char =
    let zero = System.Convert.ToInt32 '0'
    if System.Char.IsDigit char then
        Some (System.Convert.ToInt32 char - zero)
    else
        None

let digit = 
    match '5' with
    | Digit x -> "a digit " + x.ToString()
    | _ -> "not a digit"
(** #### Value of ``digit`` *)
(*** include-value: ``digit`` ***)
(**

---

### Example 3.1
List pattern match on next value *)
let rec matchNext5 list =
    match list with
    | [] -> []
    | x :: 5 :: rest -> 0 :: 5 :: matchNext5 rest
    | x :: rest -> x :: matchNext5 rest

let ``example 3.1`` = 
    matchNext5 [1..10]
(** #### Value of ``example 3.1`` *)
(*** include-value: ``example 3.1`` ***)
(**


---

### Exercise 3.1
Implement `parseScore`.

#### --------------- Your code goes below --------------- *)
let rec parseScore (chars: list<char>) : list<Option<int>> =
    []

let ``exercise 3.1`` = parseScore ['X';'4';'/';'2';'-';'N']
(** #### Value of ``exercise 3.1`` *)
(*** include-value: ``exercise 3.1`` ***)
(**







---

### New Stuff 3.2
#### Pattern match guards (`when` keyword) *)
let onlyEvenNumber optNumber =
    match optNumber with
    | Some n when n % 2 = 0 -> "ok"
    | _ -> "wrong"
    
let onlyEvenNumbers =
    [Some 2; Some 3; Some 4; Some 5; None]
    |> List.map onlyEvenNumber
(** #### Value of ``onlyEvenNumbers`` *)
(*** include-value: ``onlyEvenNumbers`` ***)
(**

---

#### Symbol alias in pattern matching *)
let rec numTriangle numbers =
    match numbers with
    | first :: (second :: _ as rest) ->
        first + second :: numTriangle rest
    | _ ->
        []

let triangle = numTriangle [1 .. 5]
(** #### Value of ``triangle`` *)
(*** include-value: ``triangle`` ***)
(**

---

### Exercise 3.2
Implement `countScore`

#### --------------- Your code goes below --------------- *)
let rec countScore (scores: list<int>) : int =
    0

(** ---  *)
let ``exercise 3.2`` = 
    [[10;10;10;10;10;10;10;10;10;10;10;10]
     [9;0;9;0;9;0;9;0;9;0;9;0;9;0;9;0;9;0;9;0]
     [5;5;5;5;5;5;5;5;5;5;5;5;5;5;5;5;5;5;5;5;5]
     [10;9;1;5;5;7;2;10;10;10;9;0;8;2;9;1;10]]
    |> List.map countScore 
(** #### Value of ``exercise 3.2`` *)
(*** include-value: ``exercise 3.2`` ***)
(**


---

### Summary: Lists  

* List are idiomatic for F# 
* Pattern matching combined with recursion allow to represent complex list algorithms in elegant way

---

### Links

* [F# Lists](https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/lists-%5Bfsharp%5D) - MSDN
* [F# Lists](http://www.dotnetperls.com/list-fs) - DotNet Pearls


***

### Options to option *)
let optsToOpt opts  =
    let rec optsToOpt' acc opts =
        match acc, opts with
        | x, [] -> x |> Option.map List.rev
        | Some xs, Some x :: rest ->
            optsToOpt' (Some (x :: xs)) rest
        | _ -> None

    optsToOpt' (Some []) opts

let oneOption = optsToOpt [Some "abc"; Some "def"; Some "ghi"]
(** #### Value of ``oneOption`` *)
(*** include-value: ``oneOption`` ***)
(**

---

### Homework 1
Implement `bowlingScore`. 

Hint: Use `optsToOpt` to convert from list of options to option of list
*)
let bowlingScore (score: string) : Option<int> =
    Some 0

let ``homework 1`` = 
    ["XXXXXXXXXXXX" 
     "9-9-9-9-9-9-9-9-9-9-" 
     "5/5/5/5/5/5/5/5/5/5/5"
     "X9/5/72XXX9-8/9/X" ] |> List.map bowlingScore
(** #### Value of ``homework 1`` *)
(*** include-value: ``homework 1`` ***)
(**

---

---

### Homework 2
Write new, **tail-recursive** versions of `parseScore` and `countScore`.

Implement `bowlingScoreTail` to use those 2 new functions
*)
let rec parseScoreTail 
            (chars: list<char>) 
            (acc : list<Option<int>>) 
            : list<Option<int>> =
    []

(** --- *)
let rec countScoreTail (scores: list<int>) (acc : int) : int =
    0

(** --- *)
let bowlingScoreTail (score: string) : Option<int> =
    Some 0

let ``homework 2`` = bowlingScoreTail "XXXXXXXXXXXX"
(** #### Value of ``homework 2`` *)
(*** include-value: ``homework 2`` ***)
(**


***

## Summary

* Sum Types (Discriminated Unions)
* Product Types (Tuples, Records)
* Lists

*)

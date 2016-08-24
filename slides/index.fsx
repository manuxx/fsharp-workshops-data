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

// TODO: Different example

### New Stuff 2.3
#### Modelling Card game with Records *)
type CardRecord = 
  { Figure : Figure
    Suit : Suit }

type Player = 
  { Name : string
    Hand : list<CardRecord> }
(**

---

#### Players *)
let player1 = 
  { Name = "Player 1" 
    Hand = [ { Figure = Four; Suit = Spades }
             { Figure = Five; Suit = Diamonds }
             { Figure = Six; Suit = Hearts }
             { Figure = Seven; Suit = Spades }
             { Figure = Eight; Suit = Clubs } ] }

let player2 = 
  { Name = "Player 2"
    Hand = [ { Figure = Three; Suit = Spades }
             { Figure = Five; Suit = Hearts }
             { Figure = Six; Suit = Clubs }
             { Figure = Seven; Suit = Clubs }
             { Figure = Eight; Suit = Hearts } ] }
(**

---

#### Record fields - player name *)
let player1Name = player1.Name
(** #### Value of ``player1Name`` *)
(*** include-value: ``player1Name`` ***)
(** #### Record fields - player figures *)
let player1Figures = 
    player1.Hand
    |> List.map (fun c -> c.Figure) 
(** #### Value of ``player1Figures`` *)
(*** include-value: ``player1Figures`` ***)
(**

---

#### List.pairwise *)
let numberPairs = 
    [1..5]
    |> List.pairwise
(** #### Value of ``numberPairs`` *)
(*** include-value: ``numberPairs`` ***)
(**

---

#### Comparing DUs cases *)
let comparisons = 
    [Three, Four
     Six, Eight
     Two, King]
    |> List.map (fun (first, second) -> compare first second)
(** #### Value of ``comparisons`` *)
(*** include-value: ``comparisons`` ***)
(**

---

### Example 2.3
Pairwise list processing  *)
let ``example 2.3`` = 
    [1 .. 10]
    |> List.map (fun x -> x*x)
    |> List.pairwise
    |> List.map (fun (first,second) -> second - first)
(** #### Value of ``example 2.3`` *)
(*** include-value: ``example 2.3`` ***)
(**

---

### Exercise 2.3
Check if hand is *Straight* (no *baby straight* )

#### --------------- Your code goes below --------------- *)
let hasStraight (player: Player) : bool = 
    false

let ``exercise 2.3`` = hasStraight player1
(** #### Value of ``exercise 2.3`` *)
(*** include-value: ``exercise 2.3`` ***)
(**







---

### New Stuff 2.4
#### DU structural equality *)
let player1FirstCard = player1.Hand.[0]
let isFour = player1FirstCard.Figure = Four
(** #### Value of ``isFour`` *)
(*** include-value: ``isFour`` ***)
(** #### Record structural equality *)
let isFourSpades = player1FirstCard = { Figure = Four; Suit = Spades }
(** #### Value of ``isFourSpades`` *)
(*** include-value: ``isFourSpades`` ***)
(**

---

#### Record copy-and-update expression *)
let player1Clone = { player1 with Name = "Someone else" }
let player1CloneName = player1Clone.Name
(** #### Value of ``player1CloneName`` *)
(*** include-value: ``player1CloneName`` ***)
(** #### Unmodified fields remain the same *)
let sameHands = player1.Hand = player1Clone.Hand
(** #### Value of ``sameHands`` *)
(*** include-value: ``sameHands`` ***)
(**

---

### Exercise 2.4
Compare High Hands (highest card when there's no other rank)

#### --------------- Your code goes below --------------- *)
let rec compareHighHands (p1: Player) (p2: Player) : Option<Player> =
    None

let ``exercise 2.4`` = 
    compareHighHands player1 player2 
    |> Option.map (fun p -> p.Name)
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

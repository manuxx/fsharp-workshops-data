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

---

### Agenda 

* Sum Types (Discriminated Unions)
* Product Types (Tuples, Records)
* Lists
* Functions as first class? HOF?

***

## Sum Types
### Discriminated Unions



---

### New Stuff 1.1
#### Binary Tree *)
type Tree =
| Empty
| Node of value: int * left: Tree * right: Tree

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

---

#### Binary Tree 

![bst](images/bst.png)






---

### Example 1.1
#### Some example *)
let ``example 1.1`` = "example"
(** #### Value of ``example 1.1`` *)
(*** include-value: ``example 1.1`` ***)
(**

---

### Exercise 1.1
Count leaves of tree

#### --------------- Your code goes below --------------- *)
let rec countLeaves (tree: Tree) : int = 
    match tree with
    | Empty -> 0
    | Node (_, Empty, Empty) -> 1
    | Node (_, left, right) -> 
        countLeaves left +
        countLeaves right

let ``exercise 1.1`` = countLeaves tree
(** #### Value of ``exercise 1.1`` *)
(*** include-value: ``exercise 1.1`` ***)
(**





---

### New Stuff 1.2
#### Something new *)
// code
(**

---

### Example 1.2
#### Some example *)
let ``example 1.2`` = "example"
(** #### Value of ``example 1.2`` *)
(*** include-value: ``example 1.2`` ***)
(**

---

### Exercise 1.2
Collect all values from tree into a list in-order

#### --------------- Your code goes below --------------- *)
let rec collectInOrder (tree : Tree) : list<int> =
    match tree with
    | Empty -> []
    | Node (v, left, right) ->
        (collectInOrder left) @ [v] @ (collectInOrder right)

let ``exercise 1.2`` = collectInOrder tree
(** #### Value of ``exercise 1.2`` *)
(*** include-value: ``exercise 1.2`` ***)
(**





---

### New Stuff 1.3
#### Something new *)
// code
(**

---

### Example 1.3
#### Some example *)
let ``example 1.3`` = "example"
(** #### Value of ``example 1.3`` *)
(*** include-value: ``example 1.3`` ***)
(**

---

### Exercise 1.3
Check if tree is sorted

#### --------------- Your code goes below --------------- *)
let rec isSorted (tree: Tree) : bool =
    match tree with
    | Empty -> true
    | Node (v, left, right) ->
        let leftOk =  match left with
                      | Empty -> true
                      | Node (lv, _, _) -> lv < v
        let rightOk = match right with
                      | Empty -> true
                      | Node (rv, _, _) -> v < rv
        leftOk && rightOk && isSorted left && isSorted right

let ``exercise 1.3`` = isSorted tree
(** #### Value of ``exercise 1.3`` *)
(*** include-value: ``exercise 1.3`` ***)
(**





---

### New Stuff 1.4
#### Something new *)
// code
(**

---

### Example 1.4
#### Some example *)
let ``example 1.4`` = "example"
(** #### Value of ``example 1.4`` *)
(*** include-value: ``example 1.4`` ***)
(**

---

### Exercise 1.4
Insert element into Binary Search Tree

#### --------------- Your code goes below --------------- *)
let rec insertBST (value: int) (tree: Tree) : Tree =
    match tree with
    | Empty -> Node (value, Empty, Empty)
    | Node (v, left, right) ->
        if value < v then 
            Node (v, insertBST value left, right)
        else
            Node (v, left, insertBST value right)

let ``exercise 1.4`` = insertBST 5 tree |> collectInOrder
(** #### Value of ``exercise 1.4`` *)
(*** include-value: ``exercise 1.4`` ***)
(**






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

type Hand = list<Card>
(**

---

### Poker hands

![handranks](images/handranks.jpg)

---

### Example 2.1
#### Some example *)
let ``example 2.1`` = "example"
(** #### Value of ``example 2.1`` *)
(*** include-value: ``example 2.1`` ***)
(**

---

### Exercise 2.1
Check if hand is *Flush*

#### --------------- Your code goes below --------------- *)
let handFlush = [King,Clubs;Queen,Clubs;Nine,Clubs;Eight,Clubs;Five,Clubs]

let isFlush (hand: Hand) : bool =
    let firstSuit = snd hand.[0]
    hand
    |> List.forall (fun (figure,suit) -> suit = firstSuit)

let ``exercise 2.1`` = isFlush handFlush
(** #### Value of ``exercise 2.1`` *)
(*** include-value: ``exercise 2.1`` ***)
(**






---

### Exercise 2.2
Check if hand is *Full House*

#### --------------- Your code goes below --------------- *)
let handFullHouse = [King,Clubs;King,Spades;Nine,Clubs;Nine,Diamonds;Nine,Spades]

let isFullHouse (hand: Hand) : bool =
    let counts = 
        hand
        |> List.groupBy fst
        |> List.map (fun (figure, cards) -> cards.Length)
        |> List.sort
    counts = [2;3]

let ``exercise 2.2`` = isFullHouse handFullHouse
(** #### Value of ``exercise 2.2`` *)
(*** include-value: ``exercise 2.2`` ***)
(**





---

### New Stuff 2.3
#### Modelling Card with Record *)
type CardRecord = 
  { Figure : Figure
    Suit : Suit }

type Player = 
  { Name : string
    Hand : list<CardRecord> }
(**

---

#### Modelling Card with Record *)
let player1 = 
  { Name = "Player 1" 
    Hand = [ { Figure = Four; Suit = Spades }
             { Figure = Five; Suit = Diamonds }
             { Figure = Six; Suit = Hearts }
             { Figure = Seven; Suit = Spades }
             { Figure = Eight; Suit = Clubs } ] }

let player2 = 
  { Name = "Player 2"
    Hand = [ { Figure = Four; Suit = Spades }
             { Figure = Five; Suit = Spades }
             { Figure = Six; Suit = Spades }
             { Figure = Seven; Suit = Spades }
             { Figure = Eight; Suit = Spades } ] }
(**

---

### Exercise 2.3
Check if hand is *Straight* (no *baby straight* )

#### --------------- Your code goes below --------------- *)
let hasStraight (player: Player) : bool = 
    player.Hand
    |> List.map (fun card -> card.Figure)
    |> List.sort
    |> List.pairwise
    |> List.map (fun (first,second) -> compare first second)
    |> List.forall (fun diff -> diff = -1)

let ``exercise 2.3`` = hasStraight player1
(** #### Value of ``exercise 2.3`` *)
(*** include-value: ``exercise 2.3`` ***)
(**






---

### Exercise 2.4
Check if hand is *Straight Flush*

#### --------------- Your code goes below --------------- *)
let allStraightFlushes : list<list<CardRecord>> =
    [Two .. Ten]

    []

let ``exercise 2.4`` = allStraightFlushes |> List.contains player2.Hand
(** #### Value of ``exercise 2.4`` *)
(*** include-value: ``exercise 2.4`` ***)
(**





***

## Lists

***

## HOF?

*)

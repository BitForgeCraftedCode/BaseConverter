# BaseX ↔ BaseY Converter

A clean, human-readable implementation of a **generic base conversion algorithm**.

This class supports converting numbers between **any two numeral systems** using a customizable alphabet definition (e.g. binary, hex, base62, etc).

---

## Core Idea

All base conversions follow the same fundamental pattern:

```
baseX → base10 → baseY
```

This class models that directly:

* **Decode**: baseX → integer (base10)
* **Encode**: integer (base10) → baseY
* **Convert**: baseX → baseY (composition of Decode + Encode)

---

## Supported Bases

Any base is supported by providing a character alphabet string:

```csharp
string binary = "01";
string hex = "0123456789ABCDEF";
string base62 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
```

The base is inferred automatically from `alphabet.Length`.

---

## Methods

### 1) `Decode(string input, string alphabet)`

**Purpose:** Convert a number from baseX to base10

**Concept:** Positional notation evaluation

Mathematical model:

```
Σ (digit_value × base^position)
```

This mirrors handwritten conversion:

* Each digit has a place value
* Each place value is `base^position`
* Each digit contributes `digit × place value`

---

### 2) `Encode(int value, string alphabet)`

**Purpose:** Convert a base10 integer to baseY

**Concept:** Remainder/division method

Process:

* Repeatedly divide by base
* Collect remainders
* Map remainders to alphabet characters
* Build result in reverse order

This models the intuition:

> Each division step moves one power up in the target base

---

### 3) `Convert(string input, string sourceAlphabet, string targetAlphabet)`

**Purpose:** Convert directly from baseX to baseY

**Implementation:**

```csharp
return Encode(Decode(input, sourceAlphabet), targetAlphabet);
```

---

## Example Usage

```csharp
string base62 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
string binary = "01";

var num = Decode("1Z", base62);      // 123
var encoded = Encode(123, base62);    // "1Z"
var bin = Convert("1Z", base62, binary); // "1111011"
var num2 = Decode(bin, binary);       // 123
```

---

## Design Goals

* ✅ Human-readable logic
* ✅ Matches handwritten math
* ✅ Algorithm-first structure
* ✅ Base-agnostic
* ✅ Learning-oriented clarity
* ✅ DSA-friendly implementation

---

## Educational Value

This implementation is intentionally structured to:

* Mirror mathematical definitions
* Preserve conceptual clarity
* Separate concerns cleanly
* Make the algorithm understandable without theory background

It is designed for **learning**, not micro-optimization.

---

## Conceptual Model

```
Number systems = positional notation
Base conversion = representation change
Algorithm = evaluation + re-encoding
```

This class treats base conversion as a **representation problem**, not a string manipulation problem.

---

## Summary

This class implements a generalized number system converter using:

* Positional evaluation (Decode)
* Remainder method (Encode)
* Functional composition (Convert)

It supports arbitrary bases, clean abstraction, and mathematically correct conversion logic.

Perfect for:

* DSA learning
* CS fundamentals
* Algorithm practice
* Number system theory
* Interview prep
* Educational tooling

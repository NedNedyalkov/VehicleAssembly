# Tire Fitting Simulation & Vehicle Assembly

A multi-project .NET solution demonstrating safe object construction, type-safe factories, concurrency via producer–consumer simulation, and complete unit test coverage with `InternalsVisibleTo` enabled. The repository models a virtual tire fitting shop where vehicles arrive, mechanics process them concurrently, and the simulation can be cancelled interactively while running.


## 📦 Solution Structure

```

VehicleAssembly/          Core domain: vehicles, tires, manufacturers, factories
TireFittingShop/          Simulation engine & concurrency implementation
ConsoleApp/               CLI interface for running the simulation
Tests/
VehicleAssembly.Tests/  Unit tests for domain and factories
TireFittingShop.Tests/  Tests for simulation, cancellation, producers/consumers

````

---

## 🎯 Project Goals

- **Exception-free runtime behavior** in the core domain by using `TryCreate` factories
- **Strict encapsulation** to prevent invalid object construction or dependency misuse
- **Compile-time type safety** using separate `CarManufacturersEnum` and `MotorcycleManufacturersEnum`
- **Deterministic and repeatable simulation** using `IRandomProvider`, `IWorkSimulator`, and factory delegates
- **Producer–Consumer concurrency** modeling real-world workshop workflow
- **Full test coverage including internal members** via `InternalsVisibleTo`
- **User-interruptible execution** using `CancellationToken` and keypress detection

---

## 🏗 VehicleAssembly — Domain & Safe Object Construction

The domain assembly defines core entities:
- `Car`, `Motorcycle`, `Vehicle`
- `SummerTires`, `WinterTires`, `Tires`
- `Manufacturer` and concrete manufacturer implementations
- Separate enums: `CarManufacturersEnum` and `MotorcycleManufacturersEnum`

### Key Architectural Decisions

| Goal | Implementation |
|------|---------------|
| Prevent invalid domain states | All constructors are `internal` and validated |
| No runtime exceptions during creation | `TryCreateX(out T?)` factory pattern used across assembly |
| Compile-time type correctness | Two specific enums instead of polymorphic base types |
| Avoid dependency injection misuse | Factories and abstract classes are internal, not replaceable externally |

---

## 🧪 Factory Pattern Approach

Vehicle and tire creation uses controlled access:

```csharp
VehicleFactory.TryCreateCar(CarManufacturersEnum.Toyota, out var car);
TiresFactory.TryCreateWinterTires(2.2f, -10f, 3.0f, out var tire);
````

This ensures that:

* No exceptions are thrown on failure
* Null result reflects invalid input
* Domain validation remains centralized

---

## 🧵 TireFittingShop — Simulation Engine

Implements a real-time workshop simulation using:

* **Producer / Consumer Pattern** via `BlockingCollection<Customer>`
* **Concurrent mechanics** processing customers in parallel
* **Random arrival and work durations** (seeded for repeatability)
* **Service factory pattern** via `TireFittingShopConfiguration`
* **Cancellation support** using `CancellationToken`

### Key Components

| Component                                | Purpose                                        |
| ---------------------------------------- | ---------------------------------------------- |
| `CustomerProducer`                       | Generates customers asynchronously             |
| `Mechanic` (inherits `CustomerConsumer`) | Processes tire changes                         |
| `TaskDelayWorkSimulator`                 | Simulates timed work with cancellation support |
| `IRandomProvider`                        | Plug-replaceable deterministic randomness      |
| `ILogger` implementations                | Console, Debug, and in-memory logging          |

---

## 🔁 Simulation Flow

```
Start simulation
↓
Producer asynchronously creates customers at random intervals
↓
Customers enter BlockingCollection queue
↓
Multiple mechanics take from queue concurrently
↓
Each customer’s tires are changed (random duration)
↓
Simulation ends when all customers are served or cancelled
```

---

## ⌨ Console Application

The ConsoleApp project provides:

* Interactive parameter entry
* Loop to run multiple simulation rounds
* Real-time cancellation by pressing any key
* Friendly status messages and timing results

Example:

```
dotnet run --project ConsoleApp
Input Customers:
Input Mechanics:
Input Minimum Arrival Time:
Input Maximum Arrival Time:
Input Minimum Tire Change Time:
Input Maximum Tire Change Time:

Starting simulation
Press any key to cancel the simulation...

...

Simulation completed successfully for ... | Simulation cancelled after ...
```

---

## 🧪 Unit Testing

The solution contains extensive tests verifying both internals and public APIs:

| Category                         | Coverage                                  |
| -------------------------------- | ----------------------------------------- |
| Domain entity validation         | Tires, vehicles, manufacturers            |
| Factory correctness              | TryCreate success / failure paths         |
| Concurrency correctness          | Multiple mechanics processing             |
| Cancellation behavior            | Work simulator, producer & consumer loops |
| Simulation duration calculations | Expected runtime boundaries               |

Tests use `InternalsVisibleTo` intentionally to test domain behaviors that remain encapsulated in production.

---

## 🧠 Design Patterns in Use

| Pattern                     | Location                                          | Benefit                                                   |
| --------------------------- | ------------------------------------------------- | --------------------------------------------------------- |
| **Abstract Factory**        | VehicleFactory, TiresFactory, ManufacturerFactory | Safe object creation with no runtime exceptions           |
| **Factory Method**          | Customer generation                               | Decouples consumer workflow from creation logic           |
| **Producer / Consumer**     | TireFittingShop simulation                        | Concurrent customer processing                            |
| **Strategy**                | IRandomProvider, ILogger, IWorkSimulator          | Replaceable components for testing and performance tuning |
| **Service Factory Pattern** | TireFittingShopConfiguration                      | Dependency creation per run, not via static DI container  |
| **Singleton (Lazy<T>)**     | Manufacturer instances & default tires            | Guaranteed single instance configuration                  |
| **Fail-safe TryCreate**     | Factories returning `bool`                        | Predictable result handling                               |

---

## 🚀 Running the Simulation

### Requirements

* .NET 9.0 SDK or later

### Run instructions

```bash
dotnet build
dotnet test
dotnet run --project ConsoleApp
```

---

## 📍 Ideas for Improvements

* Use CodeGeneration to automate creation of Manufacturers.
* Deterministic Unit tests that don't require to run the whole duration.
* Generate Customers with different type of tires and maybe log the new tires they get.


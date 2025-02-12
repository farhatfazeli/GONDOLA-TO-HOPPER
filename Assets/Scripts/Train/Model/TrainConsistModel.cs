using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;

namespace Train.Model
{
    public class TrainConsistModel
    {
        public readonly int uuid;
        public readonly string name;
        private List<RollingStock> _rollingStock;

        private float _maxSpeed;
        private float _tractionCoefficient;
        private float _brakingCoefficient;
        private float _passengerMass;
        private float _freightMass;
        private bool _isDirty = true; // Flag to track changes

        public List<RollingStock> RollingStock
        {
            get => _rollingStock;
            set
            {
                _rollingStock = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true; // Mark for recalculation when rolling stock changes
            }
        }

        public float MaxSpeed
        {
            get
            {
                RecalculateIfDirty();
                return _maxSpeed;
            }
        }

        public float TractionCoefficient
        {
            get
            {
                RecalculateIfDirty();
                return _tractionCoefficient;
            }
        }

        public float BrakingCoefficient
        {
            get
            {
                RecalculateIfDirty();
                return _brakingCoefficient;
            }
        }

        public float PassengerMass
        {
            get
            {
                RecalculateIfDirty();
                return _passengerMass;
            }
        }

        public float FreightMass
        {
            get
            {
                RecalculateIfDirty();
                return _freightMass;
            }
        }

        public float TotalMass => PassengerMass + FreightMass; // Always up-to-date

        public TrainConsistModel(string name, List<RollingStock> rollingStock) : this(Guid.NewGuid().GetHashCode(),
            name, rollingStock)
        {
        }

        public TrainConsistModel(int uuid, string name, List<RollingStock> rollingStock)
        {
            this.uuid = uuid;
            this.name = name;
            RollingStock = rollingStock;
            Recalculate();
        }

        private void RecalculateIfDirty()
        {
            if (_isDirty) Recalculate();
        }

        private void Recalculate()
        {
            _maxSpeed = _rollingStock.OfType<Locomotive>().Any()
                ? _rollingStock.OfType<Locomotive>().Min(loco => loco.maxSpeed)
                : 0; // If no locomotives, speed is 0.

            _tractionCoefficient = _rollingStock.OfType<Locomotive>().Sum(loco => loco.tractionCoefficient);
            _brakingCoefficient = _rollingStock.OfType<Locomotive>().Sum(loco => loco.brakingCoefficient);
            _passengerMass = _rollingStock.OfType<Wagon>().Where(w => w.loadType == LoadType.Passengers)
                .Sum(w => w.mass);
            _freightMass = _rollingStock.OfType<Wagon>().Where(w => w.loadType == LoadType.Freight).Sum(w => w.mass);

            _isDirty = false; // Reset dirty flag
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
//
// ### **🚀 Integrating `TrainEngine` into `TrainConsistModel`**
// Now that we have a **realistic train physics system (`TrainEngine`)**, we should integrate it **without breaking the existing `TrainConsistModel` functionality**.
//
// ---
//
// ## **✅ Updated `TrainConsistModel` with `TrainEngine`**
// ```csharp
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using ScriptableObjects;
//
// namespace Train.Model
// {
//     public class TrainConsistModel
//     {
//         public readonly int uuid;
//         public readonly string name;
//         private List<RollingStock> _rollingStock;
//
//         private float _maxSpeed;
//         private float _tractionCoefficient;
//         private float _brakingCoefficient;
//         private float _passengerMass;
//         private float _freightMass;
//         private bool _isDirty = true; // Flag to track changes
//
//         public TrainStatus Status { get; private set; } = TrainStatus.InDepot;
//         public readonly TrainEngine Engine;
//
//         public List<RollingStock> RollingStock
//         {
//             get => _rollingStock;
//             set
//             {
//                 _rollingStock = value ?? throw new ArgumentNullException(nameof(value));
//                 _isDirty = true; // Mark for recalculation when rolling stock changes
//             }
//         }
//
//         public float MaxSpeed
//         {
//             get
//             {
//                 RecalculateIfDirty();
//                 return _maxSpeed;
//             }
//         }
//
//         public float TractionCoefficient
//         {
//             get
//             {
//                 RecalculateIfDirty();
//                 return _tractionCoefficient;
//             }
//         }
//
//         public float BrakingCoefficient
//         {
//             get
//             {
//                 RecalculateIfDirty();
//                 return _brakingCoefficient;
//             }
//         }
//
//         public float PassengerMass
//         {
//             get
//             {
//                 RecalculateIfDirty();
//                 return _passengerMass;
//             }
//         }
//
//         public float FreightMass
//         {
//             get
//             {
//                 RecalculateIfDirty();
//                 return _freightMass;
//             }
//         }
//
//         public float TotalMass => PassengerMass + FreightMass; // Always up-to-date
//
//         public TrainConsistModel(string name, List<RollingStock> rollingStock) : this(Guid.NewGuid().GetHashCode(),
//             name, rollingStock)
//         {
//         }
//
//         public TrainConsistModel(int uuid, string name, List<RollingStock> rollingStock)
//         {
//             this.uuid = uuid;
//             this.name = name;
//             RollingStock = rollingStock;
//             Recalculate();
//             
//             // ✅ Initialize TrainEngine with calculated values
//             Engine = new TrainEngine(_maxSpeed, TotalMass, _tractionCoefficient, _brakingCoefficient);
//         }
//
//         private void RecalculateIfDirty()
//         {
//             if (_isDirty) Recalculate();
//         }
//
//         private void Recalculate()
//         {
//             _maxSpeed = _rollingStock.OfType<Locomotive>().Any()
//                 ? _rollingStock.OfType<Locomotive>().Min(loco => loco.maxSpeed)
//                 : 0; // If no locomotives, speed is 0.
//
//             _tractionCoefficient = _rollingStock.OfType<Locomotive>().Sum(loco => loco.tractionCoefficient);
//             _brakingCoefficient = _rollingStock.OfType<Locomotive>().Sum(loco => loco.brakingCoefficient);
//             _passengerMass = _rollingStock.OfType<Wagon>().Where(w => w.loadType == LoadType.Passengers)
//                 .Sum(w => w.mass);
//             _freightMass = _rollingStock.OfType<Wagon>().Where(w => w.loadType == LoadType.Freight).Sum(w => w.mass);
//
//             _isDirty = false; // Reset dirty flag
//
//             // ✅ Update TrainEngine if mass or maxSpeed changed
//             Engine?.UpdateTrainProperties(_maxSpeed, TotalMass, _tractionCoefficient, _brakingCoefficient);
//         }
//
//         public void Dispatch()
//         {
//             if (Status == TrainStatus.Dispatched) return;
//
//             Status = TrainStatus.Dispatched;
//             Engine.Start();
//         }
//
//         public void Recall()
//         {
//             if (Status == TrainStatus.InDepot) return;
//
//             Status = TrainStatus.InDepot;
//             Engine.Stop();
//         }
//
//         public void Reset()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }
// ```
//
// ---
//
// ## **🔹 Key Changes**
// ### **1️⃣ Integrated `TrainEngine` for Movement & Physics**
// ```csharp
// public readonly TrainEngine Engine;
// ```
// ✅ **Now each `TrainConsistModel` has a physics engine** to handle speed, acceleration, and position.
//
// ---
//
// ### **2️⃣ `TrainEngine` is Initialized with Calculated Values**
// ```csharp
// Engine = new TrainEngine(_maxSpeed, TotalMass, _tractionCoefficient, _brakingCoefficient);
// ```
// ✅ **Automatically creates a `TrainEngine` instance when a train is created.**
//
// ---
//
// ### **3️⃣ Automatically Updates `TrainEngine` When Train Stats Change**
// ```csharp
// Engine?.UpdateTrainProperties(_maxSpeed, TotalMass, _tractionCoefficient, _brakingCoefficient);
// ```
// ✅ **When rolling stock changes, `TrainEngine` is updated automatically.**
//
// ---
//
// ### **4️⃣ Dispatching & Recalling Trains Starts/Stops the Engine**
// ```csharp
// public void Dispatch()
// {
//     if (Status == TrainStatus.Dispatched) return;
//
//     Status = TrainStatus.Dispatched;
//     Engine.Start();
// }
//
// public void Recall()
// {
//     if (Status == TrainStatus.InDepot) return;
//
//     Status = TrainStatus.InDepot;
//     Engine.Stop();
// }
// ```
// ✅ **When a train is dispatched, it starts moving.**  
// ✅ **When a train is recalled, it stops.**
//
// ---
//
// ## **✅ Updated `TrainEngine` to Support Dynamic Updates**
// Since train mass, traction, and braking **can change when rolling stock is modified**, `TrainEngine` should support **updating these values dynamically**.
//
// ### **🔹 Updated `TrainEngine`**
// ```csharp
// using UnityEngine;
//
// namespace Train.Model
// {
//     public class TrainEngine
//     {
//         private float _maxSpeed;
//         private float _mass;
//         private float _traction;
//         private float _braking;
//
//         public float Speed { get; private set; }
//         public float Acceleration { get; private set; }
//         public Vector3 Position { get; private set; }
//
//         private bool _isMoving;
//
//         public TrainEngine(float maxSpeed, float mass, float traction, float braking)
//         {
//             _maxSpeed = maxSpeed;
//             _mass = mass;
//             _traction = traction;
//             _braking = braking;
//             Speed = 0;
//             Acceleration = 0;
//             Position = Vector3.zero;
//         }
//
//         public void Start()
//         {
//             _isMoving = true;
//         }
//
//         public void Stop()
//         {
//             _isMoving = false;
//             Speed = 0;
//             Acceleration = 0;
//         }
//
//         public void Update(float deltaTime)
//         {
//             if (!_isMoving) return;
//
//             // 🔥 Basic train physics: F = ma
//             float force = _traction - (_braking * Speed / _maxSpeed);
//             Acceleration = force / _mass;
//
//             // 🔥 Update speed, ensuring it doesn't exceed max speed
//             Speed = Mathf.Clamp(Speed + Acceleration * deltaTime, 0, _maxSpeed);
//
//             // 🔥 Update position
//             Position += new Vector3(Speed * deltaTime, 0, 0);
//         }
//
//         public void UpdateTrainProperties(float maxSpeed, float mass, float traction, float braking)
//         {
//             _maxSpeed = maxSpeed;
//             _mass = mass;
//             _traction = traction;
//             _braking = braking;
//         }
//     }
// }
// ```
// ✅ **Now `TrainEngine` can dynamically update its physics properties when train weight or traction changes.**  
//
// ---
//
// ## **🚀 Final Architecture**
// | **Component** | **Responsibility** |
// |--------------|----------------|
// | **`TrainConsistModel`** | Stores train metadata (name, status, rolling stock) and integrates with `TrainEngine`. |
// | **`TrainEngine`** | Manages movement, speed, acceleration, and position calculations. |
// | **`TrainServiceManager`** | Handles dispatching and recalling trains. |
// | **`TrainController`** | Updates `TrainEngine` every frame for dispatched trains. |
//
// 🔥 **Now, your train simulation is fully integrated with realistic movement physics!** 🚆🔥  
// Would you like **additional mechanics like braking zones or multi-track movement?** 🚀
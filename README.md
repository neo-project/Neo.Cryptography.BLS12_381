# Neo.Cryptography.BLS12_381

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE.txt)
[![Build](https://img.shields.io/github/actions/workflow/status/neo-project/Neo.Cryptography.BLS12_381/main.yml?branch=master)](https://github.com/neo-project/Neo.Cryptography.BLS12_381/actions)

A high-performance **BLS12-381 cryptography library for .NET**, developed and maintained by the **Neo Project**.  
It provides fast and secure pairing-based cryptography for blockchain, distributed systems, and cryptographic research.

---

## 🔍 Overview

`Neo.Cryptography.BLS12_381` implements the **BLS12-381 elliptic curve** in pure C#.  
It offers full support for pairing operations, group arithmetic (G1/G2), and BLS signature schemes — enabling use cases like **aggregated signatures**, **threshold signatures**, and **multi-party consensus validation**.

This library powers **Neo’s blockchain cryptographic layer** and can also be used as a general-purpose .NET cryptography component.

---

## ✨ Features

* Full implementation of the **BLS12-381** curve over a prime field
* Fast and constant-time **pairing computation** (Miller loop + final exponentiation)
* Complete **G1 / G2** curve arithmetic with serialization and compression
* **BLS signature** generation, verification, and aggregation
* **Cross-platform** (.NET 6+, Windows / Linux / macOS)
* Comprehensive **unit tests** and performance benchmarks

---

## 🧩 Project Structure

```
src/
 └── Neo.Cryptography.BLS12_381/     # Core library implementation
tests/
 └── Neo.Cryptography.BLS12_381.Tests/ # Unit and integration tests
```

---

## 🛡️ Security Notes

* The library is designed with constant-time primitives where applicable.
* Always use cryptographic randomness (`RandomNumberGenerator.Create()`) for key generation.
* Review and audit before using in production-critical systems.

---

## 🤝 Contributing

Contributions are welcome!
Please open an issue or pull request following the [Neo contribution guidelines](https://github.com/neo-project/neo/blob/master/CONTRIBUTING.md).

---

## 📄 License

This project is released under the **MIT License**.
See [LICENSE.txt](LICENSE.txt) for details.

---

## 🌐 Links

* [Neo Project](https://github.com/neo-project)
* [Neo Main Repository](https://github.com/neo-project/neo)
* [BLS12-381 Specification (IETF draft)](https://datatracker.ietf.org/doc/draft-irtf-cfrg-bls-signature/)

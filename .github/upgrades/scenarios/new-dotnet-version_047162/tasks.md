# EFCoreSamples .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the EFCoreSamples solution upgrade from .NET 9.0 to .NET 10.0. All 13 projects will be updated simultaneously with framework targets, package versions, and breaking changes addressed atomically.

**Progress**: 1/3 tasks complete (33%) ![0%](https://progress-bar.xyz/33)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-03-07 14:58)*
**References**: Plan §Migration Strategy Phase 1

- [✓] (1) Verify .NET 10.0 SDK installed per Plan §Prerequisites
- [✓] (2) .NET 10.0 SDK available (**Verify**)

---

### [▶] TASK-002: Atomic framework and dependency upgrade with compilation fixes
**References**: Plan §Migration Strategy Phase 2-4, Plan §Project-by-Project Plans, Plan §Package Update Reference, Plan §Breaking Changes Catalog

- [✓] (1) Update TargetFramework from net9.0 to net10.0 in all project files per Plan §Project-by-Project Plans (focus: BooksLib first as foundation, then all console apps simultaneously)
- [✓] (2) All 13 project files updated to net10.0 (**Verify**)
- [✓] (3) Update all package references to version 10.0.3 per Plan §Package Update Reference (focus: EF Core packages, Microsoft.Extensions packages)
- [✓] (4) All package references updated to 10.0.3 (**Verify**)
- [✓] (5) Restore all dependencies
- [✓] (6) All dependencies restored successfully (**Verify**)
- [✓] (7) Fix configuration binding breaking change in Cosmos\Program.cs line 11 per Plan §Breaking Changes Catalog (update Configure<T> method to use Bind pattern)
- [✓] (8) Breaking change code fix applied (**Verify**)
- [✓] (9) Build solution and fix all compilation errors per Plan §Breaking Changes Catalog
- [✓] (10) Solution builds with 0 errors (**Verify**)
- [▶] (11) Commit changes with message: "TASK-002: Atomic upgrade to .NET 10.0 with EF Core 10.0.3"

---

### [ ] TASK-003: Run test projects and validate upgrade
**References**: Plan §Testing & Validation Strategy

- [ ] (1) Run spot-check validation per Plan §Post-Upgrade Validation (test projects: Intro, Cosmos, MigrationApp)
- [ ] (2) Fix any runtime failures (reference Plan §Breaking Changes for common issues)
- [ ] (3) Re-run tests after fixes
- [ ] (4) All tested projects execute without errors (**Verify**)
- [ ] (5) Commit test fixes with message: "TASK-003: Complete .NET 10.0 upgrade validation"

---








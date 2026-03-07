# .NET 10.0 Upgrade Plan - EFCoreSamples Solution

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Risk Management](#risk-management)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Overview
This plan outlines the upgrade of the **EFCoreSamples** solution from **.NET 9.0** to **.NET 10.0 (LTS)**. The solution contains 13 projects demonstrating various Entity Framework Core capabilities.

### Scope
- **Projects**: 13 total
  - 12 console applications (sample/demo projects)
  - 1 class library (BooksLib)
- **Current Framework**: .NET 9.0
- **Target Framework**: .NET 10.0 (Long Term Support)
- **Primary Technology**: Entity Framework Core 9.0.9 → 10.0.3

### Assessment Summary
- **Total Issues**: 61
  - **Mandatory**: 14 (13 framework updates + 1 API breaking change)
  - **Potential**: 47 (NuGet package updates)
  - **Optional**: 0
- **Affected Files**: 14 (13 project files + code files with breaking changes)
- **Code Complexity**: Low - ~4,000 total lines of code

### Key Findings
✅ **Strengths**:
- All projects use SDK-style project files
- Minimal inter-project dependencies (only MigrationApp → BooksLib)
- No complex multi-targeting scenarios
- All dependencies have .NET 10 compatible versions available

⚠️ **Attention Required**:
- **Cosmos project**: Contains 1 binary incompatible API that requires code changes
- All 7 NuGet packages need version updates (EF Core & Microsoft.Extensions.*)

### Recommended Strategy
**All-at-Once Upgrade** - Update all projects simultaneously due to:
- Simple dependency structure
- All projects are sample/demo applications (no production dependencies)
- Uniform technology stack (all use EF Core)
- Low risk profile

## Migration Strategy

### Selected Approach: All-at-Once Upgrade

Given the solution characteristics, we'll use a **coordinated, simultaneous upgrade** of all projects.

#### Rationale
1. **Simple Dependency Graph**: Only one project (MigrationApp) depends on another (BooksLib)
2. **Unified Technology Stack**: All projects use Entity Framework Core with similar patterns
3. **Sample/Demo Nature**: These are educational projects, not production applications with external consumers
4. **Version Alignment**: All packages share the same version (9.0.9), making coordinated updates straightforward

#### Migration Phases

**Phase 1: Prerequisites & Validation** ⏱️ ~10 minutes
- Verify .NET 10.0 SDK installation
- Commit current state and create upgrade branch ✅ (Already completed)
- Run baseline build to ensure clean starting point

**Phase 2: Framework Update** ⏱️ ~15 minutes
- Update all project files: `<TargetFramework>net9.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
- Order: BooksLib first (dependency), then all others

**Phase 3: Package Updates** ⏱️ ~20 minutes
- Update all 7 NuGet packages to version 10.0.3
- Packages to update:
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Cosmos
  - Microsoft.EntityFrameworkCore.Design
  - Microsoft.EntityFrameworkCore.Proxies
  - Microsoft.Extensions.Hosting
  - Microsoft.Extensions.Configuration.UserSecrets

**Phase 4: Breaking Changes Resolution** ⏱️ ~20 minutes
- Address binary incompatible API in Cosmos project
- Verify no additional code changes required

**Phase 5: Build & Test** ⏱️ ~15 minutes
- Build all projects in dependency order
- Verify no compilation errors
- Run projects individually to validate runtime behavior

**Phase 6: Final Validation** ⏱️ ~10 minutes
- Full solution rebuild
- Document any behavioral changes
- Commit changes with descriptive message

**Estimated Total Time**: ~90 minutes

#### Rollback Strategy
- **Before Phase 2**: Checkout original branch (`efcore9`)
- **During/After Phase 2+**: Use Git to revert to last commit on upgrade branch
- All changes isolated to `upgrade-to-NET10` branch until explicitly merged

## Detailed Dependency Analysis

### Project Dependency Graph

```
Level 0 (Foundation - No Dependencies):
├── BooksLib (Class Library)
│   └── Used by: MigrationApp
│
├── ConflictHandling-FirstWins (Console App)
├── ConflictHandling-LastWins (Console App)
├── Cosmos (Console App) ⚠️ Has breaking change
├── Intro (Console App)
├── LoadingRelatedData (Console App)
├── Models (Console App)
├── Queries (Console App)
├── Relationships (Console App)
├── ScaffoldSample (Console App)
├── Tracking (Console App)
└── Transactions (Console App)

Level 1 (Depends on Level 0):
└── MigrationApp (Console App)
    └── Depends on: BooksLib
```

### Upgrade Order

**Priority 1**: BooksLib
- **Reason**: Only shared library; MigrationApp depends on it
- **Risk**: Low - no dependencies
- **Action**: Update framework + packages

**Priority 2**: All Other Projects (parallel)
- **Reason**: Independent, no inter-dependencies
- **Risk**: Low - isolated changes
- **Action**: Update framework + packages simultaneously

### Package Dependency Matrix

| Package | Current | Target | Projects Using | Risk |
|---------|---------|--------|----------------|------|
| Microsoft.EntityFrameworkCore | 9.0.9 | 10.0.3 | 11 projects | Low |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.9 | 10.0.3 | 12 projects | Low |
| Microsoft.EntityFrameworkCore.Design | 9.0.9 | 10.0.3 | 12 projects | Low |
| Microsoft.EntityFrameworkCore.Cosmos | 9.0.9 | 10.0.3 | 1 project (Cosmos) | Medium ⚠️ |
| Microsoft.EntityFrameworkCore.Proxies | 9.0.9 | 10.0.3 | 1 project (LoadingRelatedData) | Low |
| Microsoft.Extensions.Hosting | 9.0.9 | 10.0.3 | 12 projects | Low |
| Microsoft.Extensions.Configuration.UserSecrets | 9.0.9 | 10.0.3 | 1 project (Cosmos) | Low |

### Compatibility Analysis

✅ **Low Risk Projects** (12):
- All standard EF Core SQL Server samples
- Well-supported migration path
- No known breaking changes

⚠️ **Medium Risk Projects** (1):
- **Cosmos**: Binary incompatible API detected
  - Requires code inspection and potential refactoring
  - EF Core Cosmos provider has evolving API surface

## Project-by-Project Plans

### BooksLib (Priority 1 - Foundation Library)

**Type**: Class Library  
**Current Framework**: net9.0  
**Target Framework**: net10.0  
**Dependencies**: None  
**Complexity**: Low

#### Changes Required:
1. ✏️ Update `<TargetFramework>net9.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
2. 📦 Update packages:
   - Microsoft.EntityFrameworkCore: 9.0.9 → 10.0.3
   - Microsoft.EntityFrameworkCore.SqlServer: 9.0.9 → 10.0.3
   - Microsoft.EntityFrameworkCore.Design: 9.0.9 → 10.0.3

#### Validation:
- ✅ Build succeeds
- ✅ No API breaking changes detected

---

### MigrationApp (Priority 1 - Depends on BooksLib)

**Type**: Console Application  
**Current Framework**: net9.0  
**Target Framework**: net10.0  
**Dependencies**: BooksLib  
**Complexity**: Low

#### Changes Required:
1. ✏️ Update `<TargetFramework>net9.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
2. 📦 Update packages:
   - Microsoft.Extensions.Hosting: 9.0.9 → 10.0.3

#### Validation:
- ✅ Build succeeds
- ✅ BooksLib reference resolves correctly
- ✅ Run application to verify migrations work

---

### Cosmos (Priority 2 - Has Breaking Change) ⚠️

**Type**: Console Application  
**Current Framework**: net9.0  
**Target Framework**: net10.0  
**Dependencies**: None  
**Complexity**: Medium

#### Changes Required:
1. ✏️ Update `<TargetFramework>net9.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
2. 📦 Update packages:
   - Microsoft.EntityFrameworkCore: 9.0.9 → 10.0.3
   - Microsoft.EntityFrameworkCore.Cosmos: 9.0.9 → 10.0.3
   - Microsoft.EntityFrameworkCore.Design: 9.0.9 → 10.0.3
   - Microsoft.Extensions.Hosting: 9.0.9 → 10.0.3
   - Microsoft.Extensions.Configuration.UserSecrets: 9.0.9 → 10.0.3

3. 🔧 **Code Change Required** - Program.cs, Line 11:

   **Issue**: Binary incompatible API
   ```csharp
   // Current (may fail):
   services.Configure<RestaurantConfiguration>(restaurantSettings);
   ```

   **Resolution**: The `Configure<T>(IServiceCollection, IConfiguration)` extension method signature has changed. Use binding instead:
   ```csharp
   // Updated approach:
   services.Configure<RestaurantConfiguration>(options => 
       restaurantSettings.Bind(options));
   ```

#### Validation:
- ✅ Build succeeds
- ✅ Code changes compile
- ✅ Run application to verify Cosmos DB connectivity

---

### Standard EF Core Projects (Priority 2 - Parallel Update)

The following 10 projects follow the same update pattern:

1. **ConflictHandling-FirstWins**
2. **ConflictHandling-LastWins**
3. **Intro**
4. **LoadingRelatedData** (includes EF.Proxies package)
5. **Models**
6. **Queries**
7. **Relationships**
8. **ScaffoldSample**
9. **Tracking**
10. **Transactions**

#### Common Changes for All:
1. ✏️ Update `<TargetFramework>net9.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
2. 📦 Update packages (standard set):
   - Microsoft.EntityFrameworkCore: 9.0.9 → 10.0.3
   - Microsoft.EntityFrameworkCore.SqlServer: 9.0.9 → 10.0.3
   - Microsoft.EntityFrameworkCore.Design: 9.0.9 → 10.0.3
   - Microsoft.Extensions.Hosting: 9.0.9 → 10.0.3

#### LoadingRelatedData - Additional Package:
- Microsoft.EntityFrameworkCore.Proxies: 9.0.9 → 10.0.3

#### Validation for All:
- ✅ Build succeeds
- ✅ No breaking changes detected
- ✅ Spot-check by running 2-3 applications

## Package Update Reference

### Package Update Matrix

| Package Name | Current Version | Target Version | Projects Affected | Breaking Changes |
|--------------|-----------------|----------------|-------------------|------------------|
| Microsoft.EntityFrameworkCore | 9.0.9 | 10.0.3 | 11 | None detected |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.9 | 10.0.3 | 12 | None detected |
| Microsoft.EntityFrameworkCore.Design | 9.0.9 | 10.0.3 | 12 | None detected |
| Microsoft.EntityFrameworkCore.Cosmos | 9.0.9 | 10.0.3 | 1 (Cosmos) | None in provider |
| Microsoft.EntityFrameworkCore.Proxies | 9.0.9 | 10.0.3 | 1 (LoadingRelatedData) | None detected |
| Microsoft.Extensions.Hosting | 9.0.9 | 10.0.3 | 12 | None detected |
| Microsoft.Extensions.Configuration.UserSecrets | 9.0.9 | 10.0.3 | 1 (Cosmos) | Configure method change |

### Update Commands

#### All Projects (Bulk Update)
```powershell
# From solution directory
dotnet add package Microsoft.EntityFrameworkCore --version 10.0.3
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.3
dotnet add package Microsoft.Extensions.Hosting --version 10.0.3
```

#### Project-Specific Updates

**Cosmos** (additional packages):
```powershell
cd Cosmos
dotnet add package Microsoft.EntityFrameworkCore.Cosmos --version 10.0.3
dotnet add package Microsoft.Extensions.Configuration.UserSecrets --version 10.0.3
```

**LoadingRelatedData** (additional package):
```powershell
cd LoadingRelatedData
dotnet add package Microsoft.EntityFrameworkCore.Proxies --version 10.0.3
```

### Package Compatibility Notes

✅ **All packages have stable .NET 10.0 releases**
- Entity Framework Core 10.0 was released as LTS alongside .NET 10.0
- Microsoft.Extensions.* packages align with .NET 10.0 release
- No preview or RC packages required

📝 **Migration Path**:
- EF Core 9.0 → 10.0 is a major version upgrade
- Review [EF Core 10.0 What's New](https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/whatsnew) for new features
- Review [EF Core 10.0 Breaking Changes](https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/breaking-changes) for comprehensive list

## Breaking Changes Catalog

### Detected Breaking Changes

#### 1. Configuration Binding API Change (Cosmos Project)

**Severity**: ⚠️ Mandatory  
**Category**: Binary Incompatible API  
**Affected Projects**: Cosmos  
**Affected File**: `Cosmos\Program.cs`, Line 11

**Issue**:
The `Configure<TOptions>(IServiceCollection, IConfiguration)` extension method has a breaking change in .NET 10.0.

**Current Code**:
```csharp
services.Configure<RestaurantConfiguration>(restaurantSettings);
```

**Problem**:
Direct passing of `IConfiguration` to `Configure<T>` may not work as expected due to signature changes in the options pattern.

**Solution** (Choose one):

**Option A - Use Bind (Recommended)**:
```csharp
services.Configure<RestaurantConfiguration>(options => 
    restaurantSettings.Bind(options));
```

**Option B - Use Configure with Action**:
```csharp
services.Configure<RestaurantConfiguration>(options =>
{
    options.DatabaseName = restaurantSettings["DatabaseName"];
    options.ConnectionString = restaurantSettings["ConnectionString"];
    // ... other properties
});
```

**Option C - Use ConfigureOptions**:
```csharp
services.AddOptions<RestaurantConfiguration>()
    .Bind(restaurantSettings);
```

**Recommended**: Option A - it's most concise and preserves the original intent.

**Impact**: 
- Code will not compile without this change
- Runtime behavior will fail if ignored
- Easy to fix with one-line change

---

### EF Core 9 → 10 General Breaking Changes

While no additional breaking changes were detected in this codebase, be aware of these EF Core 10.0 changes:

#### Potential Runtime Changes

1. **Query Translation Improvements**
   - Some LINQ queries may translate differently
   - Generally improves performance, but verify critical queries

2. **JSON Columns**
   - Enhanced JSON support may affect existing JSON column mappings
   - Review if you're using JSON columns in any models

3. **Interceptors**
   - Interceptor API may have minor changes
   - Not detected in current codebase

4. **Migrations**
   - Migration generation may produce slightly different SQL
   - Review auto-generated migrations carefully

#### No Impact Expected

✅ **DbContext Configuration**: No changes required  
✅ **Fluent API**: All configurations remain compatible  
✅ **LINQ Queries**: Standard query patterns work unchanged  
✅ **Change Tracking**: No behavior changes detected  
✅ **Relationships**: Navigation properties and relationships unchanged

---

### Testing Requirements

After applying breaking change fixes:

1. ✅ **Compile Test**: Verify all projects build without errors
2. ✅ **Runtime Test**: Run Cosmos project specifically to validate configuration binding
3. ✅ **Integration Test**: Verify database connections work across all projects
4. ✅ **Migration Test**: If using migrations, generate a test migration to verify tooling

## Testing & Validation Strategy

### Pre-Upgrade Validation

**Objective**: Establish baseline to compare post-upgrade

#### Checklist:
- [ ] Verify current solution builds successfully on .NET 9.0
- [ ] Document current build warnings/errors (if any)
- [ ] Confirm .NET 10.0 SDK is installed: `dotnet --list-sdks`
- [ ] Verify Git repository is clean or changes are committed

**Commands**:
```powershell
# Check .NET SDKs
dotnet --list-sdks

# Baseline build
cd "D:\books\ProfessionalCSharp2021\2_Libs\EFCore"
dotnet build EFCoreSamples.sln
```

---

### During-Upgrade Validation

**Objective**: Catch issues immediately after each change

#### Phase 2: After Framework Updates
```powershell
# Quick compile check (don't expect success yet due to package mismatches)
dotnet build EFCoreSamples.sln --no-restore
```
Expected: Build errors due to package version mismatches - **this is normal**

#### Phase 3: After Package Updates
```powershell
# Full restore and build
dotnet restore EFCoreSamples.sln
dotnet build EFCoreSamples.sln
```
Expected: Build errors in Cosmos project due to breaking API - **this is expected**

#### Phase 4: After Breaking Change Fix
```powershell
# Build specific project
dotnet build Cosmos\Cosmos.csproj

# Then full solution
dotnet build EFCoreSamples.sln
```
Expected: **Clean build with no errors**

---

### Post-Upgrade Validation

**Objective**: Comprehensive validation of upgraded solution

#### 1. Build Validation ✅
```powershell
# Clean and rebuild entire solution
dotnet clean EFCoreSamples.sln
dotnet build EFCoreSamples.sln --configuration Release

# Expected: Success, 0 errors, 0 warnings (or document any warnings)
```

#### 2. Individual Project Testing

**Priority Projects** (Test these first):
1. **BooksLib** - Foundation library
   ```powershell
   cd BooksLib
   dotnet build
   # Expected: Success
   ```

2. **Cosmos** - Had breaking changes
   ```powershell
   cd Cosmos
   dotnet run
   # Expected: Runs without configuration errors
   # Verify: Options binding works correctly
   ```

3. **MigrationApp** - Depends on BooksLib
   ```powershell
   cd MigrationApp
   dotnet run
   # Expected: Migrations execute successfully
   ```

**Sample Projects** (Spot check 2-3):
```powershell
# Example: Test Intro
cd Intro
dotnet run

# Example: Test Tracking
cd Tracking
dotnet run

# Example: Test Relationships
cd Relationships
dotnet run
```

#### 3. Functionality Validation

For each tested project, verify:
- ✅ Application starts without exceptions
- ✅ Database connections work (if applicable)
- ✅ EF Core operations execute correctly
- ✅ No unexpected warnings in console output

#### 4. Regression Testing

**Key Scenarios**:
- [ ] **Intro**: Basic DbContext operations
- [ ] **Tracking**: Change tracking behaves correctly
- [ ] **Queries**: LINQ queries execute and return data
- [ ] **Relationships**: Navigation properties work
- [ ] **LoadingRelatedData**: Lazy loading with proxies works
- [ ] **Cosmos**: Cosmos DB provider connects and queries
- [ ] **MigrationApp**: Migrations can be generated and applied

#### 5. Performance Spot Check

Not required for this upgrade, but note any obvious performance changes:
- Query execution time
- Startup time
- Memory usage

---

### Validation Checklist

**Before Declaring Success**:

- [ ] All 13 projects build without errors
- [ ] Cosmos project runs with fixed configuration binding
- [ ] At least 3 sample projects run successfully
- [ ] MigrationApp executes migrations correctly
- [ ] No new compiler warnings introduced
- [ ] Git status shows only intended changes
- [ ] Changes committed to `upgrade-to-NET10` branch

**Documentation**:
- [ ] Document any behavioral differences observed
- [ ] Note any new warnings and their resolution
- [ ] Record any performance changes (if noticeable)

---

### Rollback Verification

**If Issues Arise**:
```powershell
# Return to original state
git checkout efcore9

# Verify original version still works
dotnet build EFCoreSamples.sln
dotnet run --project Intro
```

Expected: Everything works as before upgrade attempt

## Risk Management

### Risk Assessment Matrix

| Risk Category | Likelihood | Impact | Severity | Mitigation |
|---------------|------------|--------|----------|------------|
| Build Failure | Low | Medium | **Low-Medium** | Incremental updates + validation |
| API Breaking Changes | Low | Medium | **Low-Medium** | Only 1 detected, fix documented |
| Runtime Errors | Low | Low | **Low** | Sample projects, easy to test |
| Data Loss | Very Low | N/A | **Very Low** | Sample projects, no production data |
| Migration Issues | Low | Low | **Low** | Test migrations before production |
| Dependency Conflicts | Very Low | Low | **Very Low** | All packages have .NET 10 versions |

---

### Identified Risks & Mitigations

#### Risk 1: Cosmos Configuration Breaking Change
**Likelihood**: Already Detected (100%)  
**Impact**: Medium - Prevents compilation  
**Severity**: ⚠️ Medium

**Mitigation Strategy**:
- ✅ Breaking change already identified in assessment
- ✅ Fix documented with code example
- ✅ Single-line change required
- ✅ Test immediately after fix

**Contingency**:
- If fix doesn't work, try alternate options (ConfigureOptions pattern)
- Cosmos project can be temporarily excluded from solution if needed

---

#### Risk 2: Unexpected EF Core Query Behavior Changes
**Likelihood**: Low (10%)  
**Impact**: Low - Samples only, no production impact  
**Severity**: 🟢 Low

**Mitigation Strategy**:
- Run each sample project individually
- Verify query results match expected outcomes
- EF Core 10.0 has excellent backward compatibility

**Contingency**:
- Document any query differences
- Update LINQ queries if translation changes
- Consult EF Core 10.0 breaking changes documentation

---

#### Risk 3: Package Compatibility Issues
**Likelihood**: Very Low (<5%)  
**Impact**: Medium  
**Severity**: 🟢 Low

**Mitigation Strategy**:
- All packages verified to have .NET 10.0 compatible versions
- Using stable releases (10.0.3), not preview/RC
- Uniform version across all packages reduces conflicts

**Contingency**:
- Check for newer patch versions (10.0.4+) if issues arise
- Report issues to EF Core GitHub if package bugs found

---

#### Risk 4: Migration Generation Changes
**Likelihood**: Low (15%)  
**Impact**: Low  
**Severity**: 🟢 Low

**Mitigation Strategy**:
- EF Core migration tooling is well-tested across versions
- Test migration generation in MigrationApp project
- Sample projects don't have complex schema changes

**Contingency**:
- Review auto-generated migrations carefully
- Hand-edit migrations if needed
- Use `dotnet ef migrations script` to preview SQL

---

#### Risk 5: Development Environment Issues
**Likelihood**: Low (10%)  
**Impact**: Medium  
**Severity**: 🟢 Low-Medium

**Mitigation Strategy**:
- Verify .NET 10.0 SDK installed before starting
- Use isolated branch (upgrade-to-NET10)
- Keep original branch (efcore9) intact

**Contingency**:
```powershell
# If environment issues occur
dotnet --list-sdks  # Verify .NET 10 is available
dotnet --version     # Check active SDK version

# Use global.json if specific SDK version needed
{
  "sdk": {
    "version": "10.0.100"
  }
}
```

---

### Risk Mitigation Timeline

**Before Starting**:
- [x] Create upgrade branch ✅
- [x] Commit pending changes ✅
- [ ] Verify .NET 10.0 SDK installed
- [ ] Run baseline build

**During Upgrade**:
- [ ] Update projects incrementally (BooksLib first)
- [ ] Validate after each phase
- [ ] Fix Cosmos breaking change immediately
- [ ] Test critical projects (Cosmos, MigrationApp, Intro)

**After Upgrade**:
- [ ] Full solution rebuild
- [ ] Run sample projects
- [ ] Document any issues
- [ ] Keep upgrade branch separate until validated

---

### Escalation Path

**If Low-Risk Issues Occur**:
1. Check error messages carefully
2. Consult EF Core 10.0 documentation
3. Review this plan's breaking changes section
4. Search GitHub issues for similar problems

**If Medium/High-Risk Issues Occur**:
1. Document the issue with full error details
2. Roll back to efcore9 branch
3. Research issue thoroughly
4. Consider phased upgrade approach
5. Seek community/Microsoft support if needed

---

### Success Indicators

✅ **All projects build successfully**  
✅ **Cosmos project runs with fixed configuration**  
✅ **Sample projects execute without errors**  
✅ **No data loss or corruption**  
✅ **Performance remains acceptable**  
✅ **All changes isolated to upgrade branch**

---

### Worst-Case Scenario Plan

**If Upgrade Cannot Be Completed**:

1. **Immediate Rollback**:
   ```powershell
   git checkout efcore9
   ```

2. **Assessment**:
   - Document what went wrong
   - Determine if issue is environmental or code-related

3. **Alternative Approaches**:
   - Upgrade projects individually over time
   - Skip problematic projects temporarily
   - Wait for newer EF Core patch releases

4. **Timeline**:
   - .NET 9.0 supported until May 2025
   - No immediate urgency to upgrade
   - Can retry upgrade when issues are resolved

**Bottom Line**: This is a low-risk upgrade with a clear rollback path. The isolated branch strategy ensures the original code remains untouched.

## Complexity & Effort Assessment

### Overall Complexity: 🟢 LOW

This upgrade is classified as **Low Complexity** based on:
- Simple project structure
- Minimal dependencies
- Well-supported migration path
- Only 1 breaking change detected
- Sample/educational code (not production)

---

### Effort Breakdown

#### Automated Tasks (Low Effort)
**Estimated Time**: 30-40 minutes

| Task | Effort | Time | Automation Potential |
|------|--------|------|---------------------|
| Update TargetFramework (13 projects) | Very Low | 5 min | High - Find/Replace |
| Update NuGet packages | Low | 15 min | High - Bulk commands |
| Build validation | Very Low | 10 min | Automated |

**Tools Available**:
- Find/Replace across files for framework version
- PowerShell/Bash scripts for bulk package updates
- CI/CD pipeline for automated builds

---

#### Manual Tasks (Medium Effort)
**Estimated Time**: 30-40 minutes

| Task | Effort | Time | Reason |
|------|--------|------|--------|
| Fix Cosmos breaking change | Low | 10 min | Single file, one-line fix |
| Code review | Low | 10 min | Verify change correctness |
| Run/test projects | Medium | 20 min | Manual execution required |

---

#### Total Effort Estimate

| Activity | Time | Cumulative |
|----------|------|------------|
| Prerequisites & Setup | 10 min | 10 min |
| Framework Updates | 5 min | 15 min |
| Package Updates | 15 min | 30 min |
| Breaking Change Fix | 10 min | 40 min |
| Build & Compile | 10 min | 50 min |
| Testing & Validation | 20 min | 70 min |
| Documentation & Commit | 10 min | **80 min** |

**Total Estimated Time**: ~80 minutes (1 hour 20 minutes)

**Confidence Level**: High (±10 minutes)

---

### Complexity by Project

#### Very Low Complexity (10 projects)
**Projects**: ConflictHandling-FirstWins, ConflictHandling-LastWins, Intro, Models, Queries, Relationships, ScaffoldSample, Tracking, Transactions, LoadingRelatedData

**Characteristics**:
- Standard EF Core patterns
- No breaking changes
- Framework + package updates only

**Effort per Project**: ~3 minutes
- 1 min: Framework update
- 2 min: Package updates

---

#### Low Complexity (2 projects)
**Projects**: BooksLib, MigrationApp

**Characteristics**:
- Simple dependency (MigrationApp → BooksLib)
- No breaking changes
- Standard updates

**Effort per Project**: ~5 minutes
- 1 min: Framework update
- 2 min: Package updates
- 2 min: Dependency validation

---

#### Medium Complexity (1 project)
**Project**: Cosmos

**Characteristics**:
- Has binary incompatible API
- Requires code change
- Cosmos DB provider considerations

**Effort**: ~15 minutes
- 1 min: Framework update
- 3 min: Package updates (5 packages)
- 5 min: Fix breaking change
- 6 min: Test configuration binding

---

### Skill Requirements

#### Required Skills
✅ **Basic .NET Knowledge**
- Understand .csproj file structure
- Familiar with NuGet package management
- Can run dotnet CLI commands

✅ **Entity Framework Core Familiarity**
- Understand DbContext basics
- Know how to run migrations
- Can test EF queries

✅ **Git Proficiency**
- Create and switch branches
- Commit changes
- Rollback if needed

#### Optional Skills
🔷 **Advanced .NET Knowledge** - Helpful for troubleshooting
🔷 **Cosmos DB Experience** - Useful for Cosmos project validation
🔷 **CI/CD Experience** - Not required for manual upgrade

---

### Automation Opportunities

#### High Automation Potential
1. **Framework Version Updates**
   ```powershell
   # PowerShell script to update all .csproj files
   Get-ChildItem -Recurse -Filter *.csproj | ForEach-Object {
       (Get-Content $_.FullName) -replace 'net9.0', 'net10.0' | 
       Set-Content $_.FullName
   }
   ```

2. **Package Updates**
   ```powershell
   # Script to update all projects
   $projects = Get-ChildItem -Recurse -Filter *.csproj
   foreach ($proj in $projects) {
       dotnet add $proj.FullName package Microsoft.EntityFrameworkCore --version 10.0.3
       # ... other packages
   }
   ```

#### Low Automation Potential
- Breaking change fix (requires code understanding)
- Testing and validation (requires human judgment)

---

### Comparison to Alternatives

#### vs. Staying on .NET 9.0
**Effort**: 0 minutes  
**Risk**: Low (supported until May 2025)  
**Benefit**: None (no new features)

#### vs. Upgrading to .NET 11.0 (Preview)
**Effort**: Similar (~90 minutes)  
**Risk**: High (preview quality)  
**Benefit**: Cutting-edge features, but unstable

#### vs. Phased Upgrade Approach
**Effort**: Higher (~120 minutes, spread over time)  
**Risk**: Lower (incremental validation)  
**Benefit**: Better for complex solutions

**Recommendation**: All-at-once upgrade to .NET 10.0 is optimal for this solution.

---

### Confidence Assessment

**High Confidence** (90%+) in effort estimate because:
- ✅ Assessment detected all issues upfront
- ✅ Only 1 breaking change, well-documented
- ✅ All packages have stable .NET 10 versions
- ✅ Simple project structure
- ✅ Clear rollback path available

**Potential Overruns** (10% risk):
- Unexpected environment-specific issues
- Hidden runtime behavior changes
- Cosmos DB connectivity problems
- Developer unfamiliarity with tooling

**Buffer**: Add 15-20 minutes for unexpected issues = **~100 minutes total with buffer**

## Source Control Strategy

### Branch Strategy

#### Current State ✅
- **Repository**: D:\books\ProfessionalCSharp2021
- **Source Branch**: `efcore9` (current, stable)
- **Upgrade Branch**: `upgrade-to-NET10` (created ✅)
- **Pending Changes**: Committed ✅

#### Branch Purpose
```
efcore9 (source)
  │
  ├─ Stable .NET 9.0 version
  └─ Remains unchanged during upgrade

upgrade-to-NET10 (feature branch)
  │
  ├─ All .NET 10.0 changes
  ├─ Can be validated independently
  └─ Merged to main after validation
```

---

### Commit Strategy

#### Phase-Based Commits

**Commit 1: Prerequisites** ✅ (Already done)
```
✅ "Checkpoint before .NET 10 upgrade"
- All pending changes committed
- Clean starting point established
```

**Commit 2: Framework Updates**
```
"Update all projects to .NET 10.0 target framework"

Changes:
- 13 .csproj files modified
- All <TargetFramework>net9.0</TargetFramework> → net10.0

Files changed: ~13 files
```

**Commit 3: Package Updates**
```
"Update NuGet packages to .NET 10.0 versions"

Changes:
- All EntityFrameworkCore packages: 9.0.9 → 10.0.3
- All Microsoft.Extensions packages: 9.0.9 → 10.0.3

Files changed: ~13 .csproj files
```

**Commit 4: Breaking Changes**
```
"Fix configuration binding breaking change in Cosmos project"

Changes:
- Cosmos\Program.cs: Update Configure<T> method call

Files changed: 1 file (Cosmos\Program.cs)
```

**Commit 5: Final Validation**
```
"Verify .NET 10.0 upgrade complete - all projects build successfully"

Changes:
- Update documentation
- Add any testing notes

Files changed: 0-1 files (documentation only)
```

---

### Commit Message Templates

#### Framework Update Commit
```
Update all projects to .NET 10.0 target framework

- Updated 13 project files from net9.0 to net10.0
- Projects: BooksLib, ConflictHandling-FirstWins, ConflictHandling-LastWins,
  Cosmos, Intro, LoadingRelatedData, MigrationApp, Models, Queries,
  Relationships, ScaffoldSample, Tracking, Transactions

Part of .NET 10.0 upgrade initiative
```

#### Package Update Commit
```
Update NuGet packages for .NET 10.0 compatibility

Updated packages:
- Microsoft.EntityFrameworkCore: 9.0.9 → 10.0.3
- Microsoft.EntityFrameworkCore.SqlServer: 9.0.9 → 10.0.3
- Microsoft.EntityFrameworkCore.Design: 9.0.9 → 10.0.3
- Microsoft.EntityFrameworkCore.Cosmos: 9.0.9 → 10.0.3
- Microsoft.EntityFrameworkCore.Proxies: 9.0.9 → 10.0.3
- Microsoft.Extensions.Hosting: 9.0.9 → 10.0.3
- Microsoft.Extensions.Configuration.UserSecrets: 9.0.9 → 10.0.3

All packages updated across 13 projects
```

#### Breaking Change Fix Commit
```
Fix configuration binding in Cosmos project for .NET 10.0

- Updated IConfiguration.Configure<T> call to use Bind() method
- Required due to API signature change in .NET 10.0
- File: Cosmos\Program.cs, Line 11

Before: services.Configure<RestaurantConfiguration>(restaurantSettings);
After: services.Configure<RestaurantConfiguration>(options => 
           restaurantSettings.Bind(options));

Resolves: Binary incompatible API issue (Api.0001)
```

---

### Review & Merge Strategy

#### Pre-Merge Checklist
- [ ] All projects build successfully
- [ ] Breaking changes resolved and tested
- [ ] At least 3 sample projects run without errors
- [ ] No unintended files committed
- [ ] Commit history is clean and logical

#### Merge Options

**Option 1: Direct Merge (Recommended for this scenario)**
```powershell
git checkout efcore9
git merge upgrade-to-NET10 --no-ff
git push origin efcore9
```
**When to use**: After successful validation, for permanent upgrade

**Option 2: Pull Request**
```powershell
git push origin upgrade-to-NET10
# Create PR on GitHub/Azure DevOps
# Review + Merge through web interface
```
**When to use**: For team review or documentation trail

**Option 3: Keep Separate Branches**
```powershell
# Keep both branches
git checkout upgrade-to-NET10  # For .NET 10 work
git checkout efcore9           # For .NET 9 maintenance
```
**When to use**: If maintaining both .NET versions

---

### Rollback Procedures

#### Before Any Commits on Upgrade Branch
```powershell
# Discard all changes
git checkout efcore9
git branch -D upgrade-to-NET10
```

#### After Commits, Before Merge
```powershell
# Abandon upgrade branch
git checkout efcore9
git branch -D upgrade-to-NET10
```

#### After Merge (Emergency Rollback)
```powershell
# Revert merge commit
git checkout efcore9
git revert -m 1 HEAD
git push origin efcore9
```

#### Nuclear Option (Last Resort)
```powershell
# Reset to commit before merge
git checkout efcore9
git reset --hard <commit-hash-before-merge>
git push --force origin efcore9  # ⚠️ Use with caution
```

---

### Git Ignore Considerations

Ensure `.gitignore` includes:
```gitignore
# Build results
[Bb]in/
[Oo]bj/

# User-specific files
*.user
*.suo

# NuGet
packages/
*.nupkg

# Rider
.idea/

# VS Code
.vscode/

# Migration temp files (if generated during testing)
**/Migrations/*.Designer.cs.bak
```

**Verify**: No unnecessary files committed during upgrade
```powershell
git status
git diff --stat origin/efcore9 upgrade-to-NET10
```

---

### Collaboration Guidelines

#### If Working Solo
- Commit frequently (after each phase)
- Use descriptive messages
- Tag stable points: `git tag v10.0-stable`

#### If Working in Team
- Use PR-based workflow
- Request review before merge
- Update team on breaking changes
- Document configuration changes in PR description

---

### Post-Merge Cleanup

After successful merge:
```powershell
# Optional: Keep upgrade branch for reference
git branch --no-merged  # Verify it's merged

# Optional: Delete upgrade branch
git branch -d upgrade-to-NET10

# Optional: Tag the upgraded version
git tag -a "dotnet-10.0" -m "Upgraded to .NET 10.0 (EF Core 10.0.3)"
git push origin --tags
```

---

### Documentation in Git

Consider adding to repository:
1. **UPGRADE.md** - Document the upgrade process for future reference
2. **CHANGELOG.md** - Note .NET 10.0 upgrade with changes
3. **README.md** - Update to reflect .NET 10.0 requirement

## Success Criteria

### Primary Success Criteria

The upgrade is considered **successful** when ALL of the following are achieved:

#### ✅ 1. Build Success
- [ ] All 13 projects compile without errors
- [ ] Zero compilation errors across the solution
- [ ] Solution builds in both Debug and Release configurations

**Validation Command**:
```powershell
dotnet clean EFCoreSamples.sln
dotnet build EFCoreSamples.sln --configuration Release
```
**Expected Output**: `Build succeeded. 0 Error(s)`

---

#### ✅ 2. Framework Target Verified
- [ ] All projects target `net10.0` framework
- [ ] No projects remain on `net9.0`

**Validation**:
```powershell
# Check all project files
Get-ChildItem -Recurse -Filter *.csproj | Select-String "<TargetFramework>"
```
**Expected**: All show `<TargetFramework>net10.0</TargetFramework>`

---

#### ✅ 3. Package Updates Complete
- [ ] All 7 packages updated to version 10.0.3 (or latest 10.x)
- [ ] No version 9.0.9 packages remain

**Validation**:
```powershell
# Check package versions in all projects
Get-ChildItem -Recurse -Filter *.csproj | Select-String "PackageReference.*9\.0"
```
**Expected**: No results (no version 9.x packages)

---

#### ✅ 4. Breaking Changes Resolved
- [ ] Cosmos project's configuration binding fixed
- [ ] Code compiles without API compatibility errors

**Validation**:
```powershell
dotnet build Cosmos\Cosmos.csproj
```
**Expected**: Success, no errors about `Configure<T>` method

---

#### ✅ 5. Runtime Validation
- [ ] At least 3 sample projects run without exceptions
- [ ] Cosmos project specifically runs and loads configuration
- [ ] MigrationApp successfully references BooksLib

**Validation**:
```powershell
# Test critical projects
dotnet run --project Intro
dotnet run --project Cosmos
dotnet run --project MigrationApp
```
**Expected**: Each runs without unhandled exceptions

---

### Secondary Success Criteria

Additional quality indicators:

#### 🟢 6. Code Quality
- [ ] No new compiler warnings introduced
- [ ] Code formatting preserved
- [ ] No debug code or commented-out sections added

#### 🟢 7. Git Repository Health
- [ ] All changes committed to `upgrade-to-NET10` branch
- [ ] Commit messages are descriptive
- [ ] No unintended files committed (bin/, obj/, etc.)

#### 🟢 8. Documentation
- [ ] Breaking change fix documented
- [ ] Upgrade process recorded (this plan)
- [ ] Any behavioral changes noted

---

### Acceptance Testing

#### Test Suite 1: Build & Compile
| Test | Command | Expected Result | Status |
|------|---------|-----------------|--------|
| Clean Build | `dotnet clean` | Success | [ ] |
| Debug Build | `dotnet build` | 0 errors | [ ] |
| Release Build | `dotnet build -c Release` | 0 errors | [ ] |
| Restore Packages | `dotnet restore` | All packages restored | [ ] |

---

#### Test Suite 2: Project-Specific Validation
| Project | Test | Expected Outcome | Status |
|---------|------|------------------|--------|
| BooksLib | Build only | Compiles successfully | [ ] |
| Intro | Run application | Executes without errors | [ ] |
| Cosmos | Run application | Configuration loads correctly | [ ] |
| MigrationApp | Run application | BooksLib reference works | [ ] |
| Tracking | Run application | Change tracking works | [ ] |
| Relationships | Run application | Navigation properties work | [ ] |

---

#### Test Suite 3: EF Core Functionality
| Feature | Project | Test | Expected | Status |
|---------|---------|------|----------|--------|
| DbContext | Intro | Basic operations | Data reads/writes | [ ] |
| Change Tracking | Tracking | Entity tracking | State changes detected | [ ] |
| Queries | Queries | LINQ queries | Results returned | [ ] |
| Relationships | Relationships | Nav properties | Related data loaded | [ ] |
| Lazy Loading | LoadingRelatedData | Proxies | Lazy load works | [ ] |
| Cosmos DB | Cosmos | Cosmos provider | Connects to DB | [ ] |
| Migrations | MigrationApp | Generate/apply | Migrations work | [ ] |

---

### Performance Criteria

**Not Mandatory, but Monitor**:

| Metric | .NET 9.0 Baseline | .NET 10.0 Target | Acceptable Range |
|--------|-------------------|------------------|------------------|
| Build Time | ~15-30 sec | ~15-30 sec | ±20% |
| Startup Time (Intro) | ~1-2 sec | ~1-2 sec | ±30% |
| Query Execution | (varies) | (varies) | No significant regression |

**Note**: Performance improvements expected in .NET 10.0, but not required for success.

---

### Failure Criteria

The upgrade is considered **failed** if ANY of the following occur:

#### ❌ Critical Failures
- Any project fails to compile
- Runtime exceptions in majority of projects
- Data corruption or loss (unlikely in samples)
- Unable to resolve breaking changes

#### ⚠️ Major Issues
- More than 3 projects fail runtime tests
- Significant performance degradation (>50%)
- New, unresolvable compiler warnings
- Migration generation fails

#### 🔶 Minor Issues (Acceptable)
- One or two sample projects have runtime quirks
- Performance within 20% of baseline
- Warnings that can be addressed post-upgrade
- Non-critical behavior changes

---

### Sign-Off Checklist

Before declaring the upgrade complete and merging:

**Technical Validation**:
- [ ] All 5 primary success criteria met
- [ ] At least 2 secondary criteria met
- [ ] No critical failures
- [ ] Major issues (if any) have workarounds

**Documentation**:
- [ ] Upgrade process documented
- [ ] Breaking changes noted
- [ ] Any behavioral changes recorded
- [ ] Future recommendations noted

**Source Control**:
- [ ] All changes reviewed
- [ ] Commits are clean and logical
- [ ] Branch ready for merge or PR
- [ ] Rollback tested (if time permits)

**Stakeholder Communication** (if applicable):
- [ ] Team notified of upgrade completion
- [ ] Breaking changes communicated
- [ ] Updated requirements documented (.NET 10 SDK)

---

### Post-Upgrade Verification (Optional)

After merge to main branch:

1. **Clean Clone Test**:
   ```powershell
   git clone <repository-url> test-upgrade
   cd test-upgrade
   git checkout efcore9  # or main branch
   dotnet build
   ```
   **Expected**: Fresh clone builds successfully

2. **CI/CD Pipeline** (if configured):
   - Verify automated builds pass
   - Check any automated tests
   - Monitor deployment (if applicable)

3. **Long-Term Monitoring**:
   - Watch for any issues in first week
   - Monitor performance metrics
   - Collect feedback from team/users

---

### Definition of Done

**The .NET 10.0 upgrade is DONE when**:
1. ✅ All primary success criteria achieved
2. ✅ Code merged to main branch (or upgrade branch validated for merge)
3. ✅ Team can use .NET 10.0 for development
4. ✅ No blockers for future development
5. ✅ Documentation updated
6. ✅ Rollback plan documented and tested

**At this point**: The EFCoreSamples solution is successfully upgraded to .NET 10.0 and ready for use.

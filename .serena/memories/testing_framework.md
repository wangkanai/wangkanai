# Testing Framework and Commands

## Testing Framework
- **Primary**: xUnit
- **Pattern**: AAA (Arrange, Act, Assert)
- **Mocks**: Custom mock implementations in `Mocks/` folders

## Test Commands
```bash
# Run tests for specific module (from module directory)
dotnet test .\tests\ -c Release

# Run tests with coverage (used in CI)
dotnet-coverage collect "dotnet test --no-build --verbosity normal" -f xml -o "coverage.xml"

# Run single test class
dotnet test .\tests\ -c Release --filter "ClassName=YourTestClass"

# Run tests with specific pattern
dotnet test .\tests\ -c Release --filter "TestCategory=Unit"

# Run benchmarks (from module/benchmark directory)
dotnet run -c Release
```
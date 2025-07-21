# CLAUDE.md - Wangkanai Tabler Development Guide

This file provides guidance to Claude Code when working on the Wangkanai Tabler Blazor component library.

## Project Overview

**Wangkanai Tabler** is a comprehensive Blazor component library that provides native Blazor implementations of
the [Tabler admin dashboard UI framework](https://tabler.io/). The mission is to create production-ready Blazor
components that maintain 100% visual fidelity with the original Tabler design system while replacing JavaScript
functionality with Blazor-native implementations.

### Key References

- **Original Tabler**: https://github.com/tabler/tabler
- **Tabler Preview**: https://tabler.io/admin-template/preview
- **Tabler Documentation**: https://docs.tabler.io/ui
- **Project PRD**: [PRD.md](./PRD.md)

## Development Commands

### Build and Restore

```bash
# Build entire Tabler module
./build.ps1

# Build specific projects
dotnet build src/Core/Wangkanai.Tabler.csproj -c Release
dotnet build src/Components/Wangkanai.Tabler.Components.csproj -c Release
dotnet build src/Web/Wangkanai.Tabler.Components.Web.csproj -c Release
```

### CSS/SCSS Development

```bash
# Navigate to Web project for CSS work
cd src/Web

# Install npm dependencies
npm install

# Build CSS from SCSS sources
npm run build

# Watch SCSS files for changes
npm run watch

# Clean and rebuild CSS
npm run clean && npm run build
```

### Testing

```bash
# Run all tests
dotnet test tests/Wangkanai.Tabler.Tests.csproj --configuration Release

# Run benchmarks
dotnet run --project benchmark/Wangkanai.Tabler.Benchmark.csproj -c Release
```

## Architecture Overview

### Project Structure

```
Tabler/
├── src/
│   ├── Core/                    # Wangkanai.Tabler.csproj
│   │   ├── Extensions/          # Service registration extensions
│   │   ├── Options/             # Configuration options
│   │   └── Services/            # Core services
│   ├── Components/              # Wangkanai.Tabler.Components.csproj
│   │   ├── Layout/              # Layout components (Page, Sidebar, Navbar)
│   │   ├── Navigation/          # Navigation components (Nav, Breadcrumb, Tabs)
│   │   ├── Forms/               # Form components (Input, Select, Checkbox)
│   │   ├── Data/                # Data components (Table, Card, List)
│   │   ├── Feedback/            # Feedback components (Alert, Modal, Toast)
│   │   ├── Base/                # Base components (Button, Badge, Icon)
│   │   └── Models/              # Shared models and enums
│   └── Web/                     # Wangkanai.Tabler.Components.Web.csproj
│       ├── wwwroot/
│       │   ├── dist/            # Compiled CSS output
│       │   └── scss/            # SCSS source files
│       └── package.json         # NPM build configuration
├── tests/                       # Unit and integration tests
├── benchmark/                   # Performance benchmarks
└── samples/                     # Demo applications
```

### Technology Stack

- **.NET 8.0**: Target framework
- **Blazor Server/WebAssembly**: Component runtime
- **Tabler CSS 1.4.0+**: Base stylesheet (CSS-only approach)
- **SCSS**: Custom styling and theming
- **xUnit**: Unit testing
- **BenchmarkDotNet**: Performance testing

## Design Principles

### 1. CSS-First Approach

- Leverage Tabler's existing CSS framework for all styling
- Only create custom SCSS for Blazor-specific adaptations
- Maintain visual parity with original Tabler components

### 2. Zero JavaScript Dependencies

- Replace all JavaScript interactions with Blazor functionality
- Use Blazor's event handling, state management, and component lifecycle
- Implement animations and transitions through CSS when possible

### 3. Strongly Typed APIs

- Use enums for predefined values (colors, sizes, variants)
- Provide IntelliSense-friendly component parameters
- Follow consistent naming conventions

### 4. Component Composition

- Design components for composition and reusability
- Support nested content through `RenderFragment`
- Enable extensibility via `AdditionalAttributes`

### 5. Performance First

- Minimize component render cycles
- Use efficient CSS class concatenation
- Implement lazy loading where appropriate

## Component Development Standards

### Naming Convention

- **All components**: Use `Tabler` prefix (e.g., `TablerButton`, `TablerCard`)
- **Files**: PascalCase matching component name (e.g., `TablerButton.razor`)
- **Parameters**: PascalCase with descriptive names
- **CSS Classes**: Follow Tabler's existing class naming

### Component Structure Template

```csharp
@namespace Wangkanai.Tabler.Components
@inherits ComponentBase

<div class="@CssClass" @attributes="AdditionalAttributes">
    @ChildContent
</div>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    // Component-specific parameters with sensible defaults
    [Parameter] public ComponentColor Color { get; set; } = ComponentColor.Primary;
    [Parameter] public ComponentSize Size { get; set; } = ComponentSize.Medium;

    private string CssClass => $"tabler-component {GetColorClass()} {GetSizeClass()}";

    private string GetColorClass() => Color switch
    {
        ComponentColor.Primary => "text-primary",
        ComponentColor.Secondary => "text-secondary",
        _ => string.Empty
    };

    private string GetSizeClass() => Size switch
    {
        ComponentSize.Small => "small",
        ComponentSize.Large => "large",
        _ => string.Empty
    };
}
```

### Parameter Patterns

```csharp
// Use enums for predefined options
[Parameter] public ButtonColor Color { get; set; } = ButtonColor.Primary;

// Boolean flags for states
[Parameter] public bool Disabled { get; set; }
[Parameter] public bool Loading { get; set; }

// Event callbacks for interactions
[Parameter] public EventCallback OnClick { get; set; }
[Parameter] public EventCallback<string> OnValueChanged { get; set; }

// Always support additional attributes
[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

// Content projection
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter] public RenderFragment? HeaderContent { get; set; }
```

## Implementation Phases

### Current Phase: Foundation (Phase 1)

**Focus Areas:**

1. Core service registration and configuration
2. Base component classes and utilities
3. CSS build pipeline integration
4. Essential components: `TablerButton`, `TablerIcon`, `TablerContainer`

**Priority Components to Implement First:**

- [ ] `TablerButton` - Base interactive component
- [ ] `TablerIcon` - Icon display component
- [ ] `TablerContainer` - Layout wrapper
- [ ] `TablerPage` - Main page layout

### Component Priority Matrix

#### High Priority (Phase 1-2)

- **Layout**: `TablerPage`, `TablerContainer`, `TablerNavbar`, `TablerSidebar`
- **Base**: `TablerButton`, `TablerIcon`, `TablerBadge`
- **Navigation**: `TablerNav`, `TablerNavItem`, `TablerTabs`

#### Medium Priority (Phase 3-4)

- **Forms**: `TablerForm`, `TablerInput`, `TablerSelect`, `TablerCheckbox`
- **Data**: `TablerTable`, `TablerCard`, `TablerPagination`
- **Feedback**: `TablerAlert`, `TablerModal`

#### Lower Priority (Phase 5-6)

- **Advanced**: `TablerToast`, `TablerTooltip`, `TablerDataGrid`
- **Specialized**: `TablerDatePicker`, `TablerFileUpload`

## Component Categories & Specifications

### Layout Components

**Purpose**: Page structure and responsive layout containers

| Component         | Description                    | Tabler CSS Classes               | Status   |
|-------------------|--------------------------------|----------------------------------|----------|
| `TablerPage`      | Main page wrapper              | `.page`, `.page-wrapper`         | Planning |
| `TablerSidebar`   | Collapsible navigation sidebar | `.navbar`, `.navbar-vertical`    | Planning |
| `TablerNavbar`    | Top navigation bar             | `.navbar`, `.navbar-expand-md`   | Planning |
| `TablerContainer` | Content container              | `.container`, `.container-fluid` | Planning |

### Base Components

**Purpose**: Fundamental UI building blocks

| Component      | Description         | Tabler CSS Classes     | Status   |
|----------------|---------------------|------------------------|----------|
| `TablerButton` | Interactive button  | `.btn`, `.btn-primary` | Planning |
| `TablerIcon`   | SVG icon display    | `.icon`, `.icon-*`     | Planning |
| `TablerBadge`  | Status indicators   | `.badge`, `.badge-*`   | Planning |
| `TablerAvatar` | User profile images | `.avatar`, `.avatar-*` | Planning |

### Form Components

**Purpose**: Data input and form controls

| Component        | Description           | Tabler CSS Classes  | Status   |
|------------------|-----------------------|---------------------|----------|
| `TablerInput`    | Text input field      | `.form-control`     | Planning |
| `TablerSelect`   | Dropdown selection    | `.form-select`      | Planning |
| `TablerCheckbox` | Checkbox input        | `.form-check-input` | Planning |
| `TablerTextarea` | Multi-line text input | `.form-control`     | Planning |

## CSS/SCSS Development

### Current CSS Structure

```scss
// src/Web/wwwroot/scss/tabler.scss
@import "variables";
// Custom variables
@import "mixins";
// Utility mixins
@import "body"; // Body-level styles
```

### CSS Build Process

1. **Source**: SCSS files in `src/Web/wwwroot/scss/`
2. **Processing**: Sass compilation with source maps
3. **Output**: Compiled CSS in `src/Web/wwwroot/dist/`
4. **Minification**: CleanCSS for production builds

### Tabler CSS Integration

- **Base CSS**: Imported from `@tabler/core` npm package
- **Custom Overrides**: Added through SCSS variables and mixins
- **Component Styles**: Minimal custom CSS, leverage existing Tabler classes

## Testing Strategy

### Unit Testing

- **Location**: `tests/` directory
- **Framework**: xUnit with FluentAssertions
- **Coverage Target**: 90%+ code coverage
- **Focus**: Component rendering, parameter handling, event callbacks

### Integration Testing

- **Blazor TestHost**: For component interaction testing
- **Accessibility**: ARIA compliance and keyboard navigation
- **Cross-browser**: Automated testing across browser engines

### Performance Testing

- **Benchmark Suite**: Component render performance
- **Memory Profiling**: Component lifecycle memory usage
- **Bundle Analysis**: CSS and JavaScript bundle sizes

## Code Quality Standards

### Static Analysis

- **Nullable Reference Types**: Enabled throughout project
- **Code Analysis**: Microsoft.CodeAnalysis.NetAnalyzers enabled
- **SonarCloud**: Continuous quality monitoring
- **EditorConfig**: Consistent code formatting

### Documentation Requirements

- **XML Documentation**: All public APIs documented
- **Component Examples**: Usage examples in XML docs
- **README Updates**: Keep component lists current

### Accessibility Requirements

- **WCAG 2.1 AA**: Minimum compliance standard
- **ARIA Labels**: Proper semantic markup
- **Keyboard Navigation**: Full keyboard support
- **Color Contrast**: 4.5:1 minimum ratio

## Available MCP Commands

### JetBrains IDE Integration

```bash
# Essential for Blazor component development
mcp__jetbrains__get_current_file_errors        # Real-time error checking
mcp__jetbrains__search_in_files_content        # Find component usage
mcp__jetbrains__find_files_by_name_substring   # Locate component files
mcp__jetbrains__replace_specific_text          # Bulk component updates
mcp__jetbrains__run_configuration              # Test execution
```

### File Operations

```bash
# Project structure management
mcp__jetbrains__list_directory_tree_in_folder  # Project navigation
mcp__jetbrains__get_file_text_by_path          # Component examination
mcp__jetbrains__create_new_file_with_text      # New component creation
```

## Development Workflow

### Creating New Components

1. **Plan Component API**
	- Review Tabler documentation for target component
	- Define component parameters and events
	- Identify required CSS classes

2. **Create Component Files**
   ```bash
   # Component implementation
   src/Components/{Category}/Tabler{ComponentName}.razor

   # Component code-behind (if needed)
   src/Components/{Category}/Tabler{ComponentName}.razor.cs
   ```

3. **Implement Component**
	- Follow component structure template
	- Use appropriate Tabler CSS classes
	- Support `AdditionalAttributes` for extensibility

4. **Add Tests**
   ```bash
   tests/Components/{Category}/Tabler{ComponentName}Tests.cs
   ```

5. **Update Documentation**
	- Add component to README.md
	- Include usage examples
	- Update component status in PRD.md

### CSS/SCSS Changes

1. **Modify SCSS Sources**
   ```bash
   # Edit source files
   src/Web/wwwroot/scss/_variables.scss
   src/Web/wwwroot/scss/_mixins.scss
   ```

2. **Build and Test**
   ```bash
   cd src/Web
   npm run build
   npm run watch  # For development
   ```

3. **Verify Output**
	- Check compiled CSS in `wwwroot/dist/`
	- Test in sample applications
	- Validate responsive behavior

### Component Testing Approach

1. **Unit Tests**: Component logic and parameter handling
2. **Render Tests**: HTML output verification
3. **Integration Tests**: Component composition scenarios
4. **Visual Tests**: Screenshot-based regression testing

## Common Patterns

### CSS Class Management

```csharp
// Efficient class concatenation
private string CssClass => string.Join(" ", GetCssClasses()).Trim();

private IEnumerable<string> GetCssClasses()
{
    yield return "btn";  // Base Tabler class

    if (Color != ButtonColor.None)
        yield return $"btn-{Color.ToString().ToLowerInvariant()}";

    if (Size != ButtonSize.Medium)
        yield return $"btn-{Size.ToString().ToLowerInvariant()}";

    if (Disabled)
        yield return "disabled";
}
```

### Event Handling

```csharp
// Async event callback pattern
private async Task HandleClick()
{
    if (Disabled) return;

    await OnClick.InvokeAsync();
}
```

### Conditional Rendering

```razor
@if (Loading)
{
    <span class="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>
}

@if (ChildContent != null)
{
    @ChildContent
}
else if (!string.IsNullOrEmpty(Text))
{
    @Text
}
```

## Troubleshooting Guide

### Common Issues

**CSS Not Loading**

- Verify npm build process completed successfully
- Check `wwwroot/dist/` contains compiled CSS
- Ensure CSS referenced correctly in host application

**Component Not Rendering**

- Check component namespace and assembly references
- Verify all required parameters provided
- Use browser dev tools to inspect generated HTML

**Performance Issues**

- Profile component render cycles
- Check for unnecessary re-renders
- Optimize CSS class concatenation

### Debug Commands

```bash
# Check project references
dotnet list package --include-transitive

# Verify build output
ls -la src/Web/wwwroot/dist/

# Run specific test
dotnet test --filter "TestMethod=TablerButtonRenderTest"

# Performance profiling
dotnet run --project benchmark/ -c Release --filter "*Button*"
```

## Integration with Wangkanai Ecosystem

### Dependencies

- **Wangkanai.System**: Base utilities and extensions
- **Wangkanai.Detection**: Device/browser detection (planned)
- **Wangkanai.Validation**: Form validation enhancements (planned)

### Shared Resources

- **Color Enums**: Extend existing `BgColor`, `TextColor`
- **Size Enums**: Create consistent sizing system
- **CSS Variables**: Leverage shared design tokens

## Resources & References

### Tabler Resources

- **Main Site**: https://tabler.io/
- **GitHub**: https://github.com/tabler/tabler
- **Icons**: https://tabler.io/icons
- **Documentation**: https://docs.tabler.io/

### Development Resources

- **Blazor Components**: https://docs.microsoft.com/en-us/aspnet/core/blazor/components/
- **CSS Grid/Flexbox**: https://css-tricks.com/
- **Accessibility**: https://www.w3.org/WAI/WCAG21/quickref/

### Project Resources

- **PRD**: [PRD.md](./PRD.md) - Complete project requirements
- **README**: [README.md](./README.md) - Project overview
- **Issues**: GitHub Issues for bug tracking and features

---

**Document Version**: 1.0
**Last Updated**: 2025-01-21
**Target Framework**: .NET 8.0
**Tabler Version**: 1.4.0+

## Quick Start for Claude Code Sessions

When starting a new session:

1. **Review Current Phase**: Check PRD.md for current implementation phase
2. **Examine Existing Code**: Use JetBrains MCP commands to explore current structure
3. **Check Build Status**: Run `./build.ps1` to ensure clean build
4. **Review Tests**: Check existing test coverage and patterns
5. **Plan Component Work**: Select next component from priority matrix
6. **Follow Standards**: Use component templates and naming conventions
7. **Test Early**: Create tests alongside component implementation
8. **Document Progress**: Update PRD.md component status tables

Always prioritize visual fidelity with original Tabler components while ensuring native Blazor functionality and
performance.

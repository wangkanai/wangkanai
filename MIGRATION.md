# Module Migration History

## Domain, Audit, and EntityFramework Modules - Migrated to Dedicated Repository

**Date:** 2025-08-31  
**New Location:** https://github.com/wangkanai/domain

### What was migrated

The following modules have been moved from this monorepo to their own dedicated repository:

- **Wangkanai.Domain** - Core domain-driven design patterns and building blocks
- **Wangkanai.Audit** - Comprehensive auditing capabilities for change tracking  
- **Wangkanai.EntityFramework** - Entity Framework Core utilities and extensions

### Why the migration

These modules form a cohesive domain-driven design ecosystem that benefits from:
- Independent versioning and release cycles
- Focused development and maintenance
- Cleaner dependency management
- Dedicated CI/CD pipeline optimized for their specific needs

### Migration details

- **Structure:** Flattened organization (src/, tests/, benchmark/) instead of module-based
- **Solution:** Uses new .slnx format (Domain.slnx)
- **Packages:** Three independent NuGet packages maintained separately
- **Quality:** SonarQube Cloud integration (project key: wangkanai_domain)
- **CI/CD:** Comprehensive GitHub Actions workflow with caching and parallel testing

### For users

If you're using these packages:
- **No breaking changes** - same package names and APIs
- **New repository** for issues, contributions, and documentation: https://github.com/wangkanai/domain
- **Continued support** and active maintenance in the new location

### For contributors  

To contribute to these modules:
1. Visit the new repository: https://github.com/wangkanai/domain
2. Follow the contribution guidelines in that repository
3. All future development happens in the new location

---

This migration ensures these modules receive dedicated attention while keeping this main repository focused on its core concerns.
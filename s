[33mcommit 849eed1dbec1d28be1daecb9bc0782281327955f[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mmain[m[33m)[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Tue Jun 9 13:59:10 2026 +0300

    feat: add vector embedding support using Groq API
    
    Added float[] property to Product entity.
    
    Configured EF Core value converter for vector mapping in Infrastructure.
    
    Implemented EmbeddingService integrated with Groq.

[33mcommit 4860e43da8166a42810750419fdb65802340ebe3[m[33m ([m[1;31morigin/main[m[33m)[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Tue Jun 2 12:08:00 2026 +0300

    The getcategories endpoint has been added to the CategoriesController.

[33mcommit ce0feb6a68a273912197e432b6ad2958e2efb102[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Wed May 6 17:56:31 2026 +0300

     feat: setup category/product creation handlers, controllers and apply migrations

[33mcommit a803730840c54125338dd5aba2b432f67118d709[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Wed May 6 17:33:10 2026 +0300

    feat: implement CreateCategory feature and encapsulate Product-Category relationship.

[33mcommit b1cdeb5fed5a6fa5bf43f0861213129728ef04d2[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Tue May 5 20:56:36 2026 +0300

    refactor(domain): refactor comment policy logic and init product features
    
    - Enforced domain purity in Product entity by removing async tasks and external service dependencies.
    
    - Moved comment policy business rule (HasPurchased check) to CommentPolicyService via AddCommentToProductAsync.
    
    - Initialized Product Features in the Application layer and added ProductListDto.
    
    - Added ProductsController to the API.

[33mcommit 2cb8aa03d3161418b6f64d85aaa3756b89c8b9db[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Tue May 5 18:37:41 2026 +0300

    refactor: add cancellation token support to repositories and unit of work.

[33mcommit 239b39f07b9c3282591c1d5fa047666b7047b0c3[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Mon May 4 19:27:28 2026 +0300

    chore: setup MediatR and establish CQRS feature structure

[33mcommit 65aaf37417d7d770723d61587c015d7ec177e4bf[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Mon May 4 18:44:59 2026 +0300

    feat(persistence): reset migrations and initialize database with Guid IDs
    
    - Deleted legacy integer-based migration files.
    
    - Created ''InitialCreate_Guid'' migration to reflect the new domain model.
    
    - Updated the database schema to use uuid types for all primary and foreign keys.

[33mcommit 85c15e4e8904782ac3ec79fd5136f89cfc17a7eb[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Mon May 4 18:28:40 2026 +0300

    chore: register infrastructure and mock auth services in Program.cs

[33mcommit 4cc9bd88354e5430c6386414d959598f9f261ee2[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Mon May 4 18:01:23 2026 +0300

    refactor: migrate IDs to Guid and add FakeCurrentUserService

[33mcommit 59619602ba7826e8a98e6d1600c7b230e36ade99[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Mon May 4 15:25:58 2026 +0300

    feat(infrastructure): implement Unit of Work for centralized repository management
    
    - Integrated repository properties into UnitOfWork for unified access.
    - Implemented SaveChangesAsync to enable atomic database updates in a single transaction.
    - Centralized AppDbContext management within the Unit of Work to ensure data integrity across the AdvancedSearch project.

[33mcommit 686ef00fe9e9ba2df49f7293ff9bb59f229a8b7f[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sun May 3 23:03:22 2026 +0300

    feat(infrastructure): add boilerplate for specific repositories
    
    Implemented interfaces and base classes for all domain entities to support future custom data access logic.

[33mcommit 830122817c9560daf244a8bab7742ec51740236c[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sun May 3 22:03:03 2026 +0300

    feat(infrastructure): define Generic Repository and Unit of Work signatures
    
    - Added IGenericRepository and IUnitOfWork interfaces in the Domain layer.
    - Created empty GenericRepository and UnitOfWork implementations in Infrastructure.
    - Established the base structure for the data access layer.

[33mcommit 786fd898b2c1da5e919808a3f57ad6e59fe9be41[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sun May 3 15:05:34 2026 +0300

    feat(persistence): setup DbContext and apply initial migration
    
    - Added DbSets for all domain entities.
    - Configured Address Value Object as an owned type using OwnsOne.
    - Generated and applied the initial migration to the PostgreSQL database.

[33mcommit a44204d2aa30b56012388b4c58e003a7bbabebff[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sun May 3 12:17:26 2026 +0300

    refactor(order): prevent external OrderItem manipulation in AddOrderItem
    
    Refactored AddOrderItem to accept parameters and create the entity internally to ensure better encapsulation and data integrity.

[33mcommit 61510474b082d5eb097b2ab242980a36dc2e7e56[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sat May 2 23:23:45 2026 +0300

    feat(domain): implement comment policy and purchase validation
    
    - Added ICommentPolicyService and IOrderRepository interfaces.
    
    - Implemented AddComment in Product entity with purchase verification.
    
    - Enforced ''purchase before comment'' business rule via CommentPolicyService.

[33mcommit bea9164d26183004eadb4d82bf09fa8114d92a0f[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sat May 2 20:23:40 2026 +0300

    refactor(domain): encapsulate Product and Comment entities

[33mcommit b09527e2a33592edea1bbda30ba3ffa858dee55c[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sat May 2 19:04:45 2026 +0300

    feat(domain): introduce Address value object and link to Order and Customer
    
    - Created Address as an immutable value object.
    
    - Replaced individual address fields in Order and Customer with Address VO.

[33mcommit d91a7cfc66fadeed1d887315669da1d0cd71deda[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sat May 2 18:23:05 2026 +0300

    refactor(order): implement Order as Aggregate Root and encapsulate OrderItems
    
    - Defined Order as the Aggregate Root.
    - Set OrderItem as a child entity.
    - Ensured encapsulation by restricting direct access to the items collection.
    - Added AddOrderItem method to manage item addition logic.

[33mcommit 3224cb6b7c367ccab0d8102f2d76ea2ee9d2f5d2[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sat May 2 14:18:35 2026 +0300

    refactor(domain): improved child entity visibility via aggregate root markers

[33mcommit 709c6e14f0971a2213263f84075d374c80658e4f[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Sat May 2 14:05:05 2026 +0300

    feat: implement Customer and Order domain entities

[33mcommit 58a8ee8e4cd6c0ba9e3d842937248f5967445de5[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Thu Apr 30 13:27:57 2026 +0300

    Rename project from ShopSage to AdvancedSearch

[33mcommit a33d70f215aca76583c705333241deecb0cdbc86[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Thu Apr 30 12:00:35 2026 +0300

    New project name and Domain start

[33mcommit 08671c4fd78524b54919f52c86232277324d5157[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Wed Apr 29 23:34:10 2026 +0300

    feat(domain): create initial empty entities

[33mcommit 410aba51fdd8c0d6df4ce5198d318a9a0a7d76e3[m
Author: Bahadır Sabancı <88037188+baho-s@users.noreply.github.com>
Date:   Wed Apr 29 23:27:42 2026 +0300

    feat: setup initial architecture with Domain, Application, Infrastructure and WebAPI

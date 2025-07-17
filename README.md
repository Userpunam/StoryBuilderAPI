Story Builder API

A .NET Core Web API that allows users to create, retrieve, and manage stories based on input words.
The API supports PostgreSQL for data storage, Serilog for structured logging, and Swagger for API documentation.

Features

- ✅ Create story using a single word (max 10 characters)
- ✅ Prevent duplicate stories
- ✅ Get paginated list of stories
- ✅ Count how many times a word appears in a story (case-insensitive)
- ✅ Upload image for a specific story and store its metadata

Tech Stack

| Technology        | Purpose                              |
|--------------------|---------------------------------------|
| .NET Core 7 / 8     | Web API framework                     |
| Entity Framework Core | ORM for PostgreSQL integration       |
| PostgreSQL         | Relational database                   |
| Serilog            | Logging requests, responses, errors   |
| Swagger (Swashbuckle) | API documentation UI                |
| C#                 | Language used                         |


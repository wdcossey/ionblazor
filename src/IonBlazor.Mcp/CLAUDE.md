# IonBlazor MCP Server

## Project Identity

This is a **Model Context Protocol (MCP) server** written in C# / .NET, built to expose the
**IonBlazor** Blazor/Razor component library as a set of AI-callable tools. It runs as a
`stdio`-transport console application and is consumed by Claude Code, Claude Desktop, VS Code
Copilot (Agent mode), or any other MCP-compatible client.

**Key packages:**
- `ModelContextProtocol` v1.3+ — official C# MCP SDK (co-maintained by Anthropic & Microsoft)
- `Microsoft.Extensions.Hosting` — generic host / DI

---

## Stack & Runtime

| Concern        | Choice                                                                |
|----------------|-----------------------------------------------------------------------|
| Language       | C# 13 / .NET 9+                                                       |
| Transport      | stdio (default); HTTP via `ModelContextProtocol.AspNetCore` if needed |
| MCP SDK        | `ModelContextProtocol` (NuGet)                                        |
| Project type   | Console application                                                   |
| Target library | IonBlazor (local project reference or NuGet)                          |

---

## Project Structure

```
IonBlazor.Mcp/
├── CLAUDE.md                  ← you are here
├── .claude/
│   ├── settings.json          ← allowed CLI commands, denied paths
│   └── settings.local.json    ← machine-specific env vars (not committed)
├── .mcp.json                  ← project-scoped MCP server registration
├── IonBlazor.Mcp.csproj
├── Program.cs                 ← host setup, tool registration
├── Tools/
│   ├── ComponentTools.cs      ← tools for querying/generating components
│   ├── DocTools.cs            ← tools for component docs / prop tables
│   └── ScaffoldTools.cs       ← tools for scaffolding usage examples
└── Resources/
    └── ComponentRegistry.cs   ← component metadata / introspection helpers
```

---

## Build & Run

```bash
# Restore and build
dotnet restore
dotnet build

# Run the server directly (for manual testing)
dotnet run

# Run without rebuilding (used by MCP clients)
dotnet run --no-build

# Publish self-contained executable
dotnet publish -c Release -r win-x64 --self-contained
```

---

## Critical MCP Rules

> These are non-negotiable — violating them silently breaks the server.

1. **Never write to `stdout`** in tool methods or anywhere in the call path.
   `Console.WriteLine` / `Console.Write` / `Console.Out` corrupt the JSON-RPC stream.

2. **All logging goes to `stderr`** via `ILogger` with `LogToStandardErrorThreshold`
   configured in `Program.cs`. The host sets this up automatically when you call
   `AddStdioServerTransport()`.

3. **`[Description]` attributes are mandatory** on every tool and every parameter.
   The LLM reads these to decide whether to invoke the tool. Vague descriptions = unused tools.

4. **Return strings or structured types**, not `void`. Tools that return nothing give the
   model nothing to work with.

5. **Pin the SDK version** in the `.csproj`. The SDK was in preview for a long time; uncontrolled
   updates can break the API surface. Use a `<PackageVersion>` property or explicit version tag.

---

## Program.cs Pattern

```csharp
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();   // discovers all [McpServerToolType] classes

await builder.Build().RunAsync();
```

---

## Tool Authoring Pattern

```csharp
using System.ComponentModel;
using ModelContextProtocol.Server;

[McpServerToolType]
public static class ComponentTools
{
    [McpServerTool, Description(
        "Returns the full list of available IonBlazor components with their categories.")]
    public static string ListComponents()
    {
        // Implementation — query ComponentRegistry
        return ComponentRegistry.GetAll();
    }

    [McpServerTool, Description(
        "Returns the props/parameters for a named IonBlazor component.")]
    public static string GetComponentProps(
        [Description("Exact component name, e.g. 'IonButton' or 'IonCard'")]
        string componentName)
    {
        return ComponentRegistry.GetProps(componentName);
    }

    [McpServerTool, Description(
        "Generates a minimal Razor usage example for the specified IonBlazor component.")]
    public static string GenerateUsageExample(
        [Description("Component name, e.g. 'IonAccordion'")]
        string componentName,
        [Description("Optional variant or use-case hint, e.g. 'disabled' or 'with-icon'")]
        string? variant = null)
    {
        return ComponentRegistry.GenerateExample(componentName, variant);
    }
}
```

### Structured output (SDK v1.0+ / spec 2025-06-18+)

```csharp
[McpServerTool(UseStructuredContent = true), Description(
    "Returns structured metadata for an IonBlazor component.")]
public static ComponentMetadata GetComponentMetadata(
    [Description("Component name")] string componentName)
{
    return ComponentRegistry.GetMetadata(componentName);
}
```

---

## MCP Client Configuration

### Claude Code (project-scoped)

Create `.mcp.json` at the repo root:

```json
{
  "mcpServers": {
    "ionblazor": {
      "type": "stdio",
      "command": "dotnet",
      "args": ["run", "--project", "./IonBlazor.Mcp", "--no-build"]
    }
  }
}
```

Then register it once:

```bash
claude mcp add-json "ionblazor" \
  '{"type":"stdio","command":"dotnet","args":["run","--project","./IonBlazor.Mcp","--no-build"]}'
```

### Claude Desktop (`claude_desktop_config.json`)

```json
{
  "mcpServers": {
    "ionblazor": {
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "/ABSOLUTE/PATH/TO/IonBlazor.Mcp",
        "--no-build"
      ]
    }
  }
}
```

> **Always use absolute paths** in Claude Desktop config. Restart Claude Desktop after
> every config change — there is no hot reload.

### Published executable (faster cold start)

After `dotnet publish`, point directly at the binary:

```json
{
  "mcpServers": {
    "ionblazor": {
      "command": "/absolute/path/to/IonBlazor.Mcp.exe"
    }
  }
}
```

---

## `.claude/settings.json` (suggested)

```json
{
  "permissions": {
    "allow": [
      "Bash(dotnet build:*)",
      "Bash(dotnet run:*)",
      "Bash(dotnet test:*)",
      "Bash(dotnet restore:*)",
      "Bash(git diff:*)",
      "Bash(git status:*)",
      "Bash(git log:*)"
    ],
    "deny": [
      "Bash(rm -rf:*)",
      "Read(.env)",
      "Read(*.pfx)",
      "Read(*.snk)"
    ]
  }
}
```

---

## Conventions Claude Must Follow

- **Do not scaffold repository wrappers** — tools call `ComponentRegistry` helpers directly.
- **Component names are PascalCase** with the `Ion` prefix: `IonButton`, `IonCard`, etc.
- **Razor syntax** uses `@` directives; parameter binding uses `[Parameter]` attribute.
- **New tools go in `Tools/`**; shared metadata helpers go in `Resources/`.
- **One class per tool file**; class name matches filename.
- **No `async void`** — all async tools return `Task<T>`.
- **Write XML doc comments** on public types and tool classes for IDE discoverability.

---

## What Claude Cannot Infer from Reading the Code

- IonBlazor maps directly to Ionic Web Components — parameter names mirror Ionic prop names.
- The component registry is the source of truth; do not hardcode component lists inline.
- `stdio` transport means the process lifetime is managed by the MCP client — do not add
  `Console.ReadLine()` loops or custom shutdown logic.
- All tool output is ultimately read by an LLM, so keep return values **human-readable text
  or clean JSON** — avoid raw binary or XML blobs.

---

## Testing

```bash
# Unit tests
dotnet test

# Manual smoke test — pipe a JSON-RPC initialize message
echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"0.1"}}}' \
  | dotnet run --project ./IonBlazor.Mcp
```

---

## Useful References

- MCP C# SDK: https://github.com/modelcontextprotocol/csharp-sdk
- MCP Protocol spec: https://modelcontextprotocol.io/docs/develop/build-server
- Claude Code MCP docs: https://docs.claude.com/en/docs/claude-code/mcp
- NuGet package: https://www.nuget.org/packages/ModelContextProtocol
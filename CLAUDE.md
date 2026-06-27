# CLAUDE.md

Guidance for Claude Code (and other AI assistants) working in this repository.

## What this repository is

`CodeManager` is **not a single application**. It is a personal, multi-year
(~2013–2018) sandbox of independent .NET Framework learning projects,
utility libraries, and a couple of vendored third-party libraries, all
checked into one git repo. Commit history (`git log`) is sporadic, in
Chinese and English, and shows no consistent release process — treat this
as an archive/reference collection, not an actively-maintained product.

There is **no repository-wide build, test, or CI setup** (no root solution
file, no `.github/workflows`, no `azure-pipelines.yml`). Each top-level
folder is its own self-contained Visual Studio project/solution with its
own `.sln` file (where applicable). Changes in one folder essentially never
affect another.

Because of this structure, **always scope your work to the specific
top-level folder the user is asking about**. Don't assume conventions from
one project apply to another — they were written at different times, with
different patterns and even different target framework versions.

## Top-level layout

| Folder | Stack | What it is |
|---|---|---|
| `BootstrapMvc` | ASP.NET MVC4 (.NET Framework) | Bootstrap-themed MVC demo/tutorial app. |
| `BuildingWeb` | Static HTML/CSS/JS | Plain frontend demo pages, no backend. |
| `Canvas`, `Canvas.rar` | — | Mostly an unextracted archive; little of value. |
| `Common` | C# class library (no `.sln`) | Grab-bag of reusable helper classes (caching, Excel export via NPOI, email, zip, QR, image processing, encryption, Dapper source study, etc). See `Common/README.md` (Chinese) for an index of every subfolder. |
| `Developing` | ASP.NET MVC, layered (`GMFrameworkForBlog.sln`) | Larger "framework" demo with BLL/DAL/Models/Web layering (`GMF.*` projects). |
| `JqueryUI` | Vendored JS library | jQuery UI — third-party, do not modify. |
| `Ninesky` | ASP.NET MVC, layered (`Ninesky.sln`) | Multi-layer demo: `Ninesky.BLL`, `.DAL`, `.IBLL`, `.IDAL`, `.Models`, `.Web`. |
| `OracelEF` | ASP.NET MVC + Entity Framework | `DSMObileCollection.sln` — mobile/geo data collection app against an Oracle DB, includes coordinate-transform utilities. |
| `QCodeform` | C# | `QR_Encode_Decode.sln` — QR encode/decode using a bundled ThoughtWorks.QRCode library. |
| `SZ.CityManager` | ASP.NET MVC | `SpaceBIA.Case.SZ.CityManager.sln` — city-management web app case study. |
| `SmartSpiderCore-master` / `.zip` | C# (vendored OSS) | Cloned web-scraping engine + a `WinConsole` wrapper. Treat the core library as third-party; the console wrapper may be original. |
| `WCF` | WCF (.NET Framework) | Minimal `WcfService1` SOAP service example. |
| `WXDevelop` | ASP.NET MVC | WeChat (微信) public-API integration demo: `WXDevelop` web app + `WXHelp` helper library. |
| `YunLianLHelp` | WinForms (.NET Framework) | Desktop app demo. |
| `jqwidgets-ver4.1.2` | Vendored JS library | Full jQWidgets v4.1.2 distribution with its own demos/docs — third-party, do not modify. |
| `数据采集` / `.rar` | C# | Web data-collection/scraping utility (`数据采集.sln`). |
| `测试.txt` | — | Empty placeholder/test file. |

## Working with the .NET Framework projects

Most folders (`BootstrapMvc`, `Developing`, `Ninesky`, `OracelEF`,
`QCodeform`, `SZ.CityManager`, `SmartSpiderCore-master`, `WCF`,
`WXDevelop`, `YunLianLHelp`, `数据采集`) are classic **.NET Framework**
(not .NET Core) projects built with old-style `.csproj`/`.sln` files
targeting Visual Studio 2012/2013, using NuGet `packages/` folders (not
`PackageReference`) and `Web.config`/`App.config` for configuration.

- Build with MSBuild or Visual Studio, e.g.:
  `msbuild SomeProject.sln /p:Configuration=Debug`
- These projects generally **will not build on Linux/macOS** without
  Mono or Wine — there is no `dotnet build` support since they predate
  .NET Core's `dotnet` tooling and `PackageReference`-style restore. Don't
  assume `dotnet` commands will work; check the `.csproj` `ToolsVersion`
  and target framework first.
- `bin/`, `obj/`, and `packages/` directories are often committed in this
  repo (this predates good `.gitignore` hygiene in places) — be aware
  binaries may already be checked in; don't assume `git status` clean
  binaries are untouched build artifacts you can freely delete.

## Conventions and pitfalls

- **Mixed language comments/identifiers**: a lot of code, folder names,
  and commit messages are in Chinese. Don't "fix" Chinese names/comments
  to English unless asked — it's intentional, not a mistake.
- **Vendored code**: `JqueryUI/`, `jqwidgets-ver4.1.2/`, and the core of
  `SmartSpiderCore-master/` are third-party libraries pulled into the
  repo. Don't refactor or "clean up" these — only the wrapper/integration
  code around them is the repo owner's own.
- **No tests**: none of these projects have unit tests. Don't assume a
  test command exists; verify changes by building the specific project
  and/or asking the user how they want to validate.
- **`Common/`** has no project file of its own — it's a loose folder of
  C# source meant to be copied/referenced into other projects, organized
  by feature (see `Common/README.md` for the full index in Chinese).
- When asked to add a new feature, prefer adding to the most relevant
  existing folder rather than creating a new top-level project, unless
  the user clearly wants a brand-new standalone demo.

## Git workflow

- Default branch is `master`.
- There's no enforced branch/PR convention in this repo's history; many
  commits go straight to `master`. Follow whatever branching the current
  task/session specifies rather than inferring one from history.

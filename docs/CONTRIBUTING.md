Contributing
============
First off, thanks for taking the time to contribute!

The following is a set of guidelines for contributing to the repository and its packages.
These are mostly guidelines, not rules. 
Use your best judgment, and feel free to propose changes to this document in a pull request.

Contents:
- [Contributing](#contributing)
  - [How can I contribute?](#how-can-i-contribute)
    - [Reporting bugs](#reporting-bugs)
    - [Requesting new features](#requesting-new-features)
  - [Versioning](#versioning)
  - [Git commit messages](#git-commit-messages)
  - [Git branching strategy](#git-branching-strategy)
    - [**Release** - Represents a published package](#release---represents-a-published-package)
    - [**Main** - One to rule them all](#main---one-to-rule-them-all)
    - [**Develop** - Code integration happens here](#develop---code-integration-happens-here)
    - [**Hotfix** - Buggy bugs fixes](#hotfix---buggy-bugs-fixes)
    - [**Task** - Here is where the magic happens](#task---here-is-where-the-magic-happens)
    - [**Experimental** - Temporary branches](#experimental---temporary-branches)
  - [Workflows](#workflows)
    - [Feature development](#feature-development)
    - [Release process](#release-process)
    - [Hot fixing process](#hot-fixing-process)
  - [Styleguides](#styleguides)
    - [Csharp styleguide](#csharp-styleguide)
    - [Documentation styleguide](#documentation-styleguide)
  - [Labels](#labels)
      - [Generic labels](#generic-labels)
      - [Priority labels](#priority-labels)
      - [Work level labels](#work-level-labels)
      - [Status labels](#status-labels)

How can I contribute?
---------------------

### Reporting bugs
Bugs are tracked as issues. 
Before submiting your bug report, please ensure no similar issues have been reported already.
Submit your bug report using the provided template to provide us with enough information to solve it as fast as possible.

### Requesting new features
Features are reported as issues.
Submit your feature request by using the provided feature request template.

Versioning
----------
We follow a basic [semantic versioning](https://semver.org/).
Where X.Y.Z represents a version number:
- X is the MAYOR version, usually migrations and breaking changes.
- Y is the MINOR version, feature and funcitonallity additions with backwards compatibility.
- Z is the PATCH version, bug fixes, performance issues, refactoring, etc.

Git commit messages
-------------------
- Use the imperative mood for subjects (If applied, this commit will [commit-message-here]).
- Whenever possible reference the issue or pull request by providing the id after a pound sign. (i.e `fix: #42 Add missing semicolon`).
- Consider starting the commit message with an applicable prefix, see below).

| Prefix     | Description |
| :-         | :-          |
| `depends:` | Used when modifying dependencies (adding, removing, upgrading, downgrading).
| `enhance:` | Used when adding functionality for a feature. It can be boilerplate with TODOs as placeholders, as well as the actual implemention of those TODOs.
| `fix:`     | Used when fixing a bug. If an issue is related to it, include it in the message.
| `docs:`    | Used when adding or updating documentation. If documentation request is related to it, include it in the message.
| `test:`    | Used when adding or updating tests.
| `secure:`  | Used when making changes in the security. (i.e. Remove connection string, remove api keys).
| `refact:`  | Used when refactoring code. Modifying the code without changing the "under the hood" implementation to improve readability, cohesion, coupling, duplication, etc. (i.e. The renaming variables, moving classes into multiple files, etc).
| `style:`   | Used when applying changes to comply with style guidelines. (i.e. Indentation changes, brackets alignment, etc).
| `cicd:`    | Used for anything related to CI/CD workflows.

Git branching strategy
----------------------
This section explains the different type of branches allowed in the repository.
This will help you name and differentiate between the different branches and easily identify the kind of work being done overall in the repository.
The following table presents a resume of the strategy, for a detailed explanation on each branch keep scrolling.

| Branch          | Checkout from | Merges into |
| :-              | :-            | :-          |
| `main`          | None          | None
| `develop`       | `main`        | `main`
| `feat/#`        | `develop`     | `develop`
| `exp/#`         | Any           | None
| `hfix/#`        | `main`        | `main` and `develop` 

### **Release** - Represents a published package
- *Naming*: `release/#` where `#` is the version number.
- *Description*: This is a published package you can also find in nuget, pip, npm, etc.
- *Lifetime*: Eternal, never deleted
- *Created from*: `main`
- *Merges into*: None
- *Details*:
  - This branches are stored for safe-keeping and as a reference only.  
  - No pull requests will be allowed into this branches.
  - This branches are the representation of a finished milestone.
  - No direct commits are allowed here

### **Main** - One to rule them all
- *Naming*: `main` (aka `master`)
- *Description*: This is considered the most up-to-date and stable version of the repository.
- *Lifetime*: Eternal, never deleted
- *Created from*: None
- *Merges into*: None
- *Details*:
  - There shall only exist one `main` branch per repository.  
  - Never push commits directly to this branch, except when initially setting up a repository.
  - No direct commits are allowed here

### **Develop** - Code integration happens here
- *Naming*: `develop` (aka `dev`, `development`)
- *Description*: Point of integration for all features.
- *Lifetime*: Eternal, never deleted
- *Created from*: `main`
- *Merges into*: None
- *Notes*:
  - Only one `develop` branch shall exist at any given time.
  - Direct commits are only allowed by maintainers, but the recommendation is using a proper `feat\#` branch.

### **Hotfix** - Buggy bugs fixes
- *Naming*: `hfix/#` where `#` is the issue id of the bug if any, else provide an unique descriptive name
- *Created from*: `release/#` or `main`
- *Merges into*: `release/#` and/or `main` and/or `develop`
- *Lifetime*: Short lived, deleted after PR
- *Notes*:
  - The bug fixes usually have a thight relationship to the production environment.
  - Multiple hot fixes branches can exists at the time but the recomendation is to have one at a time, it depends on the situation (i.e. Create 2 branches for 2 bugs (one for each) when you already known that are the bugs are unreleated to each other).
  - The fixes require a patch version increment. (i.e. `1.0.0` changes to `1.0.1`)

### **Task** - Here is where the magic happens
- *Naming*: `task/#` where `#` is the id of the task. If none, provide an unique identifier.
- *Created from*: `develop` 
- *Merges into*: `develop`
- *Lifetime*: Short lived, deleted after PR
- *Notes*:
  - In case the branch you want to work on already exists we encourage you to pick a different one.
  - Alternatively, contact the person working on the other branch and align up to cooperate on it.

### **Experimental** - Temporary branches
- *Naming*: `exp/#` where `#` is a unique name to identify the experiment
- *Created from*: Any
- *Merges into*: None
- *Lifetime*: Variable
- *Notes*:
  - This branches are entirely meant for breaking changes. 
  - Feel free to use them to mess around and do research for specific features. 
  - Use them to share code or snippets with colleages.

Workflows
---------

### Feature development
1. Select a feature you wish to develop. You can find them with with the `feature` label in the issues section.
2. Verify no one is working on the feature already. 
  1. You can do this by checking the branches in the repo.
  2. If there is already someone working on the feature, consider looking for a different feature. 
  3. If you still insist in taking up on that feature, consider at the very least contacting him/her to collaborate. 
3. Pull your branch from `develop`. 
4. Do the proper changes
  1. Remeber to follow the style guidelines.
  2. Document your code.
  3. Add unit testing accordingly.
5. Create a pull request using the `feat_integration` template.
6. If the PR is denied, perform the required changes and push them again.

### Release process
1. Milestones are achieved after enough features have been added.
2. Maintainers ensure release consistency.
  1. Version in .csproj is up to date.
  2. Change log has been updated with the current changes.
3. Maintainers create a PR from develop to main.
  3. Requires approval from all maintainers.
4. The `develop` branch is emrged with `main`.
5. Tags are updated in the `main` branch with the latest commit with the value of the version.
6. Artifact is build and published in github releases.
7. Artifcat is build as nuget packaged and published in nuget packages.

### Hot fixing process
1. Select the bug you wish to work on from the issues section, with the `bug` label.
2. Verify no one is working on the bug already. 
  1. You can do this by checking the branches in the repo.
  2. If there is already someone working on the bug, consider looking for a different feature. 
  3. If you still insist in taking up on that bug, consider at the very least contacting him/her to collaborate. 
3. Pull your branch from the current version of `main`.
4. Do the proper changes
  4. Remeber to follow the style guidelines.
  5. Document your code.
  6. Add unit testing accordingly.

Styleguides
-----------

### Csharp styleguide
- End the file with a line break. It makes it easier to append new data from the cli (if ever required to).
- Follow the best practices.
- Prefer block scoped namespace.
- Prefer namespace level using statements.
- Generally, use the `.editorconfig` for the underlying rules. Modify thorugh a PR as needed.

### Documentation styleguide
- Use markdown for documentation.
- Use a single `h1` header per documentation file.
- Preffer the use of `===` over `#` for `h1` headers.
- Preffer the use of `---` over `##` for `h2` headers.
- Preffer the use of `-` over `*` and `+` for unordered lists.
- Use ` ``` ` followed by the code language for code blocks.
- Use `> #### ` followed by `NOTE`, `WARNING`, or `IMPORTANT` to highlight statements.

Labels
------

#### Generic labels
Use this labels for the tagging and categorizing of issues and pull requests.
Add as many of these as required.

| Color   | Label | Description |
| :-      | :-    | :-          |
| #9F2B00 | `bug` | Indicates something related to a bug-report.
| #489C17 | `feature` | Indicates something related to a feature request. 
| #0000FF | `documentation` | Indicates a need for improvements or additions to documentation.
| #523A28 | `invalid` | Indicates that an issue, pull/merge request, or discussion is no longer relevant.

#### Priority labels
Uses the Eisenhower Decision Principle.
This labels must be provided in pairs, always *importance* + *urgency*.
The *importance* determines how likely is the issue to affect long-term goals or milestones.
The *urgency* determines how much of a fast response is required.
The resulting combination dictates the action to take with the issue:
- `priority::importance::high` + `priority::urgency::high` = Must be done asap
- `priority::importance::high` + `priority::urgency::low` = Schedule them to be done in a calm and proper matter
- `priority::importance::low` + `priority::urgency::high` = Delegate to someone that can approach the issue asap
- `priority::importance::low` + `priority::urgency::low` = Won't do

| Color   | Label | Description |
| :-      | :-    | :-          |
| #C85250 | `priority::importance::high` | Issue is likely to block/affect future work.
| #F7BEC0 | `priority::importance::low` | Issue won't affect future work in a negative way. 
| #C85250 | `priority::urgency::high` | Issue has a close deadline, or asap attention.
| #F7BEC0 | `priority::urgency::low` | Issue deadline is far away.

#### Work level labels
Based of the Cynefin framework. 
This label dictates the level of effor for a specific issue.
The more *disorder* the label indicates, the more knowledge is required to solve it.
Project managers and architects as well as the author of the issue should be able to 
work together to break down highly disorderd issues into smaller ones with a specific
target in it. The amount of breakdown needed is directly dependand on the experience
of the technical team working in the project. For example, a senior developer might
be able to take *complex* tasks, whereas a junior developer will require the task 
to be broken down into *simple* tasks in order for them to accomplish it correctly.

| Color   | Label | Description |
| :-      | :-    | :-          |
| #E6E6FA | `work::simple`      | Consists of "known knowns". The cause and effect relationship is clear: if you do X, expect Y.
| #B1B1B1 | `work::complicated` | Consists of "known unknowns". The cause and effect relationship requires analysis or expertise.
| #747474 | `work::complex`     | Consists of "unknown unknowns". The cause and effect relationship can only be deduced in retrospect.
| #444444 | `work::chaotic`     | Issue is "too confusing to wait for a knowledge-based response". The cause and effect relationship is unclear.
| #0A0708 | `work::disorder`    | Issue with no clarity of which kind of consistency or work applies. 

#### Status labels
Based of the basic Kanban framework. 
This label dictates the status of a current issue.
Ultimately issues are open or closed, but there are also intermediate states that 
are required to be labeled for better tracking. Feel free to add as many as you need,
since this depends on the available environments for the project, and the project itself.

| Color   | Label | Description   |
| :-      | :-    | :-            |
| #00B140 | `status::ongoing`     | The issue is moving towards completion as there is work being made to solve it.
| #FBB80F | `status::testing`     | A solution for the issue has been provided and we are awaiting resolution on the implementation.
| #DC143C | `status::blocked`     | The issue is blocked and unable to work on due to a different open issue in the repository.
| #008672 | `status::help-wanted` | Issue requires help from external parties.
| #E4E669 | `status::deprecated`  | Issue is irrelevant due to a recent commit, pull request or update deprecating it.
